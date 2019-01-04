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
        CreateGame createGame;

        public MainWindow()
        {


            InitializeComponent();

            createGame = new CreateGame(myCanvas);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            myCanvas.Background = new SolidColorBrush(Colors.LightBlue);
            createGame.InitFile("med");
            createGame.CreateGrid(14, myCanvas);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            myCanvas.Background = new SolidColorBrush(Colors.LightBlue);
            createGame.InitFile("tiny");
            createGame.CreateGrid(6, myCanvas);

        }
    }
}
