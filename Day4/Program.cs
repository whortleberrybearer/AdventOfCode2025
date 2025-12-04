var input = File.ReadAllLines("Input.txt");
var result = 0;
var grid = new char[input.Length][];

for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        //grid[i] = new char[input[i].Length];

        //grid[i][j] = input[i][j];

        if (input[i][j] != '@')
        {
            continue;
        }

        var startX = j - 1;

        if (startX < 0)
        {
            startX = j;
        }

        var endX = j + 1;

        if (endX > (input[i].Length - 1))
        {
            endX = j;
        }

        var startY = i - 1;

        if (startY < 0)
        {
            startY = i;
        }

        var endY = i + 1;

        if (endY > (input.Length - 1))
        {
            endY = i;
        }

        var rolls = 0;

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                if ((x == j) && (y == i))
                {
                    continue;
                }

                if (input[y][x] == '@')
                {
                    rolls++;
                }
            }
        }

        if (rolls < 4)
        {
            result++;
        }
    }
}

Console.WriteLine(result);