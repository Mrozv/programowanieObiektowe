using System;

public class Vector
{
    private double[] coordinates;

    public Vector(byte dimension)
    {
        coordinates = new double[dimension];
    }

    public Vector(params double[] coordinates)
    {
        this.coordinates = coordinates;
    }

    public double Length => Math.Sqrt((double)DotProduct(this, this));

    public byte Dimension
    {
        get { return (byte)coordinates.Length; }
    }

    public double this[byte i]
    {
        get { return coordinates[i]; }
        set { coordinates[i] = value; }
    }

    public static double? DotProduct(Vector V, Vector W)
    {
        if (V.Dimension != W.Dimension)
            return double.NaN;

        double sum = 0;
        for (int i = 0; i < V.coordinates.Length; i++)
        {
            sum += V.coordinates[i] * W.coordinates[i];
        }
        return sum;
    }

    public static Vector Sum(params Vector[] vectors)
    {
        if (vectors.Length == 0)
            return null;

        byte dimension = vectors[0].Dimension;
        foreach (var vector in vectors)
        {
            if (vector.Dimension != dimension)
                return null; // Można również rzucić wyjątek
        }

        Vector sum = new Vector(dimension);
        for (int i = 0; i < dimension; i++)
        {
            foreach (var vector in vectors)
            {
                sum.coordinates[i] += vector.coordinates[i];
            }
        }
        return sum;
    }

    public static Vector operator +(Vector V, Vector W)
    {
        return Sum(V, W);
    }

    public static Vector operator -(Vector V, Vector W)
    {
        Vector difference = new Vector(V.Dimension);
        for (int i = 0; i < V.Dimension; i++)
        {
            difference.coordinates[i] = V.coordinates[i] - W.coordinates[i];
        }
        return difference;
    }

    public static Vector operator *(Vector V, double scalar)
    {
        Vector result = new Vector(V.Dimension);
        for (int i = 0; i < V.Dimension; i++)
        {
            result.coordinates[i] = V.coordinates[i] * scalar;
        }
        return result;
    }

    public static Vector operator *(double scalar, Vector V)
    {
        return V * scalar;
    }

    public static Vector operator /(Vector V, double scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Nie mozna dzielic przez zero.");

        Vector result = new Vector(V.Dimension);
        for (int i = 0; i < V.Dimension; i++)
        {
            result.coordinates[i] = V.coordinates[i] / scalar;
        }
        return result;
    }

    public override string ToString()
    {
        return string.Join(", ", coordinates);
    }

    class Program
    {
        static void Main()
        {
            Vector v1 = new Vector(1, 2, 3);
            Vector v2 = new Vector(4, 5, 6);

            Console.WriteLine("Wektor v1: " + v1);
            Console.WriteLine("Wektor v2: " + v2);
            Console.WriteLine("Dlugosc wektora v1: " + v1.Length);
            Console.WriteLine("Dlugosc wektora v2: " + v2.Length);
            Console.WriteLine("Iloczyn skalarny v1 i v2: " + Vector.DotProduct(v1, v2));
            Console.WriteLine("Suma wektorow v1 i v2: " + (v1 + v2));
            Console.WriteLine("Roznica wektorow v1 i v2: " + (v1 - v2));
            Console.WriteLine("Iloczyn wektora v1 przez 2: " + (v1 * 2));
            Console.WriteLine("Iloczyn 3 przez wektor v2: " + (3 * v2));
            Console.WriteLine("Wektor v1 podzielony przez 2: " + (v1 / 2));
        }
    }
}
