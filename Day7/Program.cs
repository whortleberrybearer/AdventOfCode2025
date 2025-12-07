var input = File.ReadAllLines("Input.txt");

var result = 0L;

var grid = input.Select(i => i.ToCharArray()).ToArray();

var startPoisition = input.First().IndexOf('S');
var beams = new List<(int, int)>() { (0, startPoisition) };

while (beams.Count > 0)
{
    var newBeams = new List<(int, int)>();
    foreach (var beam in beams)
    {
        if (beam.Item1 >= (grid.Length - 1))
        {
            // Reached the end;
            continue;
        }

        if (grid[beam.Item1 + 1][beam.Item2] == '.')
        {
            grid[beam.Item1 + 1][beam.Item2] = '|';

            newBeams.Add((beam.Item1 + 1, beam.Item2));
        }
        else if (grid[beam.Item1 + 1][beam.Item2] == '^')
        {
            var split = false;

            if (grid[beam.Item1 + 1][beam.Item2 - 1] == '.')
            {
                grid[beam.Item1 + 1][beam.Item2 - 1] = '|';

                newBeams.Add((beam.Item1 + 1, beam.Item2 - 1));
                
                split = true;
            }

            if (grid[beam.Item1 + 1][beam.Item2 + 1] == '.')
            {
                grid[beam.Item1 + 1][beam.Item2 + 1] = '|';

                newBeams.Add((beam.Item1 + 1, beam.Item2 + 1));

                split = true;
            }

            if (split)
            {
                result++;
            }
        }
    }

    beams = newBeams;
}

Console.WriteLine(result);