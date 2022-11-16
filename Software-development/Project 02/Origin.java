package graph;

import java.util.HashSet;
import java.util.Set;
import java.util.Stack;

public class Origin extends Point {
    private final Set<Point> children;
    private BoundBox bounds;
    private double minimumX, minimumY, maximumX, maximumY;

    /**
     * Конструктор класса
     * @param value координаты точки
     */
    public Origin(Coord2D value) {
        super(value);
        children = new HashSet<>();
    }

    /**
     * Метод, возвращащий текущих потомков вершины
     * @return список потомков
     */
    public Set<Point> getChildren() {
        return new HashSet<>(children);
    }

    /**
     * Метод, задающий новых потомков вершины
     * @param newChildren список потомков
     * @throws DAGConstraintException цикл в графе
     */
    public void setChildren(Set<Point> newChildren) throws DAGConstraintException {
        children.clear();
        for (Point child : newChildren) {
            add(child);
        }
    }

    /**
     * Добавление нового потомка
     * @param newChild новый потомок
     * @throws DAGConstraintException цикл в графе
     */
    public void add(Point newChild) throws DAGConstraintException {
        if (newChild instanceof Origin && !correctNode((Origin) newChild)) {
            throw new DAGConstraintException("Невозможно добавить узел - в графе появится цикл.");
        }
        children.add(newChild);
    }

    /**
     * Метод, возвращающий ограничивающий прямоугольник
     * @return ограничивающий прямоугольник
     */
    public BoundBox getBounds() {
        minimumX = Double.MAX_VALUE;
        minimumY = Double.MAX_VALUE;
        maximumX = -Double.MAX_VALUE;
        maximumY = -Double.MAX_VALUE;

        searchExtremePoints(this, 0, 0);
        bounds = new BoundBox(new Coord2D(minimumX, minimumY), new Coord2D(maximumX, maximumY));
        return bounds;
    }

    /**
     * Дополнительный метод, возвращающий точки экстремума
     * @param current текущая вершина
     * @param currentX текущая координата
     * @param currentY текущая координата
     */
    private void searchExtremePoints(Point current, double currentX, double currentY) {
        currentX += current.getPosition().getX();
        currentY += current.getPosition().getY();

        maximumX = Math.max(maximumX, currentX);
        minimumX = Math.min(minimumX, currentX);
        maximumY = Math.max(maximumY, currentY);
        minimumY = Math.min(minimumY, currentY);

        if (current instanceof Origin) {
            var children = ((Origin)current).getChildren();
            for (var child : children) {
                searchExtremePoints(child, currentX, currentY);
            }
        }
    }

    /**
     * Проверка ацикличности графа при добавлении новой вершины
     * @param newNode новый потомок
     * @return корректность добавления вершины
     */
    private boolean correctNode(Origin newNode) {
        Stack<Point> stack = new Stack<>();
        stack.push(newNode);
        Point current;
        while (!stack.empty()) {
            current = stack.pop();
            if (current instanceof Origin) {
                if (current == this) {
                    return false;
                }
                var children = ((Origin)current).getChildren();
                for (var child : children) {
                    stack.push(child);
                }
            }
        }
        return true;
    }

    /**
     * Переопределение вывода объекта в строку
     * @return строка объекта
     */
    @Override
    public String toString() {
        return "Origin: " + getPosition().toString() + " <" + children.size() + ">";
    }
}

/**
 * Исключение, вызывающиеся при обнаружении цикла в графе
 */
class DAGConstraintException extends Exception {
    public DAGConstraintException (String message){
        super(message);
    }
}
