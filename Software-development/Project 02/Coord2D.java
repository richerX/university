package graph;

public class Coord2D {
    private final double x;
    private final double y;

    /**
     * Конструктор класса
     * @param x координата
     * @param y координата
     */
    public Coord2D(double x, double y) {
        this.x = x;
        this.y = y;
    }

    /**
     * Метод, возвращающий координату по оси X
     * @return координата по оси X
     */
    public double getX() {
        return x;
    }

    /**
     * Метод, возвращающий координату по оси Y
     * @return координата по оси Y
     */
    public double getY() {
        return y;
    }

    /**
     * Переопределение вывода объекта в строку
     * @return строка объекта
     */
    @Override
    public String toString() {
        return "X = " + x + ", Y = " + y;
    }
}
