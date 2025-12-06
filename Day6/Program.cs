var input = File.ReadAllLines("Input.txt");

var result = 0L;

/*var grid = input.Take(input.Length - 1).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();

var operators = input.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

for (var col = 0; col < operators.Length; col++)
{
    var numbers = grid.Where(r => col < r.Length).Select(r => r[col]).ToArray();

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
}*/

var columnIndexes = new List<int>();
var index = -1;

do
{
    index = input.Last().IndexOfAny(new[] { '+', '*' }, index + 1);

    if (index > -1)
    {
        columnIndexes.Add(index);
    }
}
while (index > -1);

columnIndexes.Add(input.First().Length + 1);

var grid = input.Take(input.Length - 1).Select(x => x.ToCharArray()).ToArray();

for (var i = 0; i < columnIndexes.Count - 1; i++)
{
    var numbers = new List<long>();

    for (var col = columnIndexes[i]; col < columnIndexes[i + 1] - 1; col++)
    {
        numbers.Add(long.Parse(new string(grid.Where(r => r[col] != ' ').Select(r => r[col]).ToArray())));
    }

    if (input.Last()[columnIndexes[i]] == '+')
    {
        var total = 0L;

        foreach (var value in numbers)
        {
            total += value;
        }

        Console.WriteLine($"{string.Join("+", numbers)} = {total}");

        result += total;
    }
    else if (input.Last()[columnIndexes[i]] == '*')
    {
        var total = numbers.First();

        foreach (var value in numbers.Skip(1))
        {
            total *= value;
        }

        Console.WriteLine($"{string.Join("*", numbers)} = {total}");

        result += total;
    }
}

Console.WriteLine(result);