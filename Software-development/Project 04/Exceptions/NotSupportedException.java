package ru.hse.homework4.Exceptions;

public class NotSupportedException extends Exception {
    public NotSupportedException() {
        super();
    }

    public NotSupportedException(String message) {
        super(message);
    }
}