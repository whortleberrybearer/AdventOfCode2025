var input = File.ReadAllLines("Input.txt");

var result = 0L;

var grid = input.Take(input.Length - 1).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();

var operators = input.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

for (var col = 0; col < operators.Length; col++)
{
    var numbers = grid.Select(r => r[col]).ToArray();

    if (operators[col] == "+")
    {
        var total = 0L;

        foreach (var value in numbers)
        {
            total += value;
        }

        Console.WriteLine($"Column: {col}, Total: {total}");

        result += total;
    }
    else if (operators[col] == "*")
    {
        var total = numbers.First();

        foreach (var value in numbers.Skip(1))
        {
            total *= value;
        }

        Console.WriteLine($"Column: {col}, Total: {total}");

        result += total;
    }
}

Console.WriteLine(result);