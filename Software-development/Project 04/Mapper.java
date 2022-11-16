package ru.hse.homework4;

import ru.hse.homework4.Annotations.DateFormat;
import ru.hse.homework4.Annotations.Exported;
import ru.hse.homework4.Annotations.Ignored;
import ru.hse.homework4.Annotations.PropertyName;
import ru.hse.homework4.Enums.NullHandling;
import ru.hse.homework4.Enums.UnknownPropertiesPolicy;
import ru.hse.homework4.Exceptions.NotSupportedException;
import ru.hse.homework4.Interfaces.Mapperable;

import java.io.*;
import java.lang.reflect.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.*;

public class Mapper implements Mapperable {

    /**
     * Преобразуют строку сохраненную mapper'ом в экземпляр класса
     * @param clazz класс, сохранённый экземпляр которого находится в {@code input}
     * @param string сохраненная строка
     * @param <T> типизирующей параметр
     * @return экземпляр класса
     */
    @Override
    public <T> T readFromString(Class<T> clazz, String string) throws IllegalAccessException, NoSuchMethodException, InvocationTargetException, InstantiationException, ClassNotFoundException, NoSuchFieldException, NotSupportedException {
        T object = clazz.getConstructor().newInstance();

        Map<String, String> map = new HashMap<>();
        if (Constants.debug) {
            System.out.println("New readFromString call with string = " + string + '\n');
        }
        for (String elem : partitionListString(string, '{', '}')) {
            map.put(elem.substring(1, elem.indexOf("=")), elem.substring(elem.indexOf("=") + 1, elem.length() - 1));
        }

        boolean failUnknown = clazz.getAnnotation(Exported.class).unknownPropertiesPolicy() == UnknownPropertiesPolicy.FAIL;
        for (Field field : object.getClass().getDeclaredFields()) {
            field.setAccessible(true);
            if (correctField(field)) {
                if (map.get(getFieldName(field)) != null) {
                field.set(object, toObject(field, field.getType(), field.getGenericType().toString(), map.get(getFieldName(field))));
                } else if (failUnknown) {
                    throw new NotSupportedException("При восставлении встретил неизвестное поле");
                }
            }
        }

        return object;
    }

    /**
     * Преобразуют строку сохраненную mapper'ом в экземпляр класса
     * @param clazz класс, сохранённый экземпляр которого находится в inputStream
     * @param inputStream поток ввода, содержащий строку в UTF_8 кодировке
     * @param <T> типизирующей параметр
     * @return экземпляр класса
     */
    @Override
    public <T> T read(Class<T> clazz, InputStream inputStream) throws IOException, IllegalAccessException, NoSuchMethodException, InvocationTargetException, InstantiationException, ClassNotFoundException, NoSuchFieldException, NotSupportedException {
        return readFromString(clazz, new String(inputStream.readAllBytes(), StandardCharsets.UTF_8));
    }

    /**
     * Преобразуют строку сохраненную mapper'ом в экземпляр класса
     * @param clazz класс, сохранённый экземпляр которого находится в файле
     * @param file файл, содержимое которого - строковое представление экземпляра {@code clazz} в UTF_8 кодировке
     * @param <T> типизирующей параметр
     * @return экземпляр класса
     */
    @Override
    public <T> T read(Class<T> clazz, File file) throws IOException, IllegalAccessException, NoSuchMethodException, InvocationTargetException, InstantiationException, ClassNotFoundException, NoSuchFieldException, NotSupportedException {
        return readFromString(clazz, new String(Files.readAllBytes(Path.of(file.getAbsolutePath()))));
    }

    /**
     * Преобразуют экземлпяр класса в строку дял сохранения
     * Формат строки очень похож на JSON, только я использую больше {}
     * Каждый объект/поле/свойство выделяется в отдельные {}
     * То есть при конвератции объекта в строку мы получим
     * {{field_1 = value_1}, {field_2 = value_2}, {field_3 = value_3}}
     * При этом value само можеть быть обернуто в {}, если это @Exported объект
     * Или в [], если это массив (List) или множество (Set)
     * Такие "сложные" объекты сохраняются и восстанавливаются рекурсивно
     * @param object объект для сохранения
     * @return строку
     */
    @Override
    public String writeToString(Object object) throws NotSupportedException, IllegalAccessException {
        if (!objectIsExported(object)) {
            throw new NotSupportedException("Данный класс не является @Exported");
        }
        if (!hasEmptyConstructor(object.getClass())) {
            throw new NotSupportedException("У данного класса отсутсвует public конструктор без параметров");
        }
        if (!(object.getClass().getSuperclass() == Object.class)) {
            throw new NotSupportedException("Данный класс не наследуется непосредственно от Object");
        }

        StringBuilder string = new StringBuilder();
        string.append("{");
        boolean excludeNull = object.getClass().getAnnotation(Exported.class).nullHandling() == NullHandling.EXCLUDE;
        for (Field field : object.getClass().getDeclaredFields()) {
            if (correctField(field)) {
                field.setAccessible(true);
                if (field.get(object) == null && excludeNull) {
                    continue;
                }
                string.append("{").append(getFieldName(field)).append("=").append(toString(object, field)).append("}, ");
            }
        }
        string.setLength(Math.max(1, string.length() - 2));
        string.append("}");
        return string.toString();
    }

    /**
     * Преобразуют экземлпяр класса в строку дял сохранения и записывает в stream
     * @param object объект для сохранения
     * @param outputStream поток данных для записи
     */
    @Override
    public void write(Object object, OutputStream outputStream) throws IOException, NotSupportedException, IllegalAccessException {
        outputStream.write(writeToString(object).getBytes(StandardCharsets.UTF_8));
    }

    /**
     * Преобразуют экземлпяр класса в строку дял сохранения и записывает в file
     * @param object объект для сохранения
     * @param file файл для записи
     */
    @Override
    public void write(Object object, File file) throws IOException, NotSupportedException, IllegalAccessException {
        Files.writeString(Path.of(file.getAbsolutePath()), writeToString(object), StandardCharsets.UTF_8);
    }

    // Проверяет, что класс является @Exported
    private boolean objectIsExported(Object object) {
        return object.getClass().isAnnotationPresent(Exported.class);
    }

    // Проверяет, что поле является корректным для сохранения
    private boolean correctField(Field field) {
        return !(field.isAnnotationPresent(Ignored.class) || field.isSynthetic() || Modifier.isStatic(field.getModifiers()));
    }

    // Имя поля для сохранения
    private String getFieldName(Field field) {
        if (field.isAnnotationPresent(PropertyName.class)) {
            return field.getAnnotation(PropertyName.class).value();
        }
        return field.getName();
    }

    // Проверка на наличие пустого констурктора
    private boolean hasEmptyConstructor(Class<?> clazz) {
        for (Constructor<?> constructor : clazz.getConstructors()) {
            if (constructor.getParameterCount() == 0) {
                return true;
            }
        }
        return false;
    }

    // Рекурсиваное преобразвоание объектв в стркоу для сохранения
    private String toString(Object object, Field field) throws IllegalAccessException, NotSupportedException {
        Class<?> clazz = field.getType();

        if (field.get(object) == null) {
            return "null";
        }

        if (String.class == clazz) {
            return '"' + field.get(object).toString() + '"';
        }

        if (clazz.isAnnotationPresent(Exported.class)) {
            return writeToString(field.get(object));
        }

        if (LocalDate.class == clazz || LocalTime.class == clazz || LocalDateTime.class == clazz) {
            if (field.isAnnotationPresent(DateFormat.class)) {
                DateTimeFormatter formatter = DateTimeFormatter.ofPattern(field.getAnnotation(DateFormat.class).value());
                if (LocalDate.class == clazz) {
                    return ((LocalDate) field.get(object)).format(formatter);
                } else if (LocalTime.class == clazz) {
                    return ((LocalTime) field.get(object)).format(formatter);
                }
                return ((LocalDateTime) field.get(object)).format(formatter);
            }
        }

        return field.get(object).toString();
    }

    // Рекурсивное преобразование строки в объект класса
    private Object toObject(Field field, Class<?> clazz, String generic, String value) throws ClassNotFoundException, NoSuchFieldException, InvocationTargetException, IllegalAccessException, NoSuchMethodException, InstantiationException, NotSupportedException {
        if (Constants.debug) {
            System.out.println("Function toObject call with parameters:");
            System.out.println("Class = " + clazz.getName());
            System.out.println("Gener = " + generic);
            System.out.println("Value = " + value);
            System.out.println();
        }

        // 0. Значение отсутствует
        if (Objects.equals(value, "null")) {
            return null;
        }

        // 1. Примитивы
        if (Boolean.class == clazz || Boolean.TYPE == clazz) {
            return Boolean.parseBoolean(value);
        }
        if (Byte.class == clazz || Byte.TYPE == clazz) {
            return Byte.parseByte(value);
        }
        if (Short.class == clazz || Short.TYPE == clazz) {
            return Short.parseShort(value);
        }
        if (Integer.class == clazz || Integer.TYPE == clazz) {
            return Integer.parseInt(value);
        }
        if (Long.class == clazz || Long.TYPE == clazz) {
            return Long.parseLong(value);
        }
        if (Float.class == clazz || Float.TYPE == clazz) {
            return Float.parseFloat(value);
        }
        if (Double.class == clazz || Double.TYPE == clazz) {
            return Double.parseDouble(value);
        }

        // 2. String
        if (String.class == clazz) {
            return value.substring(1, value.length() - 1);
        }

        // 3. @Exported класс
        if (clazz.isAnnotationPresent(Exported.class)) {
            return readFromString(clazz, value);
        }

        // 4-5. List, Set
        if (List.class == clazz || Set.class == clazz) {
            Collection<Object> answer = List.class == clazz ? new ArrayList<>() : new HashSet<>();
            String next_generic = generic.substring(generic.indexOf("<") + 1, generic.length() - 1);
            String next_class = next_generic.contains("<") ? next_generic.substring(0, Math.min(generic.indexOf("<"), generic.indexOf(">"))) : next_generic;
            for (String elem : partitionListString(value, '[', ']')) {
                answer.add(toObject(null, Class.forName(next_class), next_generic, elem.strip()));
            }
            return answer;
        }

        // 6. Enum
        if (clazz.isEnum()) {
            for (var elem : clazz.getEnumConstants()) {
                if (Objects.equals(elem.toString(), value)) {
                    return elem;
                }
            }
            return null;
        }

        // 7-9. Date and Time
        if (LocalDate.class == clazz || LocalTime.class == clazz || LocalDateTime.class == clazz) {
            if (field != null && field.isAnnotationPresent(DateFormat.class)) {
                DateTimeFormatter formatter = DateTimeFormatter.ofPattern(field.getAnnotation(DateFormat.class).value());
                if (LocalDate.class == clazz) {
                    return LocalDate.parse(value, formatter);
                } else if (LocalTime.class == clazz) {
                    return LocalTime.parse(value, formatter);
                }
                return LocalDateTime.parse(value, formatter);
            }

            if (LocalDate.class == clazz) {
                return LocalDate.parse(value);
            } else if (LocalTime.class == clazz) {
                return LocalTime.parse(value);
            }
            return LocalDateTime.parse(value);
        }

        return value;
    }

    // Разбиение строки со скобками на части на верхнем уровне
    private ArrayList<String> partitionListString(String string, char beginChar, char end_char) {
        ArrayList<String> answer = new ArrayList<>();
        int begin = 0, depth = 0;
        for (int i = 1; i < string.length() - 1; ++i) {
            if (string.charAt(i) == beginChar) {
                depth++;
            } else if (string.charAt(i) == end_char) {
                depth--;
            } else if (string.charAt(i) == ',' && depth == 0) {
                answer.add(string.substring(begin + 1, i).trim());
                begin = i;
            }
        }
        String lastString = string.substring(begin + 1, string.length() - 1).trim();
        if (lastString.length() > 0) {
            answer.add(lastString);
        }
        return answer;
    }
}
