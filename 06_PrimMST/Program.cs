namespace _06_PrimMST
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Graph g = new Graph();
      g.ReadGraph("graph1.txt");
      g.PrintGraph();
    }
  }
}
