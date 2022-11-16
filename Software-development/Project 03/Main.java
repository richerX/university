package Game;

import java.util.ArrayList;

public class Main {
    private static Croupier croupier;
    static ArrayList<Player> players = new ArrayList<>();
    static ArrayList<Sharper> sharpers = new ArrayList<>();

    /**
     * Запрашивает у пользователя число, пока не будет введено корректное
     * @param message сообщение пользователю
     * @param minimum минимальное число
     * @return введенное корректное число
     */
    static int readInt(String message, int minimum) {
        String input;
        while (true) {
            System.out.print(message);
            input = Constants.console.nextLine();
            try {
                int number = Integer.parseInt(input);
                if (number < minimum) {
                    throw new NumberFormatException();
                }
                return number;
            } catch (NumberFormatException exception) {
                System.out.println("Некорректный ввод.");
                System.out.println();
            }
        }
    }

    /**
     * Создание инстансов каждого игрока
     * @param playersCount кол-во честных игроков
     * @param sharpersCount кол-во шулеров
     */
    static void initializePlayers(int playersCount, int sharpersCount) {
        int id = 0;
        for (int i = 0; i < playersCount; i++) {
            players.add(new Player(croupier, id));
            id += 1;
        }
        for (int i = 0; i < sharpersCount; i++) {
            sharpers.add(new Sharper(croupier, id));
            id += 1;
        }
    }

    /**
     * Создание потоков для каджого игрока
     * @throws InterruptedException исключение thread.join()
     */
    static void startGame() throws InterruptedException {
        ArrayList<Thread> threads = new ArrayList<>();
        for (Player player : players) {
            threads.add(new Thread(player));
        }
        for (Player sharper : sharpers) {
            threads.add(new Thread(sharper));
        }
        for (Thread thread: threads) {
            thread.start();
        }
        for (Thread thread: threads) {
            thread.join();
        }
    }

    /**
     * Вывод финальных результатов
     */
    static void displayResults() {
        ArrayList<Player> all = new ArrayList<>(players);
        all.addAll(sharpers);
        System.out.println();

        int maximum = -1;
        ArrayList<Player> champions = new ArrayList<>();
        for (Player player : all) {
            System.out.println(player + " with " + player.points + " points!");
            if (player.points > maximum) {
                maximum = player.points;
                champions.clear();
                champions.add(player);
            } else if (player.points == maximum) {
                champions.add(player);
            }
        }

        System.out.println();
        if (champions.size() == 1) {
            System.out.println("Победитель: " + champions.get(0));
        } else {
            System.out.println("Победители: " + champions.get(0));
            for (int i = 1; i < champions.size(); i++) {
                System.out.println("            " + champions.get(i));
            }
        }
    }

    /**
     * Основной метод программы
     * @param args аргументы команднйо строки
     * @throws InterruptedException исключение thread.join()
     */
    public static void main(String[] args) throws InterruptedException {
        int playersCount = readInt("Введите количество честных игроков: ", 1);
        int sharpersCount = readInt("Введите количество шулеров: ", 0);
        croupier = new Croupier(playersCount + sharpersCount);
        initializePlayers(playersCount, sharpersCount);
        startGame();
        displayResults();
    }
}
