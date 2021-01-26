using System;

class Program
{
    static void Main()
    {
        double x,y,xy;
        x = double.Parse(Console.ReadLine());
        y = double.Parse(Console.ReadLine());

        xy = Math.Sqrt(x * x + y * y);
        Console.WriteLine(xy);
    }

}