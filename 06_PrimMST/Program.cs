namespace _06_PrimMST
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Graph g = new Graph();
      g.ReadGraph("graph2.txt");
      g.PrintGraph();

      g.Prim(0);  // 0번 버텍스에서 시작하는 MST
    }
  }
}
