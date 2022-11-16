package graph;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class SpaceTest {

    Space space;

    @BeforeEach
    void beforeEach() throws DAGConstraintException {
        space = new Space(new Coord2D(0, 0));
        Origin origin = new Origin(new Coord2D(3, 9));
        Point point = new Point(new Coord2D(7, 2));

        space.add(origin);
        space.add(point);
        origin.add(point);
    }

    @Test
    void testToString() {
        String[] text = space.toString().split(System.lineSeparator());
        assertEquals("Space:  X = 0.0, Y = 0.0 <2>", text[0]);
        // Так как используется HashSet, то следующие тесты платформо-зависимы
        // assertEquals("    Point:  X = 7.0, Y = 2.0", text[1]);
        // assertEquals("    Origin: X = 3.0, Y = 9.0 <1>", text[2]);
        // assertEquals("        Point:  X = 7.0, Y = 2.0", text[3]);
    }
}