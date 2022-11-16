package battleship;

public class Input {
    private static boolean firstTry = true;

    /**
     * Ввод параметров игры.
     * @param args параметры при запуске программы
     * @return
     */
    public static int[] input(String[] args) {
        if (firstTry) {
            firstTry = false;
            try {
                return Input.argsInput(args);
            } catch (Exception exception) {
                System.out.println("Некорректные аргументы командной строки. Введите аргументы вручную.");
                System.out.println();
            }
        }
        System.out.println(" > ПАРАМЕТРЫ ИГРЫ < ");
        System.out.println();
        return Input.consoleInput();
    }

    /**
     * Очистка консоли для приятного вывода.
     */
    public static void clearConsole() {
        System.out.println();
        try {
            new ProcessBuilder("cmd", "/c", "cls").inheritIO().start().waitFor();
        } catch (Exception exception) {
            System.out.println("Возникла ошибка при попытке очистить консоль");
            System.out.println();
        }
    }

    // Ввод из консоли.
    private static int[] consoleInput() {
        if (Constants.returnClassic) {
            return Constants.classicInput;
        }
        int[] answer = new int[Constants.paramsLength];
        answer[0] = readInt("Введите длину 'океана': ");
        answer[1] = readInt("Введите ширину 'океана': ");
        answer[2] = readInt("Введите количество однопалубных кораблей: ");
        answer[3] = readInt("Введите количество двупалубных кораблей: ");
        answer[4] = readInt("Введите количество трехпалубных кораблей: ");
        answer[5] = readInt("Введите количество четырехпалубных кораблей: ");
        answer[6] = readInt("Введите количество пятипалубных кораблей: ");
        answer[7] = readInt("Введите количество торпед: ");
        System.out.println();
        return answer;
    }

    // Ввод параметрами при запуске.
    private static int[] argsInput(String[] args) {
        int[] answer = new int[Constants.paramsLength];
        for (int i = 0; i < Constants.paramsLength; i++) {
            answer[i] = Integer.parseInt(args[i]);
            if (answer[i] < 0) {
                throw new NumberFormatException();
            }
        }
        return answer;
    }

    // Считать число из консоли.
    private static int readInt(String message) {
        String input;
        while (true) {
            System.out.print(message);
            input = Constants.console.nextLine();
            try {
                int number = Integer.parseInt(input);
                if (number < 0) {
                    throw new NumberFormatException();
                }
                return number;
            } catch (NumberFormatException exception) {
                System.out.println("Некорректный ввод.");
                System.out.println();
            }
        }
    }
}
