

namespace _011_FloydWarshall
{
  internal class Program
  {
    static int V = 10;
    const int Inf = 100;

    static void Main(string[] args)
    {
      //int[,] graph =
      //{
      //  {0, 4, 2, 5, Inf },
      //  {Inf, 0, 1, Inf, 4 },
      //  {1, 3, 0, 1, 2 },
      //  {-2, Inf, Inf, 0, 2 },
      //  {Inf, -3, 3, 1, 0 }
      //};
      int[,] graph = {
        { 0, 12, 15, Inf, Inf, Inf, Inf, Inf, Inf, Inf },
        { 12, 0, Inf, Inf, 4, 10, Inf, Inf, Inf, Inf },
        { 15, Inf, 0, 21, Inf, Inf, 7, Inf, Inf, Inf },
        { Inf, Inf, 21, 0, Inf, Inf, Inf, 25, Inf, Inf },
        { Inf, 4, Inf, Inf, 0, 3, Inf, Inf, 13, Inf },
        { Inf, 10, Inf, Inf, 3, 0, 10, Inf, Inf, Inf },
        { Inf, Inf, 7, Inf, Inf, 10, 0, 19, Inf, 9 },
        { Inf, Inf, Inf, 25, Inf, Inf, 19, 0, Inf, 5 },
        { Inf, Inf, Inf, Inf, 13, Inf, Inf, Inf, 0, 15 },
        { Inf, Inf, Inf, Inf, Inf, Inf, 9, 5, 15, 0 }
      };

      FloydWarshall(graph, V);
    }

    private static void FloydWarshall(int[,] graph, int v)
    {
      printGraph(graph, V);

      for(int k=0; k<V; k++)
      {
        Console.WriteLine("k={0}", k);
        for(int i=0; i<V; i++)
        {
          for(int j=0; j<V; j++)
          {
            if ( graph[i,k] != Inf &&
              graph[k,j] != Inf &&
              graph[i,j] > graph[i,k] + graph[k,j])
            {
              graph[i, j] = graph[i, k] + graph[k, j];
              Console.WriteLine(
                "Change:[{0},{1}] = [{2},{3}] + [{4},{5}] = {6}",
                i,j, i, k, k, j, graph[i,j]);
            }
          }
        }
        printGraph(graph, V);
      }
    }

    private static void printGraph(int[,] graph, int V)
    {
      for(int i=0; i<V; i++)
      {
        for (int j = 0; j < V; j++)
          Console.Write("{0,8}", graph[i, j]);
        Console.WriteLine();
      }
    }
  }
}
