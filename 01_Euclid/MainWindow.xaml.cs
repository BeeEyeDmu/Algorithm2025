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

namespace _01_Euclid
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

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      int x = int.Parse(txtN1.Text);
      int y = int.Parse(txtN2.Text);
      int gcd = Euclid(x, y);
      txtResult.Text = "GCD = " + gcd.ToString();
    }

    private int Euclid(int a, int b)
    {
      if (b == 0)
        return a;
      else
        return Euclid(b, a%b);
    }
  }
}
