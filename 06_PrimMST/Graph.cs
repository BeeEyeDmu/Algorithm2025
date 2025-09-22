using System;

namespace _06_PrimMST
{
  internal class Graph
  {
    static int MAX = 100; // 최대 버텍스 갯수
    static int INF = 999; // 무한대
    int V = 0;            // 버텍스 개수
    string[] vertex = new string[MAX];  // 버텍스의 이름 배열
    int[,] adj = new int[MAX, MAX];     // 인접행렬
    int MSTWeight = 0;                  // MST의 가중치 합

    internal void ReadGraph(string filename)
    {
      string[] lines = File.ReadAllLines("../../../"+filename);
      V = int.Parse(lines[0]);

      for(int i=1; i<lines.Length; i++)
      {
        //A	0	3	999	2	4	999
        string[] s = lines[i].Split('\t');

        InsertVertex(i - 1, s[0]);
        for (int j = 1; j < s.Length; j++)
          InsertEdge(i - 1, j - 1, int.Parse(s[j]));
      }
    }

    private void InsertEdge(int i, int j, int w)
    {
      adj[i, j] = w;
    }

    private void InsertVertex(int index, string name)
    {
      vertex[index] = name;
    }

    internal void PrintGraph()
    {
      Console.WriteLine("Vertex 수 : " + V);
      for(int i=0; i<V; i++)
      {
        Console.Write(vertex[i]);
        for(int j=0; j <V; j++)
          Console.Write("{0,8}", adj[i, j]);
        Console.WriteLine();
      }
    }

    // start는 시작하는 정점의 index
    internal void Prim(int start)
    {
      bool[] selected = new bool[V];  // 선택되었는지 표시
      int[] dist = new int[V];

      // 초기화
      for(int i=0; i<V; i++)
      {
        selected[i] = false;
        dist[i] = INF;
      }

      PrintDist(dist);
      PrintSelected(selected);

      dist[start] = 0;  // 시작점

      for(int i=0; i<V; i++)
      {
        int u = GetMinVertex(selected, dist);
        selected[u] = true;

        MSTWeight += dist[u];
        Console.WriteLine("{0} ({1}) -> ", 
          vertex[u], MSTWeight);

        // dist[] 업데이트
        for (int v = 0; v < V; v++)
          if (adj[u, v] != INF)
            if (!selected[v] && adj[u, v] < dist[v])
              dist[v] = adj[u, v];

        PrintDist(dist);
        PrintSelected(selected);
      }
    }

    private int GetMinVertex(bool[] selected, int[] dist)
    {
      int minV = 0;
      int minDist = INF;
      for(int v = 0; v<V; v++)
      {
        if (!selected[v] && dist[v] < minDist)
        {
          minV = v;
          minDist = dist[v];
        }
      }
      return minV;
    }

    private void PrintSelected(bool[] selected)
    {
      Console.Write("selected[] : \t");
      for (int i = 0; i < V; i++)
        Console.Write("{0,8}", selected[i]);
      Console.WriteLine();
    }

    private void PrintDist(int[] dist)
    {
      Console.Write("Dist[] : \t");
      for (int i = 0; i < V; i++)
        Console.Write("{0,8}", dist[i]);
      Console.WriteLine();
    }
  }
}