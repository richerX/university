package game;

import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

class PieceTest {
    Piece piece;

    @Test
    void add() {
        piece = new Piece(0, 0);
        for (int i = 0; i < 5; ++i) {
            piece.add(i, -i, new Rectangle());
        }

        Assertions.assertEquals(5.0, piece.rectangles.size());

        Assertions.assertEquals(0.0, piece.xs.get(0));
        Assertions.assertEquals(1.0, piece.xs.get(1));
        Assertions.assertEquals(2.0, piece.xs.get(2));
        Assertions.assertEquals(3.0, piece.xs.get(3));
        Assertions.assertEquals(4.0, piece.xs.get(4));

        Assertions.assertEquals(0.0, piece.ys.get(0));
        Assertions.assertEquals(-1.0, piece.ys.get(1));
        Assertions.assertEquals(-2.0, piece.ys.get(2));
        Assertions.assertEquals(-3.0, piece.ys.get(3));
        Assertions.assertEquals(-4.0, piece.ys.get(4));
    }

    @Test
    void teleport() {
        piece = new Piece(0, 0);
        piece.add(0, 0, new Rectangle());

        piece.teleport(-5, 7);
        Assertions.assertEquals(-5.0, piece.xs.get(0));
        Assertions.assertEquals(7.0, piece.ys.get(0));

        piece.teleport(100, 3);
        Assertions.assertEquals(100.0, piece.xs.get(0));
        Assertions.assertEquals(3.0, piece.ys.get(0));
    }

    @Test
    void move() {
        piece = new Piece(0, 0);
        piece.add(0, 0, new Rectangle());

        piece.move(-5, 7);
        Assertions.assertEquals(-5.0, piece.xs.get(0));
        Assertions.assertEquals(7.0, piece.ys.get(0));

        piece.move(100, 3);
        Assertions.assertEquals(95.0, piece.xs.get(0));
        Assertions.assertEquals(10.0, piece.ys.get(0));
    }

    @Test
    void setColor() {
        piece = new Piece(0, 0);
        piece.add(0, 0, new Rectangle());
        piece.setColor(Color.GREEN);
        for (var rectangle : piece.rectangles) {
            Assertions.assertEquals(Color.GREEN, rectangle.getFill());
        }
    }
}