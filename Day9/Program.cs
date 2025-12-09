var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();
var distances = new List<(string, double)>();

for (var i = 0; i < coordinates.Length; i++)
{
    for (var j = 0; j < coordinates.Length; j++)
    {
        if (i == j)
        {
            continue;
        }

        var distance = Math.Sqrt(coordinates[i].Zip(coordinates[j], (a, b) => (a - b) * (a - b)).Sum());

        distances.Add(($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", distance));
    }
}

distances = distances.OrderByDescending(d => d.Item2).ToList();

var points = distances.First().Item1.Split('-').Select(p => p.Split(',').Select(x => long.Parse(x)).ToArray()).ToArray();

result = (Math.Abs(points[0][0] - points[1][0]) + 1) * (Math.Abs(points[0][1] - points[1][1]) + 1);

Console.WriteLine(result);

// 4414728500 = TOO LOW