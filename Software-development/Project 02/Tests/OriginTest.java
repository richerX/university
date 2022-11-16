package graph;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;

import static org.junit.jupiter.api.Assertions.*;

class OriginTest {

    Point point1, point2, point3;
    Origin origin1, origin2, origin3;

    @BeforeEach
    void beforeEach() throws DAGConstraintException {
        point1 = new Point(new Coord2D(1, 1));
        point2 = new Point(new Coord2D(2, 2));
        point3 = new Point(new Coord2D(-3, 3));

        origin1 = new Origin(new Coord2D(1, 1));
        origin2 = new Origin(new Coord2D(2, 2));
        origin3 = new Origin(new Coord2D(-3, 3));

        origin1.add(point1);
        origin1.add(origin2);
        origin2.add(point2);
        origin2.add(point3);
        origin2.add(point3);
        origin2.add(origin3);
    }

    @Test
    void getChildren() {
        assertEquals(2, origin1.getChildren().size());
        assertEquals(3, origin2.getChildren().size());
        assertEquals(0, origin3.getChildren().size());

        assertTrue(origin1.getChildren().contains(point1));
        assertTrue(origin1.getChildren().contains(origin2));
        assertTrue(origin2.getChildren().contains(point2));
        assertTrue(origin2.getChildren().contains(point3));
        assertTrue(origin2.getChildren().contains(origin3));
    }

    @Test
    void setChildren() throws DAGConstraintException {
        Set<Point> newChildren = new HashSet<>();
        newChildren.add(point1);
        newChildren.add(point2);
        newChildren.add(point3);
        origin1.setChildren(newChildren);

        assertEquals(3, origin1.getChildren().size());
        assertTrue(origin1.getChildren().contains(point1));
        assertTrue(origin1.getChildren().contains(point2));
        assertTrue(origin1.getChildren().contains(point3));
    }

    @Test
    void add() throws DAGConstraintException {
        origin1.setChildren(new HashSet<>());
        origin1.add(origin2);
        origin1.add(origin3);
        origin1.add(point1);
        origin1.add(point2);
        origin1.add(point3);
        origin1.add(point1);

        DAGConstraintException thrown = assertThrows(DAGConstraintException.class, () -> origin1.add(origin1));
        assertTrue(thrown.getMessage().contains("Невозможно добавить узел - в графе появится цикл."));

        thrown = assertThrows(DAGConstraintException.class, () -> origin2.add(origin1));
        assertTrue(thrown.getMessage().contains("Невозможно добавить узел - в графе появится цикл."));

        thrown = assertThrows(DAGConstraintException.class, () -> origin3.add(origin1));
        assertTrue(thrown.getMessage().contains("Невозможно добавить узел - в графе появится цикл."));
    }

    @Test
    void getBounds() {
        assertEquals(0, origin1.getBounds().getMinimum().getX());
        assertEquals(5, origin1.getBounds().getMaximum().getX());
        assertEquals(1, origin1.getBounds().getMinimum().getY());
        assertEquals(6, origin1.getBounds().getMaximum().getY());

        assertEquals(-1, origin2.getBounds().getMinimum().getX());
        assertEquals(4, origin2.getBounds().getMaximum().getX());
        assertEquals(2, origin2.getBounds().getMinimum().getY());
        assertEquals(5, origin2.getBounds().getMaximum().getY());

        assertEquals(-3, origin3.getBounds().getMinimum().getX());
        assertEquals(-3, origin3.getBounds().getMaximum().getX());
        assertEquals(3, origin3.getBounds().getMinimum().getY());
        assertEquals(3, origin3.getBounds().getMaximum().getY());
    }

    @Test
    void testToString() {
        assertEquals("Origin: X = 1.0, Y = 1.0 <2>", origin1.toString());
        assertEquals("Origin: X = 2.0, Y = 2.0 <3>", origin2.toString());
        assertEquals("Origin: X = -3.0, Y = 3.0 <0>", origin3.toString());
    }
}