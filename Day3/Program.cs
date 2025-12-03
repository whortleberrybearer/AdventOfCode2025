var input = File.ReadAllLines("Input.txt");
var result = 0L;

foreach (var bank in input)
{
    Console.WriteLine(bank);

    var barreries = bank.Select(b => int.Parse(b.ToString())).Reverse().ToArray();
    var largestIndex1 = 0;
    var largestIndex2 = 0;
    var largestValue1 = 0;
    var largestValue2 = 0;

    for (var i = 1; i < barreries.Length; i++)
    {
        if (barreries[i] >= largestValue1)
        {
            largestIndex1 = i;
            largestValue1 = barreries[i];
        }
    }

    for (var i = 0; i < largestIndex1; i++)
    {
        if (barreries[i] >= largestValue2)
        {
            largestIndex2 = i;
            largestValue2 = barreries[i];
        }
    }

    Console.WriteLine($"Batteries: {largestValue1}{largestValue2}");
    result += (largestValue1 * 10) + largestValue2;
}

Console.WriteLine(result);