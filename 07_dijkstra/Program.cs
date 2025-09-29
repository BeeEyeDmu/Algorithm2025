


namespace _07_dijkstra
{
  internal class Program
  {
    static string[] city = {"서울", "천안", "원주", "강릉",
      "논산","대전", "대구", "포항","광주", "부산"};
    static int V = 10;
    static bool[] sptSet = new bool[V];
    static int[] D = new int[V];

    static void Main(string[] args)
    {
      int[,] graph = new int[,]
      {
        { 0,   12,  15,  0,   0,   0,   0,   0,   0,   0 },
        { 12,  0,   0,   0,   4,   10,  0,   0,   0,   0 },
        { 15,  0,   0,   21,  0,   0,   7,   0,   0,   0 },
        { 0,   0,   21,  0,   0,   0,   0,   25,  0,   0 },
        { 0,   4,   0,   0,   0,   3,   0,   0,   13,  0 },
        { 0,   10,  0,   0,   3,   0,   10,  0,   0,   0 },
        { 0,   0,   7,   0,   0,   10,  0,   19,  0,   9 },
        { 0,   0,   0,   25,  0,   0,   19,  0,   0,   5 },
        { 0,   0,   0,   0,   13,  0,   0,   0,   0,   15 },
        { 0,   0,   0,  0,   0,   0,   9,   5,   15,  0 }
      };

      Dijkstra(graph, 5);
    }

    private static void Dijkstra(int[,] graph, int start)
    {
      // 초기화
      for(int i=0; i<V; i++)
      {
        D[i] = int.MaxValue;
        sptSet[i] = false;
      }

      D[start] = 0;

      // 아직 결정되지 않은 도시들 중에서 D[]가 최소인 도시를 찾는다
      for(int i=0; i<V; i++)
      {
        int minIndex = MinDistance();
        sptSet[minIndex] = true;
        Console.WriteLine("최단 경로 : {0}", city[minIndex]);

        // D[] 업데이트 : minIndex 도시와 연결된 도시의 D[]를 업데이트
        for(int v=0; v<V; v++)
        {
          if (sptSet[v] == false  // 아직 결정되지 않은 도시 중에서
            && graph[minIndex, v] != 0  // 연결되어 있고
            && D[minIndex] + graph[minIndex, v] < D[v])
          {
            D[v] = D[minIndex] + graph[minIndex, v];
          }
        }

        PrintD();
      }
    }

    private static void PrintD()
    {
      for(int i=0; i<V; i++)
        Console.WriteLine("{0}\t{1}\t{2}", city[i], sptSet[i], D[i]);
    }

    private static int MinDistance()
    {
      int min = int.MaxValue;
      int minIndex = -1;

      for (int i = 0; i < V; i++)
      {
        if (sptSet[i] == false && D[i] < min)
        {
          min = D[i];
          minIndex = i;
        }
      }

      return minIndex;
    }
  }
}
