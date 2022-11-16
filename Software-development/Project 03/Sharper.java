package Game;

public class Sharper extends Player implements Runnable {
    /**
     * Конструктор класса
     * @param croupier ссылка на "колоду"
     * @param id идентификатор пользователя
     */
    public Sharper(Croupier croupier, int id) {
        super(croupier, id);
    }

    /**
     * Перегрузка метода перевода в строку
     * @return текстовая интерпретация объекта
     */
    @Override
    public String toString() {
        return "Sharper [id = " + id + ", points = " + points + "]";
    }

    /**
     * Перегрузка метода исполнения потока
     */
    @Override
    public void run() {
        croupier.signalReady();
        while (croupier.getStatus() != Constants.responseFinish) {
            if (Constants.random.nextInt(100) + 1 <= Constants.probabilityToSteal) {
                croupier.pickCard(this, true);
                sleep(180, 300);
            } else {
                croupier.pickCard(this, false);
                sleep(100, 200);
            }
        }
    }
}
