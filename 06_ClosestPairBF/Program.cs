using System;
using System.Data.SqlTypes;

namespace MyApp
{
  class Point
  {
    public int X { set; get; }
    public int Y { set; get; }

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }
  }
  internal class Program
  {
    const int N = 100000;
    static Point[] points = null;
    static void Main(string[] args)
    {
      Console.Write("점의 개수 : ");
      int n = int.Parse(Console.ReadLine());
      points = new Point[n];

      Random r = new Random();

      for(int i=0; i<n; i++)
      {
        points[i] = new Point(r.Next(1000), r.Next(1000));
      }

      for (int i = 0; i < n; i++)
      {
        Console.WriteLine("points[{2}] = ({0}, {1}) ", points[i].X, points[i].Y, i);
      }

      double min = double.MaxValue;
      int pairA = 0;
      int pairB = 0;

      for (int i=0; i<n-1; i++)
        for(int j=i+1; j<n; j++)
          if(distance(i, j) < min)
          {
            min = distance(i, j);
            pairA = i;
            pairB = j;
          }

      Console.WriteLine("Closest Pair : ({0}, {1}) = {2}",
        pairA, pairB, min);
    }

    private static double distance(int i, int j)
    {
      return Math.Sqrt((points[j].X - points[i].X)* (points[j].X - points[i].X)
        + (points[j].Y - points[i].Y) * (points[j].Y - points[i].Y));
    }
  }
}
