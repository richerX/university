package ru.hse.homework4;

import ru.hse.homework4.Annotations.Exported;
import ru.hse.homework4.Enums.NullHandling;
import ru.hse.homework4.Enums.UnknownPropertiesPolicy;

@SuppressWarnings({"unused"})
@Exported(nullHandling = NullHandling.EXCLUDE, unknownPropertiesPolicy = UnknownPropertiesPolicy.FAIL)
class Car {
    private String brand;

    public Car() {
    }
}

class Computer {
    public Computer() {
    }
}

@SuppressWarnings({"unused"})
@Exported
class Apple {
    public Apple(int weight) {
    }
}

@SuppressWarnings({"unused"})
@Exported
class GreenApple extends Apple {
    public GreenApple() {
        super(100);
    }

    public GreenApple(int weight) {
        super(weight);
    }
}
