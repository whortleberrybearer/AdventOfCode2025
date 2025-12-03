var input = File.ReadAllLines("Input.txt");
var result = 0L;

foreach (var bank in input)
{
    Console.WriteLine(bank);

    var barreries = bank.Select(b => int.Parse(b.ToString())).Reverse().ToArray();
    var endIndex = barreries.Length;
    var batteriesString = string.Empty;

    for (var i = 1; i >= 0; i--)
    {
        var (largestValue, largestIndex) = FindLargestValue(barreries, i, endIndex);
        endIndex = largestIndex;

        batteriesString += largestValue;
    }

    Console.WriteLine($"Batteries: {batteriesString}");
    result += long.Parse(batteriesString);
}

Console.WriteLine(result);

static (int value, int index) FindLargestValue(int[] barreries, int startIndex, int endIndex)
{
    var index = 0;
    var value = 0;

    for (var i = startIndex; i < endIndex; i++)
    {
        if (barreries[i] >= value)
        {
            index = i;
            value = barreries[i];
        }
    }

    return (value, index);
}