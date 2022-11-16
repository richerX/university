package Game;

public class Player implements Runnable {
    int id;
    int points;
    Croupier croupier;

    /**
     * Конструктор класса
     * @param croupier ссылка на "колоду"
     * @param id идентификатор пользователя
     */
    public Player(Croupier croupier, int id) {
        this.id = id;
        this.croupier = croupier;
        this.points = 0;
    }

    /**
     * Изменение баллов
     * @param change баллы
     */
    public void changePoints(int change) {
        points += change;
    }

    /**
     * "Усыпить" поток
     * @param minimum минимальная длительность "сна"
     * @param maximum максимальная длительность "сна"
     */
    public void sleep(int minimum, int maximum) {
        int sleep = Constants.random.nextInt(maximum - minimum + 1) + minimum;
        // System.out.println(this + " slept for " + sleep + " milliseconds!");
        try {
            Thread.sleep(sleep);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /**
     * Перегрузка метода перевода в строку
     * @return текстовая интерпретация объекта
     */
    @Override
    public String toString() {
        return "Player  [id = " + id + ", points = " + points + "]";
    }

    /**
     * Перегрузка метода исполнения потока
     */
    @Override
    public void run() {
        croupier.signalReady();
        while (croupier.getStatus() != Constants.responseFinish) {
            croupier.pickCard(this, false);
            sleep(100, 200);
        }
    }
}
