package graph;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PointTest {

    Point point1, point2;

    @BeforeEach
    void beforeEach() {
        point1 = new Point(new Coord2D(1.5, -7));
        point2 = new Point(new Coord2D(-10, 3.2));
    }

    @Test
    void getPosition() {
        assertEquals(1.5, point1.getPosition().getX());
        assertEquals(-7, point1.getPosition().getY());
        assertEquals(-10, point2.getPosition().getX());
        assertEquals(3.2, point2.getPosition().getY());
    }

    @Test
    void setPosition() {
        point1.setPosition(new Coord2D(1, 2));
        point2.setPosition(new Coord2D(3, 0));
        assertEquals(1, point1.getPosition().getX());
        assertEquals(2, point1.getPosition().getY());
        assertEquals(3, point2.getPosition().getX());
        assertEquals(0, point2.getPosition().getY());
    }

    @Test
    void getBounds() {
        assertEquals(1.5, point1.getBounds().getMinimum().getX());
        assertEquals(1.5, point1.getBounds().getMaximum().getX());
        assertEquals(-7, point1.getBounds().getMinimum().getY());
        assertEquals(-7, point1.getBounds().getMaximum().getY());

        assertEquals(-10, point2.getBounds().getMinimum().getX());
        assertEquals(-10, point2.getBounds().getMaximum().getX());
        assertEquals(3.2, point2.getBounds().getMinimum().getY());
        assertEquals(3.2, point2.getBounds().getMaximum().getY());
    }

    @Test
    void testToString() {
        assertEquals("Point:  X = 1.5, Y = -7.0", point1.toString());
        assertEquals("Point:  X = -10.0, Y = 3.2", point2.toString());
    }
}