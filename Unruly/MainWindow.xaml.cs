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
using System.Drawing;

namespace Unruly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CreateGame createGame = new CreateGame();

        public MainWindow()
        {


            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            myCanvas.Background = new SolidColorBrush(Colors.LightBlue);
            createGame.InitFile("small");
            createGame.CreateGrid(8, myCanvas);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
            myCanvas.Background = new SolidColorBrush(Colors.LightBlue);
            createGame.InitFile("tiny");
            createGame.CreateGrid(6, myCanvas);

        }
    }
}
