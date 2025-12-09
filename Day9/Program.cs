var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray()).ToArray();
//var areas = new List<(string, double)>();

//for (var i = 0; i < coordinates.Length; i++)
//{
//    for (var j = 0; j < coordinates.Length; j++)
//    {
//        if (i == j)
//        {
//            continue;
//        }

//        var area = (Math.Abs(coordinates[j][0] - coordinates[i][0]) + 1) * (Math.Abs(coordinates[j][1] - coordinates[i][1]) + 1);

//        areas.Add(($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", area));
//    }
//}

//areas = areas.OrderByDescending(d => d.Item2).ToList();

//result = (long)areas.First().Item2;

var maxX = coordinates.Max(x => x[0]) + 3;
var maxY = coordinates.Max(x => x[1]) + 3;

var grid = new char[maxY][];

for (var i = 0; i < maxY; i++)
{
    grid[i] = new char[maxX];

    for (var j = 0; j < maxX; j++)
    {
        grid[i][j] = '.';
    }
}

for (var i = 0; i < coordinates.Length; i++)
{
    grid[coordinates[i][1]][coordinates[i][0]] = '#';
}

for (var i = 0; i < coordinates.Length; i++)
{
    var current = coordinates[i];
    var next = (i < (coordinates.Length - 1)) ? coordinates[i + 1] : coordinates[0];

    var startX = Math.Min(current[0], next[0]);
    var endX = Math.Max(current[0], next[0]);
    var startY = Math.Min(current[1], next[1]);
    var endY = Math.Max(current[1], next[1]);

    for (var j = startX + 1; j < endX; j++)
    {
        grid[startY][j] = 'X';
    }

    for (var j = startY + 1; j < endY; j++)
    {
        grid[j][startX] = 'X';
    }
}

for (var i = 0; i < grid.Length; i++)
{
    for (var j = 0; j < grid[i].Length; j++)
    {
        Console.Write(grid[i][j]);
    }

    Console.WriteLine();
}

Console.WriteLine();

Console.WriteLine(result);