package graph;

public class Point {
    private Coord2D position;
    BoundBox bounds;

    /**
     * Конструктор класса
     * @param value координаты
     */
    public Point(Coord2D value) {
        position = value;
    }

    /**
     * Метод, возвращающий координаты точки
     * @return текущие координаты
     */
    public Coord2D getPosition() {
        return position;
    }

    /**
     * Метод, задающий координаты точки
     * @param newValue новые координаты
     */
    public void setPosition(Coord2D newValue) {
        position = newValue;
    }

    /**
     * Метод, возвращающий ограничивающий прямоугольник
     * @return ограничивающий прямоугольник
     */
    public BoundBox getBounds() {
        bounds = new BoundBox(position);
        return bounds;
    }

    /**
     * Переопределение вывода объекта в строкуПереопределение вывода объекта в строку
     * @return строка объекта
     */
    @Override
    public String toString() {
        return "Point:  " + position.toString();
    }
}
