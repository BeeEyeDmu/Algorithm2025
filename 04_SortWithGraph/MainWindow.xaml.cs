using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;

namespace _04_SortWithGraph
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    static int MAX = 1000000;
    int[] a = new int[MAX];
    int N = 0;
    Thread t1;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnRandom_Click(object sender, RoutedEventArgs e)
    {
      N = int.Parse(txtData.Text);

      Random r = new Random();
      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(3*N);
      }

      printArray(0, N);
      Graph();
    }

    private void Graph()
    {
      canvas.Children.Clear();

      for(int i=0; i<N; i++)
      {
        Line l = new Line();
        l.X1 = l.X2 = i * 5;
        if (l.X1 > canvas.Width)
          break;
        l.Y1 = canvas.Height - (int)(a[i] / (3.0 * N) * canvas.Height);
        l.Y2 = canvas.Height;
        l.HorizontalAlignment = HorizontalAlignment.Left;
        l.VerticalAlignment = VerticalAlignment.Bottom;
        l.Stroke = Brushes.RoyalBlue;
        l.StrokeThickness = 4;
        canvas.Children.Add(l);
      }
    }

    private void printArray(int v, int n)
    {
      for(int i=0; i<N; i++)
        Console.Write(a[i] + ", ");
      Console.WriteLine();
    }

    private void btnTime_Click(object sender, RoutedEventArgs e)
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();
      BubbleSort();
      watch.Stop();
      long tickBubble = watch.ElapsedTicks;
      txtBubbleTime.Text = "버블 정렬 : " + tickBubble + " Ticks " + tickBubble/10000 + " ms";
      Graph();

      Random r = new Random();
      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(3 * N);
      }

      watch = System.Diagnostics.Stopwatch.StartNew();
      QuickSort(a, 0, N-1);
      watch.Stop();
      long tickQuick = watch.ElapsedTicks;
      txtQuickTime.Text = "퀵 정렬 : " + tickQuick + " Ticks " + tickQuick/10000 + " ms";
      Graph();

    }

    private void QuickSort(int[]a, int left, int right)
    {
      if(left < right)
      {
        int q = Partition(a, left, right);
        QuickSort(a, left, q - 1);
        QuickSort(a, q + 1, right);
      }
    }

    private int Partition(int[] a, int left, int right)
    {
      int low = left;
      int high = right + 1;
      int pivot = a[left];  // 피봇값은 맨 왼쪽 원소의 값

      do
      {
        do
        {
          low++;
        } while (low <= right && a[low] < pivot);
        do
        {
          high--;
        } while (high >= left && a[high] > pivot);
        if (low < high)
        {
          int t = a[low];
          a[low] = a[high];
          a[high] = t;
        }
      } while (low < high);

      a[left] = a[high];
      a[high] = pivot;
      return high;
    }
    
    private void BubbleSort()
    {
      for (int i = N - 1; i >= 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (a[j] > a[j + 1])
          {
            int t = a[j];
            a[j] = a[j + 1];
            a[j + 1] = t;
          }
        }
        Dispatcher.Invoke(new Action(Graph));
        Thread.Sleep(50);
      }
    }

    private void btnBubble_Click(object sender, RoutedEventArgs e)
    {
      t1 = new Thread(BubbleSort);
      t1.Start();
    }
  }
}
