using System;
using System.IO;
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

        private Nullable<Boolean>[,] _myArray = null;

        int maxRows, maxColumns;



        private Nullable<Boolean>[,] _solution = new Nullable<Boolean>[6, 6];

        private bool _isAButton = false;

        private bool _correctly = true;

        private bool isNull= true;

        private Brush _black = System.Windows.Media.Brushes.Black;
        private Brush _white = System.Windows.Media.Brushes.White;
        private Brush _gray = System.Windows.Media.Brushes.Gray;

        private bool result = false;


        public void CreateRectangle(Canvas myCanvas, String rectName, int x, int y, Nullable<Boolean> color)
        {




            Rectangle myRect = new System.Windows.Shapes.Rectangle();
            Button checkBtn = new Button();
            Button showSolutionBtn = new Button();


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
                    myRect.Fill = _black;

                }
                else if (color == true)
                {
                    _myArray[x, y] = null;
                    myRect.Fill = _gray;

                }
                else if (color == false)
                {
                    _myArray[x, y] = true;
                    myRect.Fill = _white;

                }

            };



            checkBtn.Click += (sender, args) =>
            {
                _correctly = true;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (_myArray[i, j] != _solution[i, j])
                        {
                            _correctly = false;

                            break;
                        }

                    }

                    if (!_correctly)
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

            showSolutionBtn.Click += (sender, e) =>
            {


               
                result = Solve();

                if (result)
                {
                    CreateGrid(maxColumns, myCanvas);
                    MessageBox.Show(result.ToString());

                }
                Console.WriteLine($"Result {result.ToString()}");

            };




            if (_color == null)
            {
                myRect.Fill = _gray;

            }
            else if (_color == true)
            {
                myRect.Fill = _white;

            }
            else if (_color == false)
            {
                myRect.Fill = _black;

            }

            checkBtn.Content = "Ready?!!";
            checkBtn.Width = 200;
            checkBtn.Height = 200;

            showSolutionBtn.Content = "Can't do it?!!";
            showSolutionBtn.Width = 200;
            showSolutionBtn.Height = 200;

            if (_isAButton == false)
            {
                myCanvas.Children.Add(checkBtn);
                myCanvas.Children.Add(showSolutionBtn);

                Canvas.SetRight(checkBtn, y * _myRectangleSize);
                Canvas.SetBottom(checkBtn, y * _myRectangleSize);

                Canvas.SetRight(showSolutionBtn, y * _myRectangleSize);
                Canvas.SetTop(showSolutionBtn, y * _myRectangleSize);

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

        public void InitFile()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 3);
            _myArray = OpenFile($@"Resources\Puzzle\small_{randomNumber}.txt");
        }

        public class AssignmentResult
        {
            public int i, j;
        }



        /// <summary>
        /// returns null if there is no assignment possible
        /// </summary>
        /// <returns></returns>
        public AssignmentResult GetNextAssignment()
        {
            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxColumns; j++)
                {
                    if (_myArray[i, j] == null)
                    {
                        
                        return new AssignmentResult() { i = i, j = j };
                    }
                    
                }
            }

            return null;
        }

        

        public bool RowNumberCheck()
        {


            for (int i = 0; i < maxRows; i++)
            {
                int trueIte = 0;
                int falseIte = 0;
                int iterator = 0;

                for (int j = 0; j < maxColumns; j++)
                {
                    if (trueIte > 3 || falseIte > 3)
                    {
                        return false;
                    }
                   

                    if (_myArray[i, j] == true)
                    {
                        trueIte++;
                    }

                    else if (_myArray[i, j] == false)
                    {
                        falseIte++;
                    }

                    // next row
                    if (iterator == 5)
                    {
                        iterator = 0;
                        falseIte = 0;
                        iterator = 0;
                    }
                    else
                    {
                        iterator++;
                    }
                }
            }
            return true;
        }

        public bool ColumnNumberCheck()
        {


            for (int i = 0; i < maxRows; i++)
            {
                int trueIte = 0;
                int falseIte = 0;
                int iterator = 0;
                for (int j = 0; j < maxColumns; j++)
                {
                    if (trueIte > 3 || falseIte > 3)
                    {
                        return false;
                    }
                    

                    if (_myArray[i, j] == true)
                    {
                        trueIte++;
                    }

                    else if (_myArray[j, i] == false)
                    {
                        
                        falseIte++;
                    }
                   


                    // next row
                    if (iterator == 5)
                    {
                        iterator = 0;
                        falseIte = 0;
                        iterator = 0;
                    }
                    else
                    {
                        iterator++;
                    }
                }
            }
            return true;

        }

        public bool Max2RowCheck()
        {

            //only 2 fields which are consecutive can have the same color
            for (int i = 0; i < maxRows; i++)
            {
                Nullable<bool> lastColor = null;
                bool actualColor = (bool)_myArray[0, 0];
                bool lastAndActual = false;
                int black = 0;
                int white = 0;
                int iterator = 0;
                for (int j = 0; j < maxColumns; j++)
                {

                    actualColor = (bool)_myArray[i, j];

                    if ((black > 2 && actualColor == false) || (white > 2 && actualColor == true))
                    {
                        
                        return false;
                    }
                   
                    if (actualColor == true)
                    {
                        white++;
                    }
                    else if (actualColor == false)
                    {
                        black++;
                    }
                    else
                    {
                        return false;
                    }

                    iterator++;
                    if (iterator == 6)
                    {
                        iterator = 0;
                        black = 0;
                        white = 0;
                    }
                }
            }
            return true;
        }

        public bool Max2ColumnCheck()
        {

            //only 2 fields which are consecutive can have the same color

            for (int i = 0; i < maxRows; i++)
            {
                
                bool actualColor = (bool)_myArray[0, 0];
                int black = 0;
                int white = 0;
                int iterator = 0;
                for (int j = 0; j < maxColumns; j++)
                {

                    actualColor = (bool)_myArray[j, i];


                    if ((black > 2 && actualColor == false) || (white > 2 && actualColor == true))
                    {
                        return false;
                    }
                 

                    if (actualColor == true)
                    {
                        white++;
                    }
                    else if (actualColor == false)
                    {
                        black++;
                    }
                    else
                    {
                        
                        return false;
                    }

                    iterator++;
                    if (iterator == 6)
                    {
                        iterator = 0;
                        black = 0;
                        white = 0;
                    }
                }
            }
            return true;
        }


        public bool ContainsRuleViolation()
        {
            return RowNumberCheck() && ColumnNumberCheck() && Max2RowCheck() && Max2ColumnCheck();
        }

        public bool Solve()
        {
            //optimization: some rule violations can be detected before everything is complete


            //choose variable to assign
            AssignmentResult assignmentResult = GetNextAssignment();

            if (assignmentResult == null)
            {
                //Console.WriteLine($"Rulle Violation {ContainsRuleViolation().ToString()}");
                return ContainsRuleViolation();
            }

            bool? valueBefore = _myArray[assignmentResult.i, assignmentResult.j];

            _myArray[assignmentResult.i, assignmentResult.j] = true;
            

            //assign with white

            if (!Solve())
            {
                //unassign value
                //assign black

                _myArray[assignmentResult.i, assignmentResult.j] = false;
                //myRect.Fill = System.Windows.Media.Brushes.Black;

                if (!Solve())
                {
                    _myArray[assignmentResult.i, assignmentResult.j] = valueBefore;
                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

           
        }


        public Nullable<Boolean>[,] OpenFile(string path)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 3);
            Console.WriteLine("random " + randomNumber);
            String[] puzzle = new String[]
            {
                "small_1.txt","small_2.txt",
            };

            //$@"Resources\Puzzle\small_{randomNumber}.txt"
            string[] puzzleLines = File.ReadAllLines(path);

            string[] puzzleFirstLineSplitted = puzzleLines[0].Split(' ');


            maxRows = int.Parse(puzzleFirstLineSplitted[0]);
            maxColumns = int.Parse(puzzleFirstLineSplitted[1]);
            Console.WriteLine($"MAx Rows and Colums {maxRows}, {maxColumns}");

            Nullable<Boolean>[,] resultArray = new Nullable<Boolean>[maxRows, maxColumns];


            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxColumns; j++)
                {
                    char currentPuzzleChar = puzzleLines[i + 1][j];

                    if (currentPuzzleChar == 'T')
                    {
                        resultArray[i, j] = true;
                    }
                    else if (currentPuzzleChar == 'F')
                    {
                        resultArray[i, j] = false;
                    }
                    else
                    {
                        resultArray[i, j] = null;
                    }

                }
            }
            return resultArray;




        }


    }

}
