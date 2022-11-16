package game;

import javafx.scene.paint.Color;
import java.util.Arrays;
import java.util.List;
import java.util.Random;

public class Data {
    // Рандом
    public static Random random = new Random();

    // Все данные поля и игры
    public static int pixels = 540;
    public static int size = 9;
    public static int cellSize = pixels / size;
    public static int halfSize = cellSize / 2;

    // Все цвета
    public static Color emptyCellColor = Color.WHITE;
    public static Color occupiedCellColor = Color.GREY;
    public static Color borderCellColor = Color.BLACK;
    public static Color defaultPieceColor = Color.web("#1a8cff");
    public static Color movingPieceColor = Color.ORANGE;
    public static Color borderPieceColor = Color.BLACK;

    // Сдвиг для спавна фигур в нужном окошке
    public static int spawnX = -285;
    public static int spawnY = 203;

    // Перевод нумерации 1-9 в координаты
    public static int[][] translator = {{0, 0},
            {-1, -1}, {0, -1}, {1, -1},
            {-1, 0},  {0, 0},  {1, 0},
            {-1, 1},  {0, 1},  {1, 1}
    };

    // Все фигуры закодированные в поле 3х3 пронумерованном от 1 до 9 (начиная с левого верхнего угла)
    public static List<List<Integer>> pieces = Arrays.asList(
            // J
            Arrays.asList(2, 1, 4, 7),
            Arrays.asList(4, 7, 8, 9),
            Arrays.asList(3, 6, 9, 8),
            Arrays.asList(4, 5, 6, 9),

            // L
            Arrays.asList(2, 3, 6, 9),
            Arrays.asList(4, 5, 6, 3),
            Arrays.asList(1, 4, 7, 8),
            Arrays.asList(7, 4, 5, 6),

            // Z
            Arrays.asList(1, 4, 5, 8),
            Arrays.asList(7, 8, 5, 6),
            Arrays.asList(7, 4, 5, 2),
            Arrays.asList(4, 5, 8, 9),

            // Большая L
            Arrays.asList(7, 8, 9, 6, 3),
            Arrays.asList(1, 4, 7, 8, 9),
            Arrays.asList(7, 4, 1, 2, 3),
            Arrays.asList(1, 2, 3, 6, 9),

            // T
            Arrays.asList(2, 5, 7, 8, 9),
            Arrays.asList(1, 2, 3, 5, 8),
            Arrays.asList(1, 4, 7, 5, 6),
            Arrays.asList(4, 5, 3, 6, 9),

            // Палочки и точка
            Arrays.asList(4, 5, 6),
            Arrays.asList(2, 5, 8),
            List.of(5),

            // Уголки
            Arrays.asList(1, 2, 4),
            Arrays.asList(2, 3, 6),
            Arrays.asList(8, 9, 6),
            Arrays.asList(4, 7, 8),

            // Маленькая T
            Arrays.asList(1, 4, 7, 5),
            Arrays.asList(1, 2, 3, 5),
            Arrays.asList(3, 6, 9, 5),
            Arrays.asList(5, 7, 8, 9)
    );
}
