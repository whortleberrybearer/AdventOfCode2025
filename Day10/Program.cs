var input = File.ReadAllLines("Input.txt");

var result = 0L;

var machines = new List<(char[] Lights, int[][] Buttons, int[] Joltage)>();

foreach (var line in input)
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var lights = split[0].Trim('[', ']').ToCharArray();
    var buttons = split[1..^1].Select(s => s.Trim('(', ')').Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
    var joltage = split[^1].Trim('{', '}').Split(',').Select(c => int.Parse(c)).ToArray();
    
    machines.Add((lights, buttons, joltage));
}



Console.WriteLine(result);