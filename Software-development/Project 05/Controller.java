package game;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ProgressBar;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Pane;
import javafx.scene.shape.Rectangle;

import java.util.List;
import java.util.Objects;

public class Controller {
    @FXML
    Pane pane;
    @FXML
    Button startStopButton;
    @FXML
    Label gameTimeLabel;
    @FXML
    Label turnsLabel;
    @FXML
    ProgressBar progressBar;

    private int turns = 0;
    private long startTime;
    private boolean timerActive = false;
    private final Rectangle[][] grid = new Rectangle[Data.size][Data.size];

    @FXML
    // Основная инициализация
    protected void initialize() {
        initializeTimer();
    }



    //
    // @FXML функции
    //

    @FXML
    // Кнопка "Начать/Завершить игру"
    protected void onStartStopButtonClick() {
        if (Objects.equals(startStopButton.getText(), "Начать игру")) {
            initializeGrid();
            spawnPiece();
            turns = 0;
            timerActive = true;
            startTime = System.currentTimeMillis();
            gameTimeLabel.setText("Время игры: 0 сек.");
            turnsLabel.setText("Кол-во ходов: " + turns);
            startStopButton.setText("Завершить игру");
        } else if (Objects.equals(startStopButton.getText(), "Завершить игру")) {
            showStatistics();
            pane.getChildren().clear();
            timerActive = false;
            progressBar.setProgress(0);
            gameTimeLabel.setText("Время игры: ");
            turnsLabel.setText("Кол-во ходов: ");
            startStopButton.setText("Начать игру");
        }
    }

    @FXML
    // Кнопка "Выйти"
    protected void onExitButtonClick() {
        System.exit(0);
    }



    //
    // Важные инциализации / спавны
    //

    // Инициализация таймера
    private void initializeTimer() {
        Thread timerThread = new Thread(() -> {
            while (true) {
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ignored) {

                }
                Platform.runLater(() -> {
                    if (timerActive) {
                        gameTimeLabel.setText("Время игры: " + getGameTime());
                    }
                });
            }
        });
        timerThread.start();
    }

    // Инициализация поля
    private void initializeGrid() {
        for (int i = 0; i < Data.pixels; i += Data.cellSize) {
            for (int j = 0; j < Data.pixels; j += Data.cellSize) {
                Rectangle cell = new Rectangle(i, j, Data.cellSize, Data.cellSize);
                grid[i / Data.cellSize][j / Data.cellSize] = cell;
                cell.setFill(Data.emptyCellColor);
                cell.setStroke(Data.borderCellColor);
                pane.getChildren().add(cell);
            }
        }
    }

    // Спавн новой фигуры
    private void spawnPiece() {
        Piece piece = new Piece(0, 0);
        List<Integer> currentPiece = Data.pieces.get(Data.random.nextInt(Data.pieces.size()));
        for (int current : currentPiece) {
            Rectangle rectangle = new Rectangle();
            rectangle.setFill(Data.defaultPieceColor);
            rectangle.setStroke(Data.borderPieceColor);
            rectangle.setHeight(Data.cellSize);
            rectangle.setWidth(Data.cellSize);
            rectangle.setOnMousePressed(event -> pressed(event, piece));
            rectangle.setOnMouseDragged(event -> dragged(event, piece));
            rectangle.setOnMouseReleased(event -> released(event, piece));
            pane.getChildren().add(rectangle);
            piece.add(Data.translator[current][0] * Data.cellSize, Data.translator[current][1] * Data.cellSize, rectangle);
        }
        piece.teleport(Data.spawnX, Data.spawnY);
    }

    // Сообщение со статистикой в конце игры
    private void showStatistics() {
        Alert statistics = new Alert(Alert.AlertType.INFORMATION);
        statistics.setTitle("Статистика игры");
        statistics.setHeaderText("Статистика игры");
        statistics.setContentText("Время игры: " + getGameTime() + "\n" + "Кол-во ходов: " + turns + "\n" +
                "Заполненность поля: " + Math.round(getProgress() * 100) + "%");
        statistics.showAndWait();
    }



    //
    // Event функции
    //

    // Взятие фигуры
    private void pressed(MouseEvent event, Piece piece) {
        piece.setColor(Data.movingPieceColor);
    }

    // Перетаскивание фигуры
    private void dragged(MouseEvent event, Piece piece) {
        piece.move(event.getX() - Data.halfSize, event.getY() - Data.halfSize);
    }

    // Отпусканеи фигуры
    private void released(MouseEvent event, Piece piece) {
        if (releaseIsOk(piece)) {
            releaseColor(piece);
            piece.dispose(pane);
            spawnPiece();
            progressBar.setProgress(getProgress());
            turns++;
            turnsLabel.setText("Кол-во ходов: " + turns);
            System.out.println("Фигура заняла клетки");
        } else {
            piece.teleport(Data.spawnX, Data.spawnY);
            piece.setColor(Data.defaultPieceColor);
            System.out.println("Невозможно занять данные клетки");
        }
    }



    //
    // Release функции
    //

    // Проверка, что фигура отпускается корректно
    private boolean releaseIsOk(Piece piece) {
        for (int i = 0; i < piece.rectangles.size(); ++i) {
            int x = getCellIndex(piece.xs.get(i));
            int y = getCellIndex(piece.ys.get(i));
            if (x < 0 || x >= Data.size || y < 0 || y >= Data.size) {
                return false;
            }
            if (grid[x][y].getFill() == Data.occupiedCellColor) {
                return false;
            }
        }
        return true;
    }

    // Закристь клетки поля при отпускании фигуры
    private void releaseColor(Piece piece) {
        for (int i = 0; i < piece.rectangles.size(); ++i) {
            grid[getCellIndex(piece.xs.get(i))][getCellIndex(piece.ys.get(i))].setFill(Data.occupiedCellColor);
        }
    }

    // Индекс ближайшей клетки
    private int getCellIndex(double coordinate) {
        return (int) Math.round(coordinate / Data.cellSize);
    }



    //
    // Статистика
    //

    // Заполненность поля
    private double getProgress() {
        double count = 0.0;
        for (int i = 0; i < Data.size; i += 1) {
            for (int j = 0; j < Data.size; j += 1) {
                if (grid[i][j].getFill() == Data.occupiedCellColor) {
                    count++;
                }
            }
        }
        return count / (Data.size * Data.size);
    }

    // Подсчет корректного времени для таймера
    private String getGameTime() {
        int seconds = (int)((System.currentTimeMillis() - startTime) / 1000);
        int minutes = seconds / 60;
        seconds -= minutes * 60;
        if (minutes == 0) {
            return seconds + " сек.";
        } else if (minutes < 10 && seconds < 10) {
            return "0" + minutes + ":" + "0" + seconds + " мин.";
        } else if (minutes < 10) {
            return "0" + minutes + ":" + seconds + " мин.";
        } else if (seconds < 10) {
            return minutes + ":" + "0" + seconds + " мин.";
        }
        return minutes + ":" + seconds + " мин.";
    }
}