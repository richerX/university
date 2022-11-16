package game;

import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;

import java.util.ArrayList;

public class Piece {
    public double x;
    public double y;
    public ArrayList<Double> xs = new ArrayList<>();
    public ArrayList<Double> ys = new ArrayList<>();
    public ArrayList<Rectangle> rectangles = new ArrayList<>();

    /**
     * Конструктор класса
     * @param x начальная координата
     * @param y начальная координата
     */
    public Piece(double x, double y) {
        teleport(x, y);
    }

    /**
     * Добавить новую клетку в фигуру
     * @param x сдвиг по координате
     * @param y сдвиг по координате
     * @param rectangle объект-клетка
     */
    public void add(double x, double y, Rectangle rectangle) {
        this.xs.add(x);
        this.ys.add(y);
        this.rectangles.add(rectangle);
    }

    /**
     * Перемещение в точку (абсолютные координаты)
     * @param new_x новая координата
     * @param new_y новая координата
     */
    public void teleport(double new_x, double new_y) {
        move(new_x - x, new_y - y);
    }

    /**
     * Перемещение в сторону (относительные координаты)
     * @param dx сдвиг по координате
     * @param dy сдвиг по координате
     */
    public void move(double dx, double dy) {
        for (int i = 0; i < rectangles.size(); ++i) {
            xs.set(i, xs.get(i) + dx);
            ys.set(i, ys.get(i) + dy);
            rectangles.get(i).setTranslateX(xs.get(i));
            rectangles.get(i).setTranslateY(ys.get(i));
        }
        this.x += dx;
        this.y += dy;
    }

    /**
     * Перекраска всей фигуры
     * @param color новый цвет
     */
    public void setColor(Color color) {
        for (Rectangle rectangle : rectangles) {
            rectangle.setFill(color);
        }
    }

    /**
     * Удаление объекта
     * @param pane родительский объект
     */
    public void dispose(Pane pane) {
        for (Rectangle rectangle : rectangles) {
            pane.getChildren().remove(rectangle);
        }
    }
}