using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Documents;

namespace Unruly
{
    class CreateGame
    {



        public void CreateRectangle(Canvas myGrid, int i, int j, Nullable<Boolean> color)
        {
            Rectangle myRect = new System.Windows.Shapes.Rectangle();


            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = 150;
            myRect.Width = 150;
            myRect.MouseLeftButtonUp += (sender, args) =>
            {
                MessageBox.Show($"Clicked Rect {i},{j}");
            };

            if (color == null)
            {
                myRect.Fill = System.Windows.Media.Brushes.Gray;
            }
            else if (color == true)
            {
                myRect.Fill = System.Windows.Media.Brushes.White;
            }
            else if (color == false)
            {
                myRect.Fill = System.Windows.Media.Brushes.Black;
            }

            myGrid.Children.Add(myRect);


            Canvas.SetTop(myRect, i * 150);
            Canvas.SetLeft(myRect, j * 150);

        }




    }

}
