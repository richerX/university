package Game;

import java.util.Date;

public class Croupier {
    private long startTime;
    private int playersReady;
    private final int totalPlayers;

    /**
     * Конструктор класса
     */
    public Croupier(int totalPlayers) {
        this.totalPlayers = totalPlayers;
    }

    /**
     * Текущей статус игры
     * @return код текущего статуса
     */
    public int getStatus() {
        if (playersReady != totalPlayers) {
            return Constants.responseWaiting;
        }  else if ((new Date().getTime() - startTime) / 1000 > Constants.workingTime) {
            return Constants.responseFinish;
        }
        return Constants.responseWorking;
    }

    /**
     * Получение сигнала от игрока о готовности
     */
    public synchronized void signalReady() {
        playersReady += 1;
        if (playersReady == totalPlayers) {
            startTime = new Date().getTime();
        }
    }

    /**
     * Синхронизированный метод выполнения действия игрока
     * @param player ссылка на игрока
     * @param steal крадет ли шулер карту
     */
    public synchronized void pickCard(Player player, Boolean steal) {
        if (player instanceof Sharper && steal) {
            pickCardFromPlayer(player);
        } else {
            pickCardFromDeck(player);
        }
    }

    /**
     * Взятие карты из колоды
     * @param player ссылка на игрока
     */
    public synchronized void pickCardFromDeck(Player player) {
        int picked = Constants.random.nextInt(10) + 1;
        // System.out.println(player + " picked " + picked + " points from deck!");
        player.changePoints(picked);
    }

    /**
     * Кража карты у другого игрока
     * @param sharper ссылка на шулера
     */
    public synchronized void pickCardFromPlayer(Player sharper) {
        int stolen = Constants.random.nextInt(8) + 1;
        Player target = Main.players.get(Constants.random.nextInt(Main.players.size()));
        stolen = Math.min(stolen, target.points);
        // System.out.println(sharper + " stolen " + stolen + " points from " + target + "!");
        sharper.changePoints(stolen);
        target.changePoints(-stolen);
    }
}
