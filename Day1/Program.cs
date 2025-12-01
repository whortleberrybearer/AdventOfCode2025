var input = File.ReadAllLines("Input.txt");

var position = 50;
var result = 0;

foreach (var turn in input)
{
    var direction = turn[0].ToString();
    var amount = int.Parse(turn.Substring(1));

    result += amount / 100;
    amount %= 100;

    if (direction == "R")
    {
        position += amount;

        while (position > 99)
        {
            position -= 100;
            result++;
        }
    }
    else if (direction == "L")
    {
        var isZero = position == 0;
        position -= amount;

        if (position == 0)
        {             
            result++;
            continue;
        }

        while (position < 0)
        {
            position += 100;

            if (!isZero)
            {
                result++;
            }
        }
    }
}

Console.WriteLine(result);