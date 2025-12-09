using System;
using System.Collections.Generic;
using System.Linq;

var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();
var distances = new List<(string, double)>();

for (var i = 0; i < coordinates.Length; i++)
{
    for (var j = 0; j < coordinates.Length; j++)
    {
        if (i == j)
        {
            continue;
        }

        var distance = Math.Sqrt(coordinates[i].Zip(coordinates[j], (a, b) => (a - b) * (a - b)).Sum());

        distances.Add(($"{string.Join(',', coordinates[i])}-{string.Join(',', coordinates[j])}", distance));
    }
}

distances = distances.OrderBy(d => d.Item2).ToList();

var circuits = input.Select(i => new List<string>() { i }).ToList();
for (var i = 0; i < distances.Count; i += 2)
{
    var junctionBoxes = distances[i].Item1.Split('-');

    var circuit1 = circuits.FirstOrDefault(c => c.Contains(junctionBoxes[0]));
    var circuit2 = circuits.FirstOrDefault(c => c.Contains(junctionBoxes[1]));

    if (circuit1 == circuit2)
    {
        continue;
    }
    else if (circuit1 is not null && circuit2 is not null)
    {
        // Link the circuits
        circuit1.AddRange(circuit2);
        circuits.Remove(circuit2);

        if (circuits.Count == 1)
        {
            result = long.Parse(junctionBoxes[0].Split(',')[0]) * long.Parse(junctionBoxes[1].Split(',')[0]);
            break;
        }
    }
}

//var top3 = circuits.OrderByDescending(c => c.Count).Take(3).ToArray();

//result = top3[0].Count * top3[1].Count * top3[2].Count;

Console.WriteLine(result);