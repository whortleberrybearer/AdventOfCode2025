var input = File.ReadAllLines("Input.txt");

var position = 50;
var result = 0;

foreach (var turn in input)
{
    var direction = turn[0].ToString();
    var amount = int.Parse(turn.Substring(1));

    if (direction == "R")
    {
        position += amount;

        while (position > 99)
        {
            position -= 100;
        }
    }
    else if (direction == "L")
    {
        position -= amount;

        while (position < 0)
        {
            position += 100;
        }
    }

    if (position == 0)
    {
        result++;
    }
}

Console.WriteLine(result);