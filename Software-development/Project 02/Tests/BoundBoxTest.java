package graph;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BoundBoxTest {

    BoundBox bounds1, bounds2;

    @BeforeEach
    void beforeEach() {
        bounds1 = new BoundBox(new Coord2D(1, 2));
        bounds2 = new BoundBox(new Coord2D(-2, -3), new Coord2D(7, 9));
    }

    @Test
    void getMinimum() {
        assertEquals(1, bounds1.getMinimum().getX());
        assertEquals(2, bounds1.getMinimum().getY());

        assertEquals(-2, bounds2.getMinimum().getX());
        assertEquals(-3, bounds2.getMinimum().getY());
    }

    @Test
    void getMaximum() {
        assertEquals(1, bounds1.getMaximum().getX());
        assertEquals(2, bounds1.getMaximum().getY());

        assertEquals(7, bounds2.getMaximum().getX());
        assertEquals(9, bounds2.getMaximum().getY());
    }

    @Test
    void testToString() {
        assertEquals("Minimum = {X = 1.0, Y = 2.0}, maximum = {X = 1.0, Y = 2.0}", bounds1.toString());
        assertEquals("Minimum = {X = -2.0, Y = -3.0}, maximum = {X = 7.0, Y = 9.0}", bounds2.toString());
    }
}