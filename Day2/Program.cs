var input = File.ReadAllLines("Input.txt").First().Split(",");
var result = 0L;

foreach (var range in input)
{
    var rangeSplit = range.Split("-");

    Console.WriteLine($"Range: {rangeSplit[0]}-{rangeSplit[1]}");

    if (rangeSplit[0].Length % 2 == 1)
    {
        if (rangeSplit[0].Length == rangeSplit[1].Length)
        {
            Console.WriteLine("Has is odd length and contains do duplicates");
            continue;
        }

        rangeSplit[0] = $"1{string.Join(string.Empty, Enumerable.Repeat("0", rangeSplit[0].Length))}";

        Console.WriteLine($"New range start: {rangeSplit[0]}");
    }

    var halfRangeStart = long.Parse(rangeSplit[0].Substring(0, rangeSplit[0].Length / 2));

    var rangeStart = long.Parse(rangeSplit[0]);
    var rangeEnd = long.Parse(rangeSplit[1]);

    while (true)
    {
        var duplicate = long.Parse($"{halfRangeStart}{halfRangeStart}");

        if (duplicate <= rangeEnd)
        {
            if (duplicate >= rangeStart)
            {
                Console.WriteLine($"Invalid: {duplicate}");
                result += duplicate;
            }

            halfRangeStart++;
        }
        else
        {
            break;
        }
    }
}

Console.WriteLine(result);