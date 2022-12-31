package root.account;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class AccountTest {

    @Test
    void testBasicDeposit() {
        Account account = new Account();
        assertTrue(account.deposit(100));
        assertEquals(100, account.getBalance());
    }

    @Test
    void testBasicWithdraw() {
        Account account = new Account();
        assertTrue(account.withdraw(1000));
        assertEquals(-1000, account.getBalance());
    }

    @Test
    void testBasicBlock() {
        Account account = new Account();
        account.block();
        assertTrue(account.isBlocked());
    }

    @Test
    void testBasicUnblock() {
        Account account = new Account();
        account.block();
        assertTrue(account.unblock());
        assertFalse(account.isBlocked());
    }

    @Test
    void testBasicSetMaxCredit() {
        Account account = new Account();
        account.block();
        assertTrue(account.setMaxCredit(500));
        assertEquals(500, account.getMaxCredit());
    }

    @Test
    void testIncorrectWithdraw() {
        Account account = new Account();
        assertFalse(account.withdraw(1001));
        assertFalse(account.withdraw(2360));
        assertFalse(account.withdraw(100000));
        assertEquals(0, account.getBalance());
    }

    @Test
    void testUnblockedSetMaxCredit() {
        Account account = new Account();
        assertFalse(account.setMaxCredit(1));
        assertEquals(1000, account.getMaxCredit());
    }

    @Test
    void testTooBigSetMaxCredit() {
        Account account = new Account();
        account.block();
        assertFalse(account.setMaxCredit(1263929));
        assertFalse(account.setMaxCredit(1010101));
        assertFalse(account.setMaxCredit(2000000));
        assertFalse(account.setMaxCredit(-2000000));
        assertEquals(1000, account.getMaxCredit());
    }

    @Test
    void testBlockedSetMaxCredit() {
        Account account = new Account();
        account.block();
        assertTrue(account.setMaxCredit(78823));
        assertEquals(78823, account.getMaxCredit());
    }

    @Test
    void testUnblock() {
        Account account = new Account();
        account.withdraw(50);
        account.block();

        account.setMaxCredit(25);
        assertFalse(account.unblock());
        assertTrue(account.isBlocked());

        account.setMaxCredit(100);
        assertTrue(account.unblock());
        assertFalse(account.isBlocked());
    }

    @Test
    void testTooBigDepositAndWithdraw() {
        Account account = new Account();
        assertFalse(account.deposit(-50));
        assertFalse(account.deposit(2000000));
        assertFalse(account.withdraw(-80));
        assertFalse(account.withdraw(1010101));
        assertEquals(0, account.getBalance());
    }

    @Test
    void testTooBigSumDepositAndWithdraw() {
        Account account = new Account();
        account.deposit(900000);
        assertFalse(account.deposit(200000));
        assertEquals(900000, account.getBalance());

        account = new Account();
        account.block();
        account.setMaxCredit(1000000);
        account.unblock();
        account.withdraw(900000);
        assertEquals(-900000, account.getBalance());
        assertFalse(account.withdraw(200000));
        assertEquals(-900000, account.getBalance());
    }

    @Test
    void testBlock() {
        Account account = new Account();
        account.deposit(2137);
        account.block();
        assertFalse(account.deposit(100));
        assertFalse(account.withdraw(100));
    }
}