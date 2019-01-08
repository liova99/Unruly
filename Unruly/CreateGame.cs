﻿using System;
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
using System.Threading;
using System.Diagnostics;

namespace Unruly
{
    public class CreateGame
    {
        public HashSet<Tuple<int, int>> unassignedPositions = new HashSet<Tuple<int, int>>();

        private int _myRectangleSize = 30;

        private Nullable<Boolean> _color = null;

        public Nullable<Boolean>[,] _myArray = null;
        private Nullable<Boolean>[,] _initalArray;
        private Rectangle[,] rectangles;

        public int maxRows, maxColumns;

        private Brush _black = System.Windows.Media.Brushes.Black;
        private Brush _white = System.Windows.Media.Brushes.White;
        private Brush _gray = System.Windows.Media.Brushes.Gray;


        private Canvas myCanvas;

        private IAssignmentStragey assignmentStragey = new SimpleAssignmentStrategy();

        // check speed
        private int _backtracks = 0;


        public CreateGame(Canvas myCanvas)
        {
            this.myCanvas = myCanvas;

            Button checkBtn = new Button();
            Button showSolutionBtn = new Button();

            checkBtn.Click += (sender, args) =>
            {

                if (Solved())
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

                _myArray = _initalArray.Clone() as Nullable<Boolean>[,];




                //Task.Run(() =>
                //{

                bool result;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                RunPreSolve();
                result = true; //  Solve(); //  true; // 
                stopwatch.Stop();

                Console.WriteLine(stopwatch.Elapsed);

                if (result)
                {
                    //myCanvas.Dispatcher.Invoke(() =>
                    // {

                    CreateGrid(maxRows, myCanvas);
                    MessageBox.Show("Backtracks: " + _backtracks);
                    _backtracks = 0;
                    //});
                }
                else
                {
                    //myCanvas.Dispatcher.Invoke(() =>
                    //{
                    CreateGrid(maxRows, myCanvas);
                    MessageBox.Show("I can not solve it :( ");
                    //});
                }
                // });


            };

            // check Button
            checkBtn.Content = "Ready?!!";
            checkBtn.Width = 200;
            checkBtn.Height = 200;

            // showSolution Button
            showSolutionBtn.Content = "Can't do it?!!";
            showSolutionBtn.Width = 200;
            showSolutionBtn.Height = 200;
            myCanvas.Children.Add(checkBtn);
            myCanvas.Children.Add(showSolutionBtn);

            Canvas.SetRight(checkBtn, _myRectangleSize);
            Canvas.SetBottom(checkBtn, _myRectangleSize);

            Canvas.SetRight(showSolutionBtn, _myRectangleSize);
            Canvas.SetTop(showSolutionBtn, _myRectangleSize);


        }

        public void CreateRectangle(Canvas myCanvas, String rectName, int x, int y, Nullable<Boolean> color)
        {
            Rectangle myRect;
            if (rectangles[x, y] == null)
            {
                myRect = new System.Windows.Shapes.Rectangle();
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
                myCanvas.Children.Add(myRect);
                Canvas.SetTop(myRect, x * _myRectangleSize);
                Canvas.SetLeft(myRect, y * _myRectangleSize);


                rectangles[x, y] = myRect;
            }
            else
            {
                myRect = rectangles[x, y];
            }





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


        public void RunPreSolve()
        {
            PreSolveRowsColorAssign();
            PreSolveColumnColorAssign();
        }


        /// <summary>
        /// if there is maxRows/2 of one color fill the
        /// other rows with the other color
        /// ex. in one 6x6 puzzle, if there is 3 black in one row
        /// the other 3 must be white
        /// </summary>
        public void PreSolveRowsColorAssign()
        {
            _backtracks++;
            Console.WriteLine("Backtrack: " + _backtracks);
            List<AssignmentResult> unitResults = UnitPropagation();

            for (int i = 0; i < maxColumns; i++)
            {
                int white = 0;
                int black = 0;

                for (int j = 0; j < maxRows; j++)
                {
                    // wont include the j==0, or j== j < maxColumns - 2. I make it in else if stmt.
                    // if there 2 the same color, fill the next with the other color 
                    if (j > 1 && j < maxColumns - 2 && _myArray[i, j] == null)
                    {
                        //
                        if (_myArray[i, j - 1] == _myArray[i, j - 2] && _myArray[i, j - 1] != null)
                        {

                            _myArray[i, j] = _myArray[i, j - 1] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                        else if (_myArray[i, j + 1] == _myArray[i, j + 2] && _myArray[i, j + 1] != null)
                        {
                            _myArray[i, j] = _myArray[i, j + 1] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                        // if the last and the next is the same color, the current must be other color
                        else if (_myArray[i, j - 1] == _myArray[i, j + 1] && _myArray[i, j + 1] != null)
                        {
                            _myArray[i, j] = _myArray[i, j - 1] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }

                    // if j==0 or j== 1 and the next 2 is the same color
                    else if ((j == 0 || j == 1 ) && _myArray[i, j] == null)
                    {
                        if (_myArray[i, j + 1] == _myArray[i, j + 2] && _myArray[i, j + 1] != null)
                        {
                            _myArray[i, j] = _myArray[i, j + 1] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }
                    // if j == maxRows - 1 or j == maxRows - 2 and the previus 2 is the same color
                    else if ((j == maxRows - 1 || j == maxRows - 2) && _myArray[i, j] == null)
                    {
                        if (_myArray[i, j - 1] == _myArray[i, j - 2] && _myArray[i, j - 1] != null)
                        {
                            _myArray[i, j] = _myArray[i, j - 1] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }

                    if (_myArray[i, j] == true)
                    {
                        white++;
                    }
                    else if (_myArray[i, j] == false)
                    {
                        black++;
                    }

                    if (black == maxRows / 2)
                    {
                        for (int y = 0; y < maxRows; y++)
                        {
                            if (_myArray[i, y] == null)
                            {
                                _myArray[i, y] = true;
                                AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = y, color = true };
                                Assign(assignmentResult, true);

                            }
                        }

                    }
                    else if (white == maxRows / 2)
                    {
                        for (int y = 0; y < maxRows; y++)
                        {
                            if (_myArray[i, y] == null)
                            {
                                _myArray[i, y] = false;
                                AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = y, color = false };
                                Assign(assignmentResult, false);

                            }
                        }

                    }
                }
            }
        }


        /// <summary>
        /// if there is maxcolums/2 of one color fill the
        /// others with the other color
        /// ex. in one 6x6 puzzle, if there is 3 black in one column
        /// the other 3 must  be white
        /// </summary>
        public void PreSolveColumnColorAssign()
        {
            _backtracks++;
            Console.WriteLine("Backtrack: " + _backtracks);
            List<AssignmentResult> unitResults = UnitPropagation();

            for (int j = 0; j < maxRows; j++)
            {
                int white = 0;
                int black = 0;

                for (int i = 0; i < maxColumns; i++)
                {
                    // if there 2 the same color, fill the next with the other color 
                    if (i > 1 && i < maxColumns - 2 && _myArray[i, j] == null)
                    {
                        if (_myArray[i - 1, j] == _myArray[i - 2, j] && _myArray[i - 1, j] != null)
                        {

                            _myArray[i, j] = _myArray[i - 1, j] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                        else if (_myArray[i + 1, j] == _myArray[i + 2, j] && _myArray[i + 1, j] != null)
                        {
                            _myArray[i, j] = _myArray[i + 1, j] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                        // if the last and the next is the same color, the current must be other color
                        else if (_myArray[i - 1, j] == _myArray[i + 1, j] && _myArray[i + 1, j] != null)
                        {
                            _myArray[i, j] = _myArray[i - 1, j] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }

                    // if i==0 or i== 1 and the next 2 is the same color
                    else if ((i == 0 || i == 1) && _myArray[i, j] == null)
                    {
                        if (_myArray[i + 1, j] == _myArray[i + 2, j] && _myArray[i + 1, j] != null)
                        {
                            _myArray[i, j] = _myArray[i + 1, j] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }

                    // if i == maxRows - 1 or i == maxRows - 2 and the next 2 is the same color
                    else if ((i == maxRows - 1 || i == maxRows - 2) && _myArray[i, j] == null)
                    {
                        if (_myArray[i - 1, j] == _myArray[i - 2, j] && _myArray[i - 1, j] != null)
                        {
                            _myArray[i, j] = _myArray[i - 1, j] == true ? false : true;
                            AssignmentResult assignmentResult = new AssignmentResult() { i = i, j = j, color = (bool)_myArray[i, j] };
                            Assign(assignmentResult, (bool)_myArray[i, j]);
                            RunPreSolve();
                        }
                    }

                    if (_myArray[i, j] == true)
                    {
                        white++;
                    }
                    else if (_myArray[i, j] == false)
                    {
                        black++;
                    }

                    if (black == maxColumns / 2)
                    {
                        for (int y = 0; y < maxColumns; y++)
                        {
                            if (_myArray[y, j] == null)
                            {
                                _myArray[y, j] = true;
                                AssignmentResult assignmentResult = new AssignmentResult() { i = y, j = j, color = true };
                                Assign(assignmentResult, true);

                            }
                        }

                    }
                    else if (white == maxColumns / 2)
                    {
                        for (int y = 0; y < maxColumns; y++)
                        {
                            if (_myArray[y, j] == null)
                            {
                                _myArray[y, j] = false;
                                AssignmentResult assignmentResult = new AssignmentResult() { i = y, j = j, color = false };
                                Assign(assignmentResult, false);

                            }
                        }

                    }
                }
            }
        }




        #region Check if rule violation

        // TODO: Check the loops, make them more efficient




        public bool RowNumberCheck()
        {
            for (int i = 0; i < maxColumns; i++)
            {
                int white = 0;
                int black = 0;

                for (int j = 0; j < maxRows; j++)
                {

                    if (_myArray[i, j] == true)
                    {
                        white++;
                    }
                    else if (_myArray[i, j] == false)
                    {
                        black++;
                    }

                    if (white > maxRows / 2 || black > maxRows / 2)
                    {
                        return false;
                    }

                }
            }
            return true;
        }


        public bool ColumnNumberCheck()
        {

            for (int j = 0; j < maxRows; j++)
            {
                int white = 0;
                int black = 0;

                for (int i = 0; i < maxColumns; i++)
                {

                    if (_myArray[i, j] == true)
                    {
                        white++;
                    }
                    else if (_myArray[i, j] == false)
                    {
                        black++;
                    }

                    if (white > maxRows / 2 || black > maxRows / 2)
                    {
                        return false;
                    }

                }
            }
            return true;

        }

        /// <summary>
        /// only 2 fields which are consecutive can have the same color
        /// </summary>
        /// <returns></returns>
        public bool Max2RowCheck()
        {

            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 1; j < maxColumns - 1; j++)
                {

                    if (new[] { _myArray[i, j - 1], _myArray[i, j + 1] }.All(x => x == _myArray[i, j] && _myArray[i, j] != null))
                    {

                        return false;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// //only 2 fields which are consecutive can have the same color
        /// </summary>
        /// <returns></returns>
        public bool Max2ColumnCheck()
        {
            for (int j = 0; j < maxRows; j++)
            {

                for (int i = 1; i < maxColumns - 1; i++)
                {

                    if (new[] { _myArray[i - 1, j], _myArray[i + 1, j] }.All(x => x == _myArray[i, j] && _myArray[i, j] != null))
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        /// <summary>
        /// true = No rule violation
        /// false = the is a rule violation
        /// </summary>
        /// <returns></returns>
        public bool ContainsRuleViolation()
        {
            return RowNumberCheck() && ColumnNumberCheck() && Max2RowCheck() && Max2ColumnCheck();
        }

        #endregion

        /// <summary>
        /// check if the solotion is right
        /// </summary>
        /// <returns></returns>
        public bool Solved()
        {
            if (assignmentStragey.GetNextAssignment(this) != null || ContainsRuleViolation() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Assign(AssignmentResult assignmentResult, bool color)
        {
            unassignedPositions.Remove(new Tuple<int, int>(assignmentResult.i, assignmentResult.j));
            _myArray[assignmentResult.i, assignmentResult.j] = color;
            //myCanvas.Dispatcher.Invoke(() =>
            //{
            //    rectangles[assignmentResult.i, assignmentResult.j].Fill = color == true ? Brushes.White : Brushes.Black;
            //}, System.Windows.Threading.DispatcherPriority.Render);

        }

        private void UnAssign(AssignmentResult assignmentResult)
        {
            unassignedPositions.Add(new Tuple<int, int>(assignmentResult.i, assignmentResult.j));
            _myArray[assignmentResult.i, assignmentResult.j] = null;
            //myCanvas.Dispatcher.Invoke(() =>
            //{
            //    rectangles[assignmentResult.i, assignmentResult.j].Fill = Brushes.Gray;
            //}, System.Windows.Threading.DispatcherPriority.Render);

        }


        public List<AssignmentResult> UnitPropagation()
        {
            List<AssignmentResult> assignments = new List<AssignmentResult>();
            return assignments;
            bool hasFoundSomething = false;

            do
            {
                hasFoundSomething = false;

                for (int i = 0; i < maxRows; i++)
                {
                    for (int j = 0; j < maxColumns; j++)
                    {
                        if (j > 2 && _myArray[i, j] == null)
                        {
                            //todo check if we can do it 
                            if (_myArray[i, j - 2] == _myArray[i, j - 1] == true)
                            {
                                hasFoundSomething = true;
                                AssignmentResult result = new AssignmentResult()
                                {
                                    i = i,
                                    j = j,
                                    color = false
                                };


                                Assign(result, false);
                            }
                        }
                    }

                }
            }
            while (hasFoundSomething);

            return assignments;
        }



        /// <summary>
        /// Solve the puzzle
        /// </summary>
        /// <returns></returns>
        public bool Solve()
        {
            //optimization: some rule violations can be detected before everything is complete

            _backtracks++;
            Console.WriteLine("Backtrack: " + _backtracks);

            if (!ContainsRuleViolation())
            {
                return false;
            }

            List<AssignmentResult> unitResults = UnitPropagation();

            //choose variable to assign
            AssignmentResult assignmentResult = assignmentStragey.GetNextAssignment(this);

            //Thread.Sleep(1000);

            if (assignmentResult == null)
            {

                return ContainsRuleViolation();
            }

            Assign(assignmentResult, assignmentResult.color);

            if (!Solve())
            {
                unitResults.ForEach(result => UnAssign(result));
                Assign(assignmentResult, !assignmentResult.color);
                if (!Solve())
                {
                    unitResults.ForEach(result => UnAssign(result));
                    UnAssign(assignmentResult);
                    return false;
                }
                else
                {
                    return Solve();
                }

            }
            else
            {
                return Solve();
            }

        }

        public void InitFile(string size)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 3);

            // if there is only one version open: {size}_1.txt"
            try
            {
                _myArray = OpenFile($@"C:\Temp\{size}_{randomNumber}.txt");
                _initalArray = _myArray.Clone() as Nullable<Boolean>[,];
            }
            catch
            {
                _myArray = OpenFile($@"C:\Temp\{size}_1.txt");
                _initalArray = _myArray.Clone() as Nullable<Boolean>[,];
            }
            rectangles = new Rectangle[maxRows, maxColumns];

        }

        public Nullable<Boolean>[,] OpenFile(string path)
        {
            //Random random = new Random();
            //int randomNumber = random.Next(1, 3);
            //Console.WriteLine("random " + randomNumber);

            //// todo, delete this:
            //String[] puzzle = new String[]
            //{
            //    "small_1.txt","small_2.txt",
            //};

            ////$@"Resources\Puzzle\small_{randomNumber}.txt"
            ///
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
                        unassignedPositions.Add(new Tuple<int, int>(i, j));
                    }

                }
            }
            return resultArray;




        }


    }

}
