using System;

public static class TemperatureCalculator
{
    // Main методы
    public static double FromCelsiusToFahrenheit(double celsiusTemperature)
    {
        if (celsiusTemperature < -273.15)
            throw new ArgumentException("Incorrect input");
        return celsiusTemperature * 1.8 + 32;
    }

    public static double FromFahrenheitToKelvin(double fahrenheitTemperature)
    {
        if (fahrenheitTemperature < -459.67)
            throw new ArgumentException("Incorrect input");
        return (fahrenheitTemperature - 32) * (5.0 / 9.0) + 273.15;
    }

    public static double FromKelvinToCelsius(double kelvinTemperature)
    {
        if (kelvinTemperature < 0)
            throw new ArgumentException("Incorrect input");
        return kelvinTemperature - 273.15;
    }


    // Зациклинные методы
    public static double FromCelsiusToKelvin(double celsiusTemperature)
    {
        return FromFahrenheitToKelvin(FromCelsiusToFahrenheit(celsiusTemperature));
    }

    public static double FromFahrenheitToCelsius(double fahrenheitTemperature)
    {
        return FromKelvinToCelsius(FromFahrenheitToKelvin(fahrenheitTemperature));
    }

    public static double FromKelvinToFahrenheit(double kelvinTemperature)
    {
        return FromCelsiusToFahrenheit(FromKelvinToCelsius(kelvinTemperature));
    }

}
