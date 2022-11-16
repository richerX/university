package Game;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class SharperTest {
    Player player1, player2;

    @BeforeEach
    void beforeEach() {
        Croupier croupier = new Croupier(2);
        player1 = new Sharper(croupier, 0);
        player2 = new Sharper(croupier, 1);
    }

    @Test
    void testToString() {
        assertEquals("Sharper [id = 0, points = 0]", player1.toString());
        assertEquals("Sharper [id = 1, points = 0]", player2.toString());
    }
}