using System.Text;

var input = File.ReadAllLines("Input.txt");

var result = 0L;

var machines = new List<(bool[] Lights, int[][] Buttons, int[] Joltage)>();

foreach (var line in input)
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var lights = split[0].Trim('[', ']').Select(c => c == '#').ToArray();
    var buttons = split[1..^1].Select(s => s.Trim('(', ')').Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
    var joltage = split[^1].Trim('{', '}').Split(',').Select(c => int.Parse(c)).ToArray();
    
    machines.Add((lights, buttons, joltage));
}

foreach (var machine in machines)
{
    var expansions = new List<Expansion>();
    var previousExpansions = new List<string>();

    foreach (var button in machine.Buttons)
    {
        expansions.Add(new Expansion()
        {
            CurrentLights = Enumerable.Repeat(false, machine.Lights.Length).ToArray(),
            ButtonToPress = button,
            PreviousButtonPresses = new()
        });
    }

    Expansion? shortestExpansion = null;

    do
    {
        Console.WriteLine($"To expand: {expansions.Count}");

        expansions.Sort((a, b) => a.PreviousButtonPresses.Count - b.PreviousButtonPresses.Count);

        foreach (var expansion in expansions.ToArray())
        {
            expansions.Remove(expansion);

            expansion.PressButton();

            if (expansion.IsEndState(machine.Lights))
            {
                shortestExpansion = expansion;
                break;
            }

            var stateString = expansion.ToStateString();

            if (!previousExpansions.Contains(stateString))
            {
                previousExpansions.Add(stateString);

                foreach (var button in machine.Buttons)
                {
                    expansions.Add(new Expansion()
                    {
                        CurrentLights = expansion.CurrentLights.Select(l => l).ToArray(),
                        ButtonToPress = button,
                        PreviousButtonPresses = new List<int[]>(expansion.PreviousButtonPresses)
                    });
                }
            }
        }
    }
    while (shortestExpansion is null);

    Console.WriteLine($"Shortest expansion: {shortestExpansion.Value.PreviousButtonPresses.Count}");

    result += shortestExpansion.Value.PreviousButtonPresses.Count;
}

Console.WriteLine(result);

struct Expansion
{
    public bool[] CurrentLights;
    public int[] ButtonToPress;
    public List<int[]> PreviousButtonPresses;

    public void PressButton()
    {
        for (int i = 0; i < ButtonToPress.Length; i++)
        {
            CurrentLights[ButtonToPress[i]] = !CurrentLights[ButtonToPress[i]];
        }

        PreviousButtonPresses.Add(ButtonToPress);
    }

    public bool IsEndState(bool[] lights)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (CurrentLights[i] != lights[i])
            {
                return false;
            }
        }

        return true;
    }

    public string ToStateString()
    {
        var builder = new StringBuilder();

        for (int i = 0; i < CurrentLights.Length; i++)
        {
            builder.Append(CurrentLights[i] ? "1" : "0");
        }

        return builder.ToString();
    }
}