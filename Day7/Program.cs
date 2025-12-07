var input = File.ReadAllLines("Input.txt");

var result = 0L;

var grid = input.Select(i => i.ToCharArray()).ToArray();

var startPoisition = input.First().IndexOf('S');
var beams = new Dictionary<(int, int), long>() { { (0, startPoisition), 1 } };

while (beams.Count > 0)
{
    var newBeams = new Dictionary<(int, int), long>();

    foreach (var beam in beams)
    {
        if (beam.Key.Item1 >= (grid.Length - 1))
        {
            result += beam.Value;

            continue;
        }

        if (newBeams.TryGetValue((beam.Key.Item1 + 1, beam.Key.Item2), out var count))
        {
            newBeams[(beam.Key.Item1 + 1, beam.Key.Item2)] = count + beam.Value;
        }
        else if (grid[beam.Key.Item1 + 1][beam.Key.Item2] == '.')
        {
            newBeams.Add((beam.Key.Item1 + 1, beam.Key.Item2), beam.Value);
        }
        else if (grid[beam.Key.Item1 + 1][beam.Key.Item2] == '^')
        {
            if (newBeams.TryGetValue((beam.Key.Item1 + 1, beam.Key.Item2 - 1), out var count1))
            {
                newBeams[(beam.Key.Item1 + 1, beam.Key.Item2 - 1)] = count1 + beam.Value;
            }
            else
            {
                newBeams.Add((beam.Key.Item1 + 1, beam.Key.Item2 - 1), beam.Value);
            }

            if (newBeams.TryGetValue((beam.Key.Item1 + 1, beam.Key.Item2 + 1), out var count2))
            {
                newBeams[(beam.Key.Item1 + 1, beam.Key.Item2 + 1)] = count2 + beam.Value;
            }
            else
            {
                newBeams.Add((beam.Key.Item1 + 1, beam.Key.Item2 + 1), beam.Value);
            }
        }
    }

    beams = newBeams;
}

Console.WriteLine(result);