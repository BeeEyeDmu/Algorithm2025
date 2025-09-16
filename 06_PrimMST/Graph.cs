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
  }
}