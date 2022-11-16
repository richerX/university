package ru.hse.homework4;

import ru.hse.homework4.Exceptions.NotSupportedException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.io.*;
import java.lang.reflect.InvocationTargetException;


class MapperTest {
    private static Mapper mapper;
    private static Comment comment;
    private static InnerComment innerComment;

    @BeforeAll
    static void beforeAll() {
        mapper = new Mapper();
        comment = new Comment(true);
        innerComment = new InnerComment(true);
    }

    @Test
    void readFromString() throws NotSupportedException, IllegalAccessException, NoSuchFieldException, ClassNotFoundException, InvocationTargetException, NoSuchMethodException, InstantiationException {
        checkUnpacked(mapper.readFromString(Comment.class, mapper.writeToString(comment))); // @UnknownPropertiesPolicy.IGNORE
        NotSupportedException thrown = Assertions.assertThrows(NotSupportedException.class, () -> mapper.readFromString(Car.class, mapper.writeToString(new Car())));
        Assertions.assertTrue(thrown.getMessage().contains("При восставлении встретил неизвестное поле")); // @UnknownPropertiesPolicy.FAIL
    }

    @Test
    void writeToString() throws NotSupportedException, IllegalAccessException {
        String target;
        NotSupportedException thrown;

        target = "{{resolved=true}, {comment=\"Great Book!\"}, {innerComment={{number=10}}}, {list=[[1, 2], [3, 4]]}, {set=[true]}, {color=GREEN}, {localDate=2022-02-22}, {localTime=10:15}, {localDateTime=2022-02-22T10:15}, {paramPropertyName=true}, {formattedDate=01/01/2022}, {nullProperty=null}}";
        Assertions.assertEquals(target, mapper.writeToString(comment)); // NullHandling.INCLUDE

        target = "{{number=10}}";
        Assertions.assertEquals(target, mapper.writeToString(innerComment)); // NullHandling.EXCLUDE

        thrown = Assertions.assertThrows(NotSupportedException.class, () -> mapper.writeToString(new Computer()));
        Assertions.assertTrue(thrown.getMessage().contains("Данный класс не является @Exported")); // not @Exported

        thrown = Assertions.assertThrows(NotSupportedException.class, () -> mapper.writeToString(new Apple(100)));
        Assertions.assertTrue(thrown.getMessage().contains("У данного класса отсутсвует public конструктор без параметров")); // нет public конструктора

        thrown = Assertions.assertThrows(NotSupportedException.class, () -> mapper.writeToString(new GreenApple()));
        Assertions.assertTrue(thrown.getMessage().contains("Данный класс не наследуется непосредственно от Object")); // не наследуется от Object
    }

    @Test
    void streamTest() throws IOException, NotSupportedException, IllegalAccessException, NoSuchFieldException, ClassNotFoundException, InvocationTargetException, NoSuchMethodException, InstantiationException {
        File file = new File("stream");
        OutputStream output = new FileOutputStream(file);
        InputStream input = new FileInputStream(file);
        mapper.write(comment, output);
        checkUnpacked(mapper.read(Comment.class, input)); // Stream test
    }

    @Test
    void fileTest() throws IOException, NotSupportedException, IllegalAccessException, NoSuchFieldException, ClassNotFoundException, InvocationTargetException, NoSuchMethodException, InstantiationException {
        File file = new File("file");
        mapper.write(comment, file);
        checkUnpacked(mapper.read(Comment.class, file)); // File test
    }

    void checkUnpacked(Comment unpacked) {
        Assertions.assertEquals(unpacked.isResolved(), comment.isResolved()); // boolean
        Assertions.assertEquals(unpacked.getComment(), comment.getComment()); // String
        Assertions.assertEquals(unpacked.getInnerComment().getNumber(), comment.getInnerComment().getNumber()); // @Exported
        Assertions.assertEquals(unpacked.getList(), comment.getList()); // List
        Assertions.assertEquals(unpacked.getSet(), comment.getSet()); // Set
        Assertions.assertEquals(unpacked.getColor(), comment.getColor()); // Enum
        Assertions.assertEquals(unpacked.getLocalDate(), comment.getLocalDate()); // LocalDate
        Assertions.assertEquals(unpacked.getLocalTime(), comment.getLocalTime()); // LocalTime
        Assertions.assertEquals(unpacked.getLocalDateTime(), comment.getLocalDateTime()); // LocalDateTime

        Assertions.assertNull(unpacked.getAuthor()); // @Ignore
        Assertions.assertEquals(unpacked.isParamRealName(), comment.isParamRealName()); // @PropertyName
        Assertions.assertEquals(unpacked.getFormattedDate().toString(), "2022-01-01"); // @DateFormat
        Assertions.assertNull(unpacked.getNullProperty()); // Null
    }
}
