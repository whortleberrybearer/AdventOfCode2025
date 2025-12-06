var input = File.ReadAllLines("Input.txt");

var result = 0L;
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
/*
var operators = input.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

for (var col = 0; col < operators.Length; col++)
{
    var strings = grid.Select(r => r[col]).ToArray();
    var maxLength = strings.Max(s => s.Length);

    strings = strings.Select(s => s.PadLeft(maxLength, ' ')).ToArray();

    var i = 0;

//}
*/

Console.WriteLine(result);