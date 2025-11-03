

namespace _10_Tiling
{
  internal class Program
  {
    static void Main(string[] args)
    {
      int n;

      Console.Write("벽의 폭을 입력 : ");
      n = int.Parse(Console.ReadLine());

      for(int i=1; i<=n; i++)
        Console.WriteLine(i + " : " + Tile2(i));
    }

    static int[] t = new int[100];
    private static int Tile2(int n)
    {
      if (n == 1)
        return t[n] = 1;
      else if (n == 2)
        return t[n] = 3;
      else if (t[n] != 0)
        return t[n];
      else
        return t[n] = t[n - 1] + 2 * t[n - 2];
    }

    
    private static int Tile(int n)
    {
      if (n == 1)
        return t[n] = 1;
      else if (n == 2)
        return t[n] = 2;
      else if (t[n] != 0)
        return t[n];
      else
        return t[n] = t[n - 1] + t[n - 2];
    }
  }
}
