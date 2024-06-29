using System;
using System.Collections.Generic;

public class Rectangle
{
    private double sideA;
    private double sideB;
    private static readonly double sqrtOfTwo = Math.Sqrt(2);
    private static readonly Dictionary<char, decimal> sheetHeight0 = new Dictionary<char, decimal>
    {
        ['A'] = 1189,
        ['B'] = 1414,
        ['C'] = 1297
    };

    public Rectangle(double sideA, double sideB)
    {
        SideA = sideA;
        SideB = sideB;
    }

    public double SideA
    {
        get => sideA;
        set
        {
            if (double.IsNaN(value) || value < 0)
                throw new ArgumentException("Side A musi byc skonczona, nieujemna liczba.");
            sideA = value;
        }
    }

    public double SideB
    {
        get => sideB;
        set
        {
            if (double.IsNaN(value) || value < 0)
                throw new ArgumentException("Side B musi byc skonczona, nieujemna liczba.");
            sideB = value;
        }
    }

    public static Rectangle PaperSheet(string format)
    {
        char series = format[0];
        if (!sheetHeight0.ContainsKey(series))
            throw new ArgumentException("Nieprawidlowa seria arkusza.");

        byte i = byte.Parse(format.Substring(1));
        decimal height = sheetHeight0[series];
        double heightMM = (double)height / Math.Pow(sqrtOfTwo, i);
        double widthMM = heightMM / sqrtOfTwo;

        return new Rectangle(heightMM, widthMM);
    }

    public override string ToString()
    {
        return $"Bok A: {sideA:F2} mm, Bok B: {sideB:F2} mm";
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Rectangle sheetA0 = Rectangle.PaperSheet("A0");
            Console.WriteLine(sheetA0);

            Rectangle sheetA1 = Rectangle.PaperSheet("A1");
            Console.WriteLine(sheetA1);

            Rectangle sheetB2 = Rectangle.PaperSheet("B2");
            Console.WriteLine(sheetB2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Blad: {ex.Message}");
        }
    }
}
