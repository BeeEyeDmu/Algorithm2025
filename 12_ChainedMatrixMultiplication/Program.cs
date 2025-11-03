


namespace _12_ChainedMatrixMultiplication
{
  internal class Program
  {
    static void Main(string[] args)
    {
      int[] arr = { 10, 30, 5, 60, 15, 10 };
      int n = 6;

      matrixChainOrder(arr, n);
    }

    private static void matrixChainOrder(int[] a, int n)
    {
      int[,] m = new int[n, n]; // 곱셈 횟수
      int[,] s = new int[n, n]; // 최적 분할 위치

      for(int L=2; L<n; L++)
      {
        for(int i=1; i<=n-L; i++)
        {
          int j = i + L - 1;
          m[i, j] = int.MaxValue;
          for(int k=i; k<j; k++)
          {
            int temp = m[i, k] + m[k + 1, j] + a[i - 1] * a[k] * a[j];
            if(temp < m[i,j])
            {
              m[i,j] = temp;
              s[i, j] = k;
            }
          }
        }
        Console.WriteLine("L = {0}", L);
        printArray(m, n);
        printArray(s, n);
      }

      // 최적 곱셈 순서(재귀함수)
      printParen(s, 1, n - 1);    // s배열의 s[1,5]
    }

    private static void printParen(int[,] s, int i, int j)
    {
      if (i == j)
        Console.Write("A{0}", i);
      else
      {
        Console.Write("(");
        printParen(s, i, s[i, j]);
        printParen(s, s[i, j] + 1, j);
        Console.Write(")");
      }
    }

    private static void printArray(int[,] m, int n)
    {
      for(int i=1; i<n;i++)
      {
        for(int j=1; j<n; j++)
          Console.Write("{0,8}", m[i, j]);
        Console.WriteLine();
      }
      Console.WriteLine();
    }
  }
}
