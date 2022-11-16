package Game;

import java.util.Random;
import java.util.Scanner;

public class Constants {
    static final int workingTime = 10; // секунды
    static final int probabilityToSteal = 40; // шанс, того что шулер будет красть карту

    static final int responseWaiting = 0;
    static final int responseWorking = 1;
    static final int responseFinish = 2;

    static Scanner console = new Scanner(System.in);
    static final Random random = new Random();
}
