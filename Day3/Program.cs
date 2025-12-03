var input = File.ReadAllLines("Input.txt");
var result = 0L;

foreach (var bank in input)
{
    Console.WriteLine(bank);

    var barreries = bank.Select(b => int.Parse(b.ToString())).Reverse().ToArray();

    var (largestValue1, largestIndex1) = FindLargestValue(barreries, 1, barreries.Length);
    var (largestValue2, largestIndex2) = FindLargestValue(barreries, 0, largestIndex1);

    Console.WriteLine($"Batteries: {largestValue1}{largestValue2}");
    result += (largestValue1 * 10) + largestValue2;
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