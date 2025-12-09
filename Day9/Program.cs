var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();
var areas = new List<(string, double)>();

for (var i = 0; i < coordinates.Length; i++)
{
    for (var j = 0; j < coordinates.Length; j++)
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

result = (long)areas.First().Item2;

Console.WriteLine(result);