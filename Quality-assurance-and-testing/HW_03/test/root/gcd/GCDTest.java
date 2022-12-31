package root.gcd;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class GCDTest {
    GCD gcd = new GCD();

    @Test
    // положительные значения аргументов
    void testCondition_1() {
        assertEquals(3, gcd.gcd(6, 21));
        assertEquals(5, gcd.gcd(15, 20));
    }

    @Test
    // отрицательное значение первого, второго, обоих аргументов
    void testCondition_2() {
        assertEquals(5, gcd.gcd(-20, 15));
        assertEquals(5, gcd.gcd(20, -15));
        assertEquals(5, gcd.gcd(-20, -15));
    }

    @Test
    // нулевое значение первого, второго, обоих аргументов
    void testCondition_3() {
        assertEquals(7, gcd.gcd(0, 7));
        assertEquals(13, gcd.gcd(13, 0));
        assertEquals(9, gcd.gcd(-9, 0));
        assertEquals(0, gcd.gcd(0, 0));
    }

    @Test
    // неединичные взаимно простые аргументы
    void testCondition_4() {
        assertEquals(1, gcd.gcd(3, 14));
        assertEquals(1, gcd.gcd(100, 93));
        assertEquals(1, gcd.gcd(7, 15));
    }

    @Test
    // равные значения аргументов
    void testCondition_5() {
        assertEquals(10, gcd.gcd(10, 10));
        assertEquals(7, gcd.gcd(7, 7));
        assertEquals(15, gcd.gcd(-15, -15));
    }

    @Test
    // неравные значения аргументов, при которых один делит второй
    void testCondition_6() {
        assertEquals(14, gcd.gcd(14, 28));
        assertEquals(15, gcd.gcd(30, 15));
        assertEquals(10, gcd.gcd(-10, 20));
        assertEquals(25, gcd.gcd(100, -25));
    }

    @Test
    // неравные значения аргументов, дающие неединичный наибольший общий делитель
    void testCondition_7() {
        assertEquals(21, gcd.gcd(105, 63));
        assertEquals(4, gcd.gcd(24, 76));
        assertEquals(5, gcd.gcd(1000, 55));
    }

    @Test
    // граничные значения аргументов
    void testCondition_8() {
        long correct = (long) 214748364 * 10 + 8;
        assertEquals(correct, gcd.gcd(Integer.MIN_VALUE, Integer.MIN_VALUE));
        assertEquals(1, gcd.gcd(Integer.MIN_VALUE, Integer.MAX_VALUE));
        assertEquals(Integer.MAX_VALUE, gcd.gcd(Integer.MAX_VALUE, Integer.MAX_VALUE));
    }
}