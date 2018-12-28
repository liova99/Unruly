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



























