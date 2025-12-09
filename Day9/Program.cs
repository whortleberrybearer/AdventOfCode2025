var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();

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

    for (var j = startX; j <= endX; j++)
    {
        grid[startY][j] = 'X';
    }

    for (var j = startY; j <= endY; j++)
    {
        grid[j][startX] = 'X';
    }
}

var start = (0, 0);

for (var i = 1; i < grid.Length; i++)
{
    for (var j = 1; j < grid[i].Length; j++)
    {
        if (grid[i][j] != '.')
        {
            continue;
        }

        if ((grid[i - 1][j] != '.') && (grid[i][j - 1] != '.'))
        {
            start = (i, j);
            break;
        }
    }

    if (start != (0, 0))
    {
        break;
    }
}

var expand = new List<(int, int)>() { start };

while (expand.Any())
{
    Console.WriteLine($"To expand: {expand.Count}");

    var newExpand = new List<(int, int)>();

    foreach (var coordinate in expand)
    {
        if (grid[coordinate.Item1][coordinate.Item2] == '.')
        {
            grid[coordinate.Item1][coordinate.Item2] = 'X';

            if ((coordinate.Item1 > 0) && (grid[coordinate.Item1 - 1][coordinate.Item2] == '.'))
            {
                newExpand.Add((coordinate.Item1 - 1, coordinate.Item2));
            }

            if ((coordinate.Item1 < maxY - 1) && (grid[coordinate.Item1 + 1][coordinate.Item2] == '.'))
            {
                newExpand.Add((coordinate.Item1 + 1, coordinate.Item2));
            }

            if ((coordinate.Item2 > 0) && (grid[coordinate.Item1][coordinate.Item2 - 1] == '.'))
            {
                newExpand.Add((coordinate.Item1, coordinate.Item2 - 1));
            }

            if ((coordinate.Item2 < maxX - 1) && (grid[coordinate.Item1][coordinate.Item2 + 1] == '.'))
            {
                newExpand.Add((coordinate.Item1, coordinate.Item2 + 1));
            }
        }
    }

    expand = newExpand.Distinct().ToList();
}


for (var i = 0; i < grid.Length; i++)
{
    for (var j = 0; j < grid[i].Length; j++)
    {
        Console.Write(grid[i][j]);
    }

    Console.WriteLine();
}

var areas = new List<(string, double)>();

for (var i = 0; i < coordinates.Length; i++)
{
    for (var j = i + 1; j < coordinates.Length; j++)
    {
        if (i == j)
        {
            continue;
        }

        var area = (Math.Abs(coordinates[j][0] - coordinates[i][0]) + 1) * (Math.Abs(coordinates[j][1] - coordinates[i][1]) + 1);

        areas.Add(($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", area));
    }
}

areas = areas.OrderByDescending(d => d.Item2).ToList();

while (areas.Count > 1)
{
    Console.WriteLine($"Options: {areas.Count}");

    var area = areas[(areas.Count - 1) / 2];

    var coords = area.Item1.Split('-').Select(a => a.Split(',').Select(c => long.Parse(c)).ToArray()).ToArray();

    var startX = Math.Min(coords[0][0], coords[1][0]);
    var endX = Math.Max(coords[0][0], coords[1][0]);
    var startY = Math.Min(coords[0][1], coords[1][1]);
    var endY = Math.Max(coords[0][1], coords[1][1]);

    var invalid = false;

    for (var y = startY; y <= endY; y++)
    {
        for (var x = startX; x <= endX; x++)
        {
            if (grid[y][x] == '.')
            {
                invalid = true;
                break;
            }
        }

        if (invalid)
        {
            break;
        }
    }

    if (!invalid)
    {
        areas.RemoveRange((areas.Count / 2) + 1, areas.Count - (areas.Count / 2) - 1);
    }
    else
    {
        areas.Remove(area);
    }
}

result = (long)areas.First().Item2;

Console.WriteLine(result);