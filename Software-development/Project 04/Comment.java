package ru.hse.homework4;

import ru.hse.homework4.Annotations.DateFormat;
import ru.hse.homework4.Annotations.Exported;
import ru.hse.homework4.Annotations.Ignored;
import ru.hse.homework4.Annotations.PropertyName;
import ru.hse.homework4.Enums.NullHandling;
import ru.hse.homework4.Enums.UnknownPropertiesPolicy;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;
import java.util.Set;

@SuppressWarnings({"unused"})
enum Color{
    RED,
    GREEN,
    BLUE
}

@SuppressWarnings({"unused"})
@Exported(nullHandling = NullHandling.EXCLUDE, unknownPropertiesPolicy = UnknownPropertiesPolicy.IGNORE)
class InnerComment {
    private int number;
    private List<Integer> nullList;

    public InnerComment() {
    }

    public InnerComment(boolean param) {
        number = 10;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }
}

@SuppressWarnings({"unused"})
@Exported(nullHandling = NullHandling.INCLUDE)
public class Comment {
    private boolean resolved;
    private String comment;
    private InnerComment innerComment;
    private List<List<Integer>> list;
    private Set<Boolean> set;
    private Color color;
    private LocalDate localDate;
    private LocalTime localTime;
    private LocalDateTime localDateTime;
    @Ignored
    private String author;
    @PropertyName("paramPropertyName")
    private boolean paramRealName;
    @DateFormat("dd/MM/yyyy")
    private LocalDate formattedDate;
    private Integer nullProperty;

    public Comment() {
    }

    public Comment(boolean param) {
        resolved = param;
        comment = "Great Book!";
        innerComment = new InnerComment(true);
        list = List.of(List.of(1, 2), List.of(3, 4));
        set = Set.of(true);
        color = Color.GREEN;
        localDate = LocalDate.of(2022, 2, 22);
        localTime = LocalTime.of(10, 15);
        localDateTime = LocalDateTime.of(localDate, localTime);

        author = "John";
        paramRealName = true;
        formattedDate = LocalDate.of(2022, 1, 1);

        nullProperty = null;
    }

    public boolean isResolved() {
        return resolved;
    }

    public void setResolved(boolean resolved) {
        this.resolved = resolved;
    }

    public String getComment() {
        return comment;
    }

    public void setComment(String comment) {
        this.comment = comment;
    }

    public InnerComment getInnerComment() {
        return innerComment;
    }

    public void setInnerComment(InnerComment innerComment) {
        this.innerComment = innerComment;
    }

    public List<List<Integer>> getList() {
        return list;
    }

    public void setList(List<List<Integer>> list) {
        this.list = list;
    }

    public Set<Boolean> getSet() {
        return set;
    }

    public void setSet(Set<Boolean> set) {
        this.set = set;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }

    public LocalDate getLocalDate() {
        return localDate;
    }

    public void setLocalDate(LocalDate localDate) {
        this.localDate = localDate;
    }

    public LocalTime getLocalTime() {
        return localTime;
    }

    public void setLocalTime(LocalTime localTime) {
        this.localTime = localTime;
    }

    public LocalDateTime getLocalDateTime() {
        return localDateTime;
    }

    public void setLocalDateTime(LocalDateTime localDateTime) {
        this.localDateTime = localDateTime;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public boolean isParamRealName() {
        return paramRealName;
    }

    public void setParamRealName(boolean paramRealName) {
        this.paramRealName = paramRealName;
    }

    public LocalDate getFormattedDate() {
        return formattedDate;
    }

    public void setFormattedDate(LocalDate formattedDate) {
        this.formattedDate = formattedDate;
    }

    public Integer getNullProperty() {
        return nullProperty;
    }

    public void setNullProperty(Integer nullProperty) {
        this.nullProperty = nullProperty;
    }
}
