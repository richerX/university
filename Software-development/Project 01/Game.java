package battleship;

import java.util.Objects;

public class Game {
    private final Ocean ocean;
    private String lastShot;
    private boolean isTorpedoShot;

    private int totalHp;
    private int totalTurns;
    private int totalTorpedoes;

    /**
     * Конструктор класса.
     * @param ocean океан игры
     */
    public Game(Ocean ocean) {
        this.ocean = ocean;
        lastShot = "";
        isTorpedoShot = false;

        totalHp = 0;
        totalTurns = 0;
        totalTorpedoes = ocean.torpedoes;

        updateHp();
    }

    /**
     * Запустить игру.
     */
    public void run() {
        while (true) {
            Input.clearConsole();
            System.out.println(" > МОРСКОЙ БОЙ < ");
            System.out.println();
            System.out.println("Текущий ход: " + (totalTurns + 1));
            System.out.println("Количество торпед: " + totalTorpedoes);
            System.out.println("Общее здоровье врага: " + totalHp);
            System.out.println("Результат предыдущего выстрела: " + lastShot);
            System.out.println();
            ocean.print();
            updateHp();
            if (totalHp == 0) {
                break;
            }
            shot(getShotInfo());
            totalTurns += 1;
        }
        System.out.println("Поздравляю! Ты победил. Общее количество ходов = " + totalTurns);
        System.out.println();
    }

    // Обновление здоровья врага.
    private void updateHp() {
        totalHp = 0;
        for (int i = 0; i < ocean.n; i++) {
            for (int j = 0; j < ocean.m; j++) {
                totalHp += (0 < ocean.field[i][j] && ocean.field[i][j] <= 5) ? 1 : 0;
            }
        }
    }

    // Получение инфомации о выстреле от пользователя.
    private int[] getShotInfo() {
        String input;
        String[] inputArray;
        int[] answer = new int[2];
        while (true) {
            System.out.print("Введите два числа через пробел (строку и столбец): ");
            input = Constants.console.nextLine();
            inputArray = input.strip().split(" ");
            try {
                // Ввод клетки для выстрела
                answer[0] = Integer.parseInt(inputArray[0]) - 1;
                answer[1] = Integer.parseInt(inputArray[1]) - 1;
                if (answer[0] < 0 || answer[0] >= ocean.n || answer[1] < 0 || answer[1] >= ocean.m) {
                    System.out.print("Введите индексы не выходящие за границы поля. ");
                    throw new NumberFormatException();
                }
                if (ocean.field[answer[0]][answer[1]] < 0) {
                    System.out.print("В эту клетку уже был произведен выстрел. ");
                    throw new NumberFormatException();
                }

                // Запрос режима выстрела торпедой
                if (totalTorpedoes > 0) {
                    System.out.print("Хотите использовать режим выстрела торпедой? (Y/N): ");
                    String torpedoInput = Constants.console.nextLine().toLowerCase().strip();
                    if (Objects.equals(torpedoInput, "y") || Objects.equals(torpedoInput, "yes")) {
                        isTorpedoShot = true;
                    }
                }

                return answer;
            } catch (NumberFormatException | ArrayIndexOutOfBoundsException exception) {
                System.out.println("Некорректный ввод.");
            }
            System.out.println();
        }
    }

    // Совершение выстрела.
    private void shot(int[] turn) {
        int index = ocean.field[turn[0]][turn[1]];
        if (isTorpedoShot) {
            totalTorpedoes -= 1;
        }

        if (index == Constants.empty) {
            ocean.field[turn[0]][turn[1]] = Constants.missed;
            lastShot = "Промах";
        } else if (index > 0) {
            totalHp -= 1;
            if (isSunk(turn)) {
                lastShot = "Потопил " + getNameByLength(ocean.field[turn[0]][turn[1]]);
                ocean.field[turn[0]][turn[1]] = Constants.sunk;
            } else {
                lastShot = "Попадание";
                ocean.field[turn[0]][turn[1]] = Constants.hit;
            }
        } else {
            lastShot = "По клетке был произведен повторный выстрел.";
        }
        isTorpedoShot = false;
    }

    // Проверка затонет ли корабль после выстрела.
    private boolean isSunk(int[] turn) {
        int totalHits = sunkShip(turn, false) + 1;
        int currentShip = ocean.field[turn[0]][turn[1]];
        if (isTorpedoShot || totalHits == currentShip) {
            sunkShip(turn, true);
            return true;
        }
        return false;
    }

    // Затопление корабля.
    private int sunkShip(int[] turn, boolean isSunkMode) {
        int answer = 0;
        for (int i = 0; i < Constants.planeShifts.length; i++) {
            int x = turn[0] + Constants.planeShifts[i][0];
            int y = turn[1] + Constants.planeShifts[i][1];
            while (0 <= x && x < ocean.n && 0 <= y && y < ocean.m) {
                if (!isSunkMode && ocean.field[x][y] != Constants.hit) {
                    break;
                }
                if (isSunkMode && (ocean.field[x][y] == Constants.missed || ocean.field[x][y] == Constants.empty)) {
                    break;
                }
                if (isSunkMode) {
                    ocean.field[x][y] = Constants.sunk;
                }
                x += Constants.planeShifts[i][0];
                y += Constants.planeShifts[i][1];
                answer += 1;
            }
        }
        return answer;
    }

    // Получение имени корабля по его длине.
    private String getNameByLength(int length) {
        return switch (length) {
            case 1 -> "субмарину (1 палуба)";
            case 2 -> "эсминец (2 палубы)";
            case 3 -> "крейсер (3 палубы)";
            case 4 -> "линкор (4 палубы)";
            case 5 -> "авианосец (5 палуб)";
            default -> "неизвестный корабль (" + length + " палуб)";
        };
    }
}
