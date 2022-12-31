package root.integrat;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import org.mockito.Mock;
import org.mockito.Mockito;

import static org.mockito.Mockito.when;
import org.mockito.junit.jupiter.MockitoExtension;
import org.junit.jupiter.api.extension.ExtendWith;

@ExtendWith(MockitoExtension.class)
class AccountManagerTest {

    @Mock
    IServer serverMock;
    AccountManager manager = Mockito.spy(AccountManager.class);

    @BeforeEach
    void init() {
        assertNotNull(serverMock);
        manager.init(serverMock);
    }

    @Test
    void callLogin() {
        when(manager.makeSecure("password")).thenReturn("password");
        when(manager.makeSecure("wrong")).thenReturn("wrong");
        when(manager.makeSecure("password2")).thenReturn("password2");

        when(serverMock.login("login", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.login("login", "wrong")).thenReturn(new ServerResponse(ServerResponse.NO_USER_INCORRECT_PASSWORD, 2L));
        when(serverMock.login("login2", "password2")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, new Object()));

        assertEquals(AccountManagerResponse.NO_USER_INCORRECT_PASSWORD, manager.callLogin("login", "wrong").code);
        assertEquals(AccountManagerResponse.SUCCEED, manager.callLogin("login", "password").code);
        assertEquals(AccountManagerResponse.ALREADY_LOGGED, manager.callLogin("login", "password").code);
        assertEquals(AccountManagerResponse.INCORRECT_RESPONSE, manager.callLogin("login2", "password2").code);
    }

    @Test
    void callLogout() {
        when(manager.makeSecure("password")).thenReturn("password");

        when(serverMock.login("login2", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 2L));
        when(serverMock.login("login3", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 3L));
        when(serverMock.login("login4", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 4L));
        when(serverMock.logout(3L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, new Object()));
        when(serverMock.logout(4L)).thenReturn(new ServerResponse(ServerResponse.NO_MONEY, new Object()));

        assertEquals(AccountManagerResponse.NOT_LOGGED, manager.callLogout("login", 1L).code);
        manager.callLogin("login2", "password");
        assertEquals(AccountManagerResponse.INCORRECT_SESSION, manager.callLogout("login2", 1L).code);
        manager.callLogin("login3", "password");
        assertEquals(AccountManagerResponse.SUCCEED, manager.callLogout("login3", 3L).code);
        manager.callLogin("login4", "password");
        assertEquals(AccountManagerResponse.INCORRECT_RESPONSE, manager.callLogout("login4", 4L).code);
    }

    @Test
    void withdraw() {
        when(manager.makeSecure("password")).thenReturn("password");

        when(serverMock.login("login1", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.login("login2", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 2L));
        when(serverMock.login("login3", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 3L));
        when(serverMock.withdraw(1L, 100)).thenReturn(new ServerResponse(ServerResponse.NO_MONEY, 1.0));
        when(serverMock.withdraw(2L, 100)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 100.0));
        when(serverMock.withdraw(3L, 100)).thenReturn(new ServerResponse(ServerResponse.UNDEFINED_ERROR, new Object()));

        assertEquals(AccountManagerResponse.NOT_LOGGED, manager.withdraw("login", 1L, 100).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.INCORRECT_SESSION, manager.withdraw("login1", 2L, 100).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.NO_MONEY, manager.withdraw("login1", 1L, 100).code);
        manager.callLogin("login2", "password");
        assertEquals(AccountManagerResponse.SUCCEED, manager.withdraw("login2", 2L, 100).code);
        manager.callLogin("login3", "password");
        assertEquals(AccountManagerResponse.INCORRECT_RESPONSE, manager.withdraw("login3", 3L, 100).code);
    }

    @Test
    void deposit() {
        when(manager.makeSecure("password")).thenReturn("password");

        when(serverMock.login("login1", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.login("login2", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 2L));
        when(serverMock.deposit(1L, 100)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 100.0));
        when(serverMock.deposit(2L, 100)).thenReturn(new ServerResponse(ServerResponse.UNDEFINED_ERROR, new Object()));

        assertEquals(AccountManagerResponse.NOT_LOGGED, manager.deposit("login", 1L, 100).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.INCORRECT_SESSION, manager.deposit("login1", 2L, 100).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.SUCCEED, manager.deposit("login1", 1L, 100).code);
        manager.callLogin("login2", "password");
        assertEquals(AccountManagerResponse.INCORRECT_RESPONSE, manager.deposit("login2", 2L, 100).code);
    }

    @Test
    void getBalance() {
        when(manager.makeSecure("password")).thenReturn("password");

        when(serverMock.login("login1", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.login("login2", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 2L));
        when(serverMock.getBalance(1L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 100.0));
        when(serverMock.getBalance(2L)).thenReturn(new ServerResponse(ServerResponse.UNDEFINED_ERROR, new Object()));

        assertEquals(AccountManagerResponse.NOT_LOGGED, manager.getBalance("login", 1L).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.INCORRECT_SESSION, manager.getBalance("login1", 2L).code);
        manager.callLogin("login1", "password");
        assertEquals(AccountManagerResponse.SUCCEED, manager.getBalance("login1", 1L).code);
        manager.callLogin("login2", "password");
        assertEquals(AccountManagerResponse.INCORRECT_RESPONSE, manager.getBalance("login2", 2L).code);
    }

    @Test
    void scenario_1() {
        when(manager.makeSecure("password")).thenReturn("password");
        when(manager.makeSecure("wrong")).thenReturn("wrong");

        when(serverMock.login("wrong", "password")).thenReturn(new ServerResponse(ServerResponse.NO_USER_INCORRECT_PASSWORD, 1L));
        when(serverMock.login("login", "wrong")).thenReturn(new ServerResponse(ServerResponse.NO_USER_INCORRECT_PASSWORD, 1L));
        when(serverMock.login("login", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.getBalance(1L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 50.0));
        when(serverMock.deposit(1L, 100)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 100.0));

        manager.callLogin("wrong", "password");
        manager.callLogin("login", "wrong");
        manager.callLogin("login", "password");
        Double currentBalance = (Double) manager.getBalance("login", 1L).response;
        manager.deposit("login", 1L, 100);
        when(serverMock.getBalance(1L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 150.0));
        assertEquals(currentBalance + 100, (Double) manager.getBalance("login", 1L).response);
    }

    @Test
    void scenario_2() {
        when(manager.makeSecure("password")).thenReturn("password");

        when(serverMock.login("login", "password")).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 1L));
        when(serverMock.withdraw(1L, 50)).thenReturn(new ServerResponse(ServerResponse.NO_MONEY, 20.0));
        when(serverMock.getBalance(1L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 20.0));
        when(serverMock.deposit(1L, 100)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 100.0));
        when(serverMock.logout(1L)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, new Object()));

        manager.callLogin("login", "password");
        assertEquals(AccountManagerResponse.NO_MONEY, manager.withdraw("login", 1L, 50).code);
        Double currentBalance = (Double) manager.getBalance("login", 1L).response;
        manager.deposit("login", 1L, 100);
        assertEquals(AccountManagerResponse.INCORRECT_SESSION, manager.withdraw("login", 2L, 50).code);
        when(serverMock.withdraw(1L, 50)).thenReturn(new ServerResponse(ServerResponse.SUCCESS, 50.0));
        assertEquals(AccountManagerResponse.SUCCEED, manager.withdraw("login", 1L, 50).code);
        assertEquals(AccountManagerResponse.SUCCEED, manager.callLogout("login", 1L).code);
    }
}