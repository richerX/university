package graph;

public class BoundBox {
    private final Coord2D minimum;
    private final Coord2D maximum;

    /**
     * Конструктор класса
     * @param point координаты точки
     */
    public BoundBox(Coord2D point) {
        this.minimum = point;
        this.maximum = point;
    }

    /**
     * Конструктор класса
     * @param minimum координаты левого конца диагонали
     * @param maximum координаты правого конца диагонали
     */
    public BoundBox(Coord2D minimum, Coord2D maximum) {
        this.minimum = minimum;
        this.maximum = maximum;
    }

    /**
     * Метод, возвращающий координаты левого конца диагонали
     * @return координаты левого конца диагонали
     */
    public Coord2D getMinimum() {
        return minimum;
    }

    /**
     * Метод, возвращающий координаты правого конца диагонали
     * @return координаты правого конца диагонали
     */
    public Coord2D getMaximum() {
        return maximum;
    }

    /**
     * Переопределение вывода объекта в строку
     * @return строка объекта
     */
    @Override
    public String toString() {
        return "Minimum = {" + minimum + "}, maximum = {" + maximum + "}";
    }
}
