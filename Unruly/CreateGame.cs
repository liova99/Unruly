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
        private int _myRectangleSize = 50;

        private Nullable<Boolean> _color = null;

        private Nullable<Boolean>[,] _myArray = new Nullable<Boolean>[6, 6] {

               {null, null, null, null, true, null },
               {null, null, true, null, false,  false },
               {false, false, null, null, null, null },
               {false, null, null, true, null, null },
               {null, null, null, null, null, true },
               {null, null, null, null,  null, false },

            };

        private Nullable<Boolean>[,] _solution = new Nullable<Boolean>[6, 6] {

               {false, true, false, false, true, true},
               {true, false, true, true,  false,  false },
               {false, false, true, false, true, true },
               {false, true, false, true, true, false },
               {true, false, true, false, false, true },
               {true, true, false, true,  false, false },

            };

        private bool _isAButton = false;

        private bool _correctly = true;


        public void CreateRectangle(Canvas myCanvas, String rectName, int x, int y, Nullable<Boolean> color)
        {




            Rectangle myRect = new System.Windows.Shapes.Rectangle();
            Button checkBtn = new Button();


            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = _myRectangleSize;
            myRect.Width = _myRectangleSize;


            myRect.Name = rectName + x.ToString() + y.ToString();






            myRect.MouseLeftButtonUp += (sender, args) =>
            {
                Console.WriteLine($"Clicked Rect {myRect.Name}");

                color = _myArray[x, y];

                if (color == null)
                {
                    _myArray[x, y] = false;
                    myRect.Fill = System.Windows.Media.Brushes.Black;

                }
                else if (color == true)
                {
                    _myArray[x, y] = null;
                    myRect.Fill = System.Windows.Media.Brushes.Gray;

                }
                else if (color == false)
                {
                    _myArray[x, y] = true;
                    myRect.Fill = System.Windows.Media.Brushes.White;

                }

            };

           

            checkBtn.Click += (sender, args) =>
            {


                _correctly = true;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if(_myArray[i,j] != _solution[i,j])
                        {
                            _correctly = false;
                            
                            break;
                        }
                        
                    }

                    if(!_correctly)
                    {
                        break;
                    }
                }

                if (_correctly)
                {
                    MessageBox.Show("Well Done!!");
                }
                else
                {

                    MessageBox.Show("Nope, du blede Kue");
                }

            };




            if (_color == null)
            {
                myRect.Fill = System.Windows.Media.Brushes.Gray;

            }
            else if (_color == true)
            {
                myRect.Fill = System.Windows.Media.Brushes.White;

            }
            else if (_color == false)
            {
                myRect.Fill = System.Windows.Media.Brushes.Black;

            }

            checkBtn.Content = "Ready?!!";
            checkBtn.Width = 200;
            checkBtn.Height = 200;

            if (_isAButton == false)
            {
                myCanvas.Children.Add(checkBtn);

                Canvas.SetRight(checkBtn, y * _myRectangleSize);
                Canvas.SetBottom(checkBtn, y * _myRectangleSize);
                _isAButton = true;
            }



            myCanvas.Children.Add(myRect);
            Canvas.SetTop(myRect, x * _myRectangleSize);
            Canvas.SetLeft(myRect, y * _myRectangleSize);



        }

        public void CreateGrid(int gridSize, Canvas myCanvas)
        {

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _color = _myArray[i, j];
                    CreateRectangle(myCanvas, "rect_", i, j, _color);
                }
            }

        }


        public void ChangeColor(Rectangle r)
        {

        }


    }

}
