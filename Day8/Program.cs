using System;
using System.Collections.Generic;
using System.Linq;

var input = File.ReadAllLines("Input.txt");

var result = 0L;

var coordinates = input.Select(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToArray()).ToArray();
var distances = new List<(string, double)>();
var distancesDictionary = new Dictionary<string, (string, double)[]>();

for (var i = 0; i < coordinates.Length; i++)
{
    var linkLengths = new List<(string, double)>();

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
        linkLengths.Add((string.Join(',', coordinates[j]), distance));
    }

    distancesDictionary.Add(string.Join(',', coordinates[i]), linkLengths.OrderBy(l => l.Item2).ToArray());
}

distances = distances.OrderBy(d => d.Item2).ToList();

var circuits = input.Select(i => new List<string>() { i }).ToList();
var connections = 0;

foreach (var distance in distances)
{
    Console.WriteLine($"Connections: {connections}");

    var junctionBoxes = distance.Item1.Split('-');

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

        connections++;

        if (connections == 9)
        {
            break;
        }
    }

    //foreach (var additionalDelete in distancesDictionary[junctionBoxes[1]].ToArray())
    //{
    //    distances.Remove(($"{}-{}", additionalDelete);
    //}
}

var top3 = circuits.OrderByDescending(c => c.Count).Take(3).ToArray();

result = top3[0].Count * top3[1].Count * top3[2].Count;

Console.WriteLine(result);

// 2145 = too low
// 2688 = too low