package game;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;

public class Application extends javafx.application.Application {
    /**
     * Основной метод запуска JavaFX
     * @param stage сцена
     * @throws IOException исключение
     */
    @Override
    public void start(Stage stage) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(Application.class.getResource("hello-view.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 1300, 700);
        stage.setTitle("Jigsaw");
        stage.setScene(scene);
        stage.setResizable(false);
        stage.show();
    }

    /**
     * Основной метод программы
     * @param args аргументы командной строки
     */
    public static void main(String[] args) {
        launch();
    }
}