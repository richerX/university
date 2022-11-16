package battleship;

import java.util.ArrayList;
import java.util.Random;

public class Ocean {
    int n, m;
    int[][] field;
    int torpedoes;
    private final int[] ships;

    /**
     * Констурктор класса.
     * @param input значения для заполнения океана
     */
    public Ocean(int[] input) {
        this.n = input[0];
        this.m = input[1];
        field = new int[n][m];
        ships = new int[5];
        System.arraycopy(input, 2, ships, 0, 5);
        torpedoes = input[7];
    }

    /**
     * Заполнение океана кораблями.
     * @return успешно ли прошло заполнение
     */
    public boolean fill() {
        for (int i = 0; i < Constants.numberOfFillFieldTries; i++) {
            if (tryFill()) {
                return true;
            }
        }
        return false;
    }

    /**
     * Вывод океана с кораблями в консоль.
     */
    public void print() {
        // Первые две строки поля
        StringBuilder firstTopString = new StringBuilder("IEK |");
        StringBuilder secondTopString = new StringBuilder("--- +");
        for (int j = 0; j < m; j++) {
            firstTopString.append(" ").append(String.format("%1$3d", j + 1).replace(' ', '0'));
            secondTopString.append(" " + "---");
            if ((j + 1) % Constants.divider == 0 || j == m - 1) {
                firstTopString.append(" |");
                secondTopString.append(" |");
            }
        }
        System.out.println(firstTopString);
        System.out.println(secondTopString);

        // Все остальные строки поля
        for (int i = 0; i < n; i++) {
            System.out.print(String.format("%1$3d", i + 1).replace(' ', '0') + " |");
            for (int j = 0; j < m; j++) {
                System.out.print("  " + getChar(field[i][j]) + " ");
                if ((j + 1) % Constants.divider == 0 || j == m - 1) {
                    System.out.print(" |");
                }
            }
            System.out.println();

            // Горизантальные "отбивочные" линии
            if ((i + 1) % Constants.divider == 0 || i == n - 1) {
                System.out.print("--- |");
                for (int j = 0; j < m; j++) {
                    System.out.print(" ---");
                    if ((j + 1) % Constants.divider == 0) {
                        System.out.print(" |");
                    }
                }
                System.out.println();
            }
        }
        System.out.println();
    }

    // Одна попытка заполнить океан кораблями.
    private boolean tryFill() {
        ArrayList<Position> correctPositions;
        Position currentPosition;
        for (int length = 5; length > 0; length--) {
            for (int count = 0; count < ships[length - 1]; count++) {
                correctPositions = getCorrectPositions(length);
                if (correctPositions.size() == 0){
                    clearField();
                    return false;
                }
                currentPosition = correctPositions.get(new Random().nextInt(correctPositions.size()));
                for (int shift = 0; shift < length; shift++) {
                    if (currentPosition.isHorizontal) {
                        field[currentPosition.i + shift][currentPosition.j] = length;
                    } else {
                        field[currentPosition.i][currentPosition.j + shift] = length;
                    }
                }
            }
        }
        return true;
    }

    // Очиста океана от кораблей.
    private void clearField() {
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                field[i][j] = 0;
            }
        }
    }

    // Получить все возможные позиции для конкретного корабля.
    private ArrayList<Position> getCorrectPositions(int shipLength) {
        Position currentPosition;
        ArrayList<Position> answer = new ArrayList<Position>();
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                for (boolean isHorizontal : Constants.booleans) {
                    currentPosition = new Position(i, j, isHorizontal);
                    if (isCorrectPosition(shipLength, currentPosition)) {
                        answer.add(currentPosition);
                    }
                }
            }
        }
        return answer;
    }

    // Проверка корректности конкретной позиции для конкретного корабля.
    private boolean isCorrectPosition(int shipLength, Position position) {
        int dx = position.isHorizontal ? 1 : 0;
        int dy = position.isHorizontal ? 0 : 1;
        if (position.i + shipLength * dx > n || position.j + shipLength * dy > m) {
            return false;
        }
        for (int lengthShift = 0; lengthShift < shipLength; lengthShift++) {
            if (!isCorrectCell(position.i + dx * lengthShift, position.j + dy * lengthShift)) {
                return false;
            }
        }
        return true;
    }

    // Является ли клетка допустимой для размещения.
    private boolean isCorrectCell(int i, int j) {
        for (int dx : Constants.lineShifts) {
            for (int dy : Constants.lineShifts) {
                if (0 <= i + dx && i + dx < n && 0 <= j + dy && j + dy < m && field[i + dx][j + dy] != 0) {
                    return false;
                }
            }
        }
        return true;
    }

    // Получения символа по значению в клетке океана.
    private static char getChar(int number) {
        if (Constants.adminPrint && 1 <= number && number <= 5) {
            return (char) ('1' + number - 1);
        } else if (number == Constants.missed) {
            return Constants.chars[1];
        } else if (number == Constants.hit) {
            return Constants.chars[2];
        } else if (number == Constants.sunk) {
            return Constants.chars[3];
        }
        return Constants.chars[0];
    }
}
