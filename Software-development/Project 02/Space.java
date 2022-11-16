package graph;

public class Space extends Origin {

    /**
     * Конструктор класса
     * @param value координаты точки
     */
    public Space(Coord2D value) {
        super(value);
    }

    /**
     * Переопределение вывода объекта в строку
     * @return строка объекта
     */
    @Override
    public String toString() {
        return getString(this, "    ");
    }

    /**
     * Дополнительный рекурсивный метод вывода объекта в строку
     * @param current текущая вершина
     * @param tabs сдвиг
     * @return строка объекта
     */
    private String getString(Point current, String tabs) {
        StringBuilder answer;
        if (current instanceof Space) {
            answer = new StringBuilder("Space:  " + getPosition().toString() + " <" + getChildren().size() + ">" + System.lineSeparator());
        } else {
            answer = new StringBuilder(current.toString() + System.lineSeparator());
        }

        if (current instanceof Origin) {
            var children = ((Origin)current).getChildren();
            for (var child : children) {
                answer.append(tabs);
                answer.append(getString(child, tabs + "    "));
            }
        }
        return answer.toString();
    }
}
