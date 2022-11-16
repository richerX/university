package Game;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PlayerTest {
    Player player1, player2;

    @BeforeEach
    void beforeEach() {
        Croupier croupier = new Croupier(2);
        player1 = new Player(croupier, 0);
        player2 = new Player(croupier, 1);
    }

    @Test
    void changePoints() {
        player1.changePoints(10);
        player2.changePoints(-5);
        assertEquals(10, player1.points);
        assertEquals(-5, player2.points);
    }

    @Test
    void testToString() {
        assertEquals("Player  [id = 0, points = 0]", player1.toString());
        assertEquals("Player  [id = 1, points = 0]", player2.toString());
    }
}