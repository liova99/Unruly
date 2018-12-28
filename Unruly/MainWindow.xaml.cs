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
            createGame.OpenFile();
            myCanvas.Background = new SolidColorBrush(Colors.LightBlue);

            createGame.CreateGrid(6, myCanvas);
            

        }


    }
}
