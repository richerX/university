package graph;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class Coord2DTest {

    Coord2D coord1, coord2, coord3;

    @BeforeEach
    void beforeEach() {
        coord1 = new Coord2D(1.2, -2.7);
        coord2 = new Coord2D(5.6, 10);
        coord3 = new Coord2D(-0, -5);
    }

    @Test
    void getX() {
        assertEquals(1.2, coord1.getX());
        assertEquals(5.6, coord2.getX());
        assertEquals(0, coord3.getX());
    }

    @Test
    void getY() {
        assertEquals(-2.7, coord1.getY());
        assertEquals(10, coord2.getY());
        assertEquals(-5, coord3.getY());
    }

    @Test
    void testToString() {
        assertEquals("X = 1.2, Y = -2.7", coord1.toString());
        assertEquals("X = 5.6, Y = 10.0", coord2.toString());
        assertEquals("X = 0.0, Y = -5.0", coord3.toString());
    }
}