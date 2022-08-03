using System.Threading;

public static class Joule
{
    public static double Pico(double value)
        => value / 1000 / 1000 / 1000 / 1000;
    
    public static double Nano(double value)
        => value / 1000 / 1000 / 1000;

    public static double Micro(double value)
        => value / 1000 / 1000;

    public static double Milli(double value)
        => value / 1000;

    public static double Killo(double value)
        => value * 1000;

    public static double Mega(double value)
        => value * 1000 * 1000;

    public static double Giga(double value)
        => value * 1000 * 1000 * 1000;

    public static double Tera(double value)
        => value * 1000 * 1000 * 1000 * 1000;
}

public static class Configuration
{
    public static void EnableSTAThread()
    {
        //enable STA Thread to use some features like Clipboard
        Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
        Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
    }
}