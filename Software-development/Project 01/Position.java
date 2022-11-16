package battleship;

public class Position {
    int i;
    int j;
    boolean isHorizontal;

    /**
     * Конструктор класса.
     * @param i позиция по горизонтали
     * @param j позиция по вертикали
     * @param isHorizontal является ли расположение горизонтальным
     */
    public Position(int i, int j, boolean isHorizontal) {
        this.i = i;
        this.j = j;
        this.isHorizontal = isHorizontal;
    }
}
