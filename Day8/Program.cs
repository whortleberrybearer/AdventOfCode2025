using System;

var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray()).ToArray();
var distances = new List<(string, double)>();

for (var i = 0; i < coordinates.Length; i++)
{
    var shortest = ("", double.MaxValue);

    for (var j = 0; j < coordinates.Length; j++)
    {
        if (i == j)
        {
            continue;
        }

        var distance = Math.Sqrt(coordinates[i].Zip(coordinates[j], (a, b) => (a - b) * (a - b)).Sum());

        //if (distance < shortest.Item2)
        //{
        //    shortest = ($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", distance);
        //}

        distances.Add(($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", distance));
    }

    //distances.Add(shortest);
}

distances = distances.OrderBy(d => d.Item2).ToList();

var circuits = new List<List<string>>();
var connections = 0;

while (connections < 10)
{
    var junctionBoxes = distances.First().Item1.Split('-');
    var circuit = circuits.FirstOrDefault(c => c.Contains(junctionBoxes[0]));

    if (circuit is null)
    {
        circuit = circuits.FirstOrDefault(c => c.Contains(junctionBoxes[1]));
    }

    var connection = false;

    if (circuit is null)
    {
        circuits.Add(new List<string>(junctionBoxes));
        connection = true;
    }
    else
    {
        if (!circuit.Contains(junctionBoxes[0]))
        {
            circuit.Add(junctionBoxes[0]);
            connection = true;
        }

        if (!circuit.Contains(junctionBoxes[1]))
        {
            circuit.Add(junctionBoxes[1]);
            connection = true;
        }
    }

    if (connection)
    {
        connections++;
    }

    distances.Remove(distances.First());

    var x = distances.FirstOrDefault(d => d.Item1.StartsWith(junctionBoxes[1]));

    if (x != default)
    {
        distances.Remove(x);
    }
}

var top3 = circuits.OrderByDescending(c => c.Count).Take(3).ToArray();

result = top3[0].Count * top3[1].Count * top3[2].Count;

Console.WriteLine(result);