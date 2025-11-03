
namespace _09_Fibonacci
{
  internal class Program
  {
    static void Main(string[] args)
    {
      int n;

      Console.Write("n 입력 : ");
      n = int.Parse(Console.ReadLine());

      for(int i=1; i<=n; i++)
        Console.WriteLine(i + " : " + Fibo(i));
    }

    static long[] d = new long[100];
    private static long Fibo(int n)
    {
      if (n == 1 || n == 2)
        return 1;
      else if (d[n] != 0)
        return d[n];
      else
        return d[n] = Fibo(n - 1) + Fibo(n - 2);
    }
  }
}
