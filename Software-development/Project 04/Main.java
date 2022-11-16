package ru.hse.homework4;

import ru.hse.homework4.Exceptions.NotSupportedException;

import java.lang.reflect.InvocationTargetException;


public class Main {
    /**
     * Основной метод программы
     * @param args аргументы командной строки
     */
    public static void main(String[] args) throws NotSupportedException, IllegalAccessException, NoSuchMethodException, InvocationTargetException, InstantiationException, NoSuchFieldException, ClassNotFoundException {
        Mapper mapper = new Mapper();
        Comment comment = new Comment(false);
        String mapped_comment = mapper.writeToString(comment);
        mapper.readFromString(Comment.class, mapped_comment);
    }
}
