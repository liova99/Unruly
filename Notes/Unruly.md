[TOC]

### Lamda + ClickEvent

```c#
myRect.MouseLeftButtonUp += (sender, args) =>
            {
                MessageBox.Show($"Clicked Rect {i},{j}");
            };
```

> note:

```c#
 public void Click(object sender, MouseButtonEventArgs args)
        {
            //MessageBox.Show($"Clicked Rect {i},{j}");

        }
```



### arrays

https://www.tutorialspoint.com/csharp/csharp_multi_dimensional_arrays.htm



### read File

```c#
try
            {
                string[] lines = File.ReadAllLines(@"Resources\Puzzle\small_1.txt");

                string[] firstLineSplitted = lines[0].Split(' ');

                int n = int.Parse(firstLineSplitted[0]);
                int m = int.Parse(firstLineSplitted[1]);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        char currentChar = lines[i + 1][j];

                        if (currentChar == 'T')
                        {

                        }
                        else if (currentChar == 'F')
                        { }
                    }
                }
            }
            catch
            {

            }
```







### recursion

```c#
        public bool Solve()
        {
            //optimization: some rule violations can be detected before everything is complete

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
```





















