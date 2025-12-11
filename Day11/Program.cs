var input = File.ReadAllLines("Input.txt");

var result = 0L;

var devices = new Dictionary<string, Device>();

foreach (var line in input)
{
    var split = line.Split(':');

    devices.Add(split[0], new Device() { Name = split[0] });
}

devices.Add("out", new Device() { Name = "out" });

foreach (var line in input)
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    foreach (var device in split.Skip(1))
    {
        devices[split[0].TrimEnd(':')].Outputs.Add(devices[device]);
    }    
}

result = devices["you"].FollowOutput(new List<Device>());

Console.WriteLine(result);

class Device
{
    public string Name { get; set; }
    public List<Device> Outputs = new List<Device>();

    internal long FollowOutput(List<Device> previousDevices)
    {
        if (previousDevices.Contains(this))
        {
            return 0;
        }

        if (Outputs.Any(o => o.Name == "out"))
        {
            Console.WriteLine($"{string.Join("->", previousDevices.Select(d => d.Name))}->{Name}->out");

            return 1;
        }

        previousDevices = new List<Device>(previousDevices) { this };

        return Outputs.Sum(o => o.FollowOutput(previousDevices));
    }
}

class Expansion
{
    public Device Device { get; set; }
}