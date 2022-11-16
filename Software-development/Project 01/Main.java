package battleship;

import java.util.Objects;

public class Main {
    /**
     * Основной метод программы.
     * @param args параметры запуска
     */
    public static void main(String[] args) {
        Ocean ocean;
        while(true) {
            Input.clearConsole();
            ocean = new Ocean(Input.input(args));
            if (ocean.fill()) {
                Game game = new Game(ocean);
                game.run();
                System.out.print("Спасибо за игру! Хочешь сыграть еще? (Y/N)? ");
            } else {
                System.out.print("Не получилось заполнить поле кораблями. Попробуем еще раз? (Y/N)?");
            }
            String input = Constants.console.nextLine().toLowerCase().strip();
            if (Objects.equals(input, "n") || Objects.equals(input, "no")) {
                break;
            }
        }
        System.out.println("До свидания!");
    }
}
