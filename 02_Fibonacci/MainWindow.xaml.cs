using System;
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

namespace _02_Fibonacci
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnCalc_Click(object sender, RoutedEventArgs e)
    {
      int[] a = new int[50];
      a[0] = a[1] = 1;
      int n = int.Parse(textBox.Text);

      listBox.Items.Clear();

      for (int i = 2; i < n; i++)
        a[i] = a[i - 1] + a[i - 2];

      for(int i=0; i<n; i++)
        listBox.Items.Add(a[i]);
    }
  }
}
