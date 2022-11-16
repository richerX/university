package battleship;

import java.util.Scanner;

public class Constants {
    static Scanner console = new Scanner(System.in);

    static boolean adminPrint = false;

    static char[] chars = new char[] {
            ' ',  // nothing
            'o',  // miss
            '+',  // hit
            'x'}; // sunk

    // Не занимать числа в пределах от 1 до 5
    static int empty = 0;
    static int missed = -1;
    static int hit = -2;
    static int sunk = -3;

    static int paramsLength = 8;
    static boolean returnClassic = false;
    static int[] classicInput = new int[] {10, 10, 5, 4, 3, 2, 1, 10};

    // divider - блоки какого размера надо отделять при выводе поля
    static int divider = 5;
    static int numberOfFillFieldTries = 100000;

    static int[] lineShifts = new int[] {-1, 0, 1};
    static int[][] planeShifts = {{1, 0}, {0, 1}, {-1, 0}, {0, -1}};
    static boolean[] booleans = new boolean[] {false, true};
}
