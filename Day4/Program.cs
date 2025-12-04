var input = File.ReadAllLines("Input.txt");
var result = 0;
var rollsToRemove = new List<(int, int)>();

do
{
    rollsToRemove.Clear();

    for (var i = 0; i < input.Length; i++)
    {
        for (var j = 0; j < input[i].Length; j++)
        {
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

            var rollLocations = new List<(int, int)>();

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
                        rollLocations.Add((x, y));
                    }
                }
            }

            if (rollLocations.Count < 4)
            {
                result++;
                rollsToRemove.Add((i, j));
            }
        }
    }

    foreach (var roll in rollsToRemove)
    {
        var chars = input[roll.Item1].ToCharArray();
        chars[roll.Item2] = '.';
        input[roll.Item1] = new string(chars);       
    }

    Console.WriteLine($"End pass: {rollsToRemove.Count()}, Result: {result}");
}
while (rollsToRemove.Count > 0);

Console.WriteLine(result);