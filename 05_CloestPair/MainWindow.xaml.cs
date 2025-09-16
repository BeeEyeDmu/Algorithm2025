using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_CloestPair
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    int noOfPoints; // 점의 개수
    Point[] points; // 점의 배열

    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
      can.Children.Clear();
      noOfPoints = int.Parse(txtNo.Text);
      points = new Point[noOfPoints];
      MakePointArray();
      SortPointArray();
    }

    private void SortPointArray()
    {
      IComparer xComp = new XComparer();
      Array.Sort(points, xComp);
      PrintPoints();
    }

    private void PrintPoints()
    {
      for (int i = 0; i < noOfPoints; i++)
      {
        Console.WriteLine(points[i].X + ", " + points[i].Y);
      }
    }

    // 랜덤하게 점의 좌표를 만들기
    private void MakePointArray()
    {
      Random r = new Random();

      for (int i = 0; i < noOfPoints; i++)
      {
        points[i] = new Point(r.NextDouble() * can.Width,
          r.NextDouble() * can.Height);
      }

      foreach (var p in points)
      {
        Rectangle rect = new Rectangle();

        rect.Width = rect.Height = 2;
        rect.Stroke = Brushes.Black;
        Canvas.SetLeft(rect, p.X - 1);
        Canvas.SetTop(rect, p.Y - 1);
        can.Children.Add(rect);
      }
    }

    private void btnBruteForce_Click(object sender, RoutedEventArgs e)
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();
      PointPair result = FindClosestPair(points, 0, noOfPoints - 1);
      watch.Stop();

      long tick = watch.ElapsedTicks;
      long ms = watch.ElapsedMilliseconds;
      MessageBox.Show("Brute Force : " + tick + " ticks, " + ms + " ms");
      HightLight(result);

      MessageBox.Show(String.Format("({0},{1})-({2},{3}) = {4}",
        result.P1.X, result.P1.Y, result.P2.X, result.P2.Y, result.Dist));      
    }

    private void HightLight(PointPair result)
    {
      // 최근접점의 쌍을 빨간색 박스로 표시
      int size = 12;
      Rectangle rect = new Rectangle();
      double left = 0;

      if(result.P1.X < result.P2.X)
      {
        rect.Width = result.P2.X - result.P1.X + size;
        left = result.P1.X - size/2;
      }
      else
      {
        rect.Width = result.P1.X - result.P2.X + size;
        left = result.P2.X - size / 2;
      }

      double top = 0;
      if (result.P1.Y < result.P2.Y)
      {
        rect.Height = result.P2.Y - result.P1.Y + size;
        top = result.P1.Y - size / 2;
      }
      else
      {
        rect.Height = result.P1.Y - result.P2.Y + size;
        top = result.P2.Y - size / 2;
      }

      rect.Stroke = Brushes.Red;
      rect.StrokeThickness = 1;
      Canvas.SetLeft(rect, left);
      Canvas.SetTop(rect, top);

      can.Children.Add(rect);
    }

    // BruteForce 방법의 알고리즘(N2)
    private PointPair FindClosestPair(Point[] points, int start, int end)
    {
      double min =double.MaxValue;
      int minI = 0, minJ = 0;  // 가장 가까운 점 두개의 인덱스

      for (int i = start; i < end - 1; i++)
        for (int j = i + 1; j < end; j++)
          if (Distance(i, j) < min)
          {
            min = Distance(i, j);
            minI = i;
            minJ = j;
          }

      PointPair pp = new PointPair(points[minI], points[minJ], min);
      return pp;
    }

    private double Distance(int i, int j)
    {
      return Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) +
        Math.Pow(points[i].Y - points[j].Y, 2));
    }

    private void btnDC_Click(object sender, RoutedEventArgs e)
    {           
      var watch = System.Diagnostics.Stopwatch.StartNew();
      PointPair result = FindClosestPairDC(points, 0, noOfPoints-1);
      watch.Stop();

      long tick = watch.ElapsedTicks;
      long ms = watch.ElapsedMilliseconds;
      MessageBox.Show("Divede & Conquer : " + tick + " ticks, " + ms + " ms");

      HightLight(result);

      MessageBox.Show(String.Format("({0},{1})-({2},{3}) = {4}",
        result.P1.X, result.P1.Y, result.P2.X, result.P2.Y, result.Dist));
    }

    // 분할정복 최근접 점의 쌍 찾기 알고리즘
    private PointPair FindClosestPairDC(Point[] points, int left, int right)
    {
      // 100보다 작으면 Brute Force 알고리즘을 사용(더이상 분할하지 않는다)
      if(right - left < 100)
        return FindClosestPair(points, left, right);

      // 분할한다
      int mid = (left+right)/2;
      CenterLine(points[mid].X);

      PointPair cPL = FindClosestPairDC(points, left, mid);
      PointPair cPR = FindClosestPairDC(points, mid + 1, right);

      double d = Math.Min(cPL.Dist, cPR.Dist);

      PointPair cPc = FindMidRange(points, mid, d);

      return MinPointPair(cPL, cPR, cPc);
    }

    private void CenterLine(double mid)
    {
      Line line = new Line();
      line.X1 = line.X2 = mid;
      line.Y1 = 0;
      line.Y2 = can.Height;
      line.Stroke = Brushes.Blue;
      line.StrokeThickness = 1;
      can.Children.Add(line);
    }

    private PointPair MinPointPair(PointPair cPL, PointPair cPR, PointPair cPc)
    {
      if (cPL.Dist <= cPR.Dist && cPL.Dist <= cPc.Dist)
        return cPL;
      else if (cPR.Dist <= cPL.Dist && cPR.Dist <= cPc.Dist)
        return cPR;
      else
        return cPc;
    }

    private PointPair FindMidRange(Point[] points, int mid, double d)
    {
      int left = 0, right = 0;

      for (int i = mid; i >= 0; i--)
      {
        if (points[mid].X - points[i].X > d)
        {
          left = i;
          break;
        }
      }

      for (int i = mid; i < points.Length; i++)
      {
        if (points[i].X - points[mid].X > d)
        {
          right = i;
          break;
        }
      }

      return FindClosestPair(points, left, right); // Brute Force
    }
  }

  internal class XComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      if (((Point)x).X - ((Point)y).X > 0)
        return 1;
      else 
        return -1;
    }
  }
}
