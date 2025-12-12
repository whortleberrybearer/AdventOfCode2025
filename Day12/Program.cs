
var input = File.ReadAllLines("Input.txt");

var result = 0L;

var shapes = new List<Shape>();
var regions = new List<Region>();

for (var i = 0; i < input.Length; i++)
{
    if (input[i].EndsWith(':'))
    {
        var shapeLines = input.Skip(i + 1).TakeWhile(i => i != string.Empty).Select(c => c.ToCharArray()).ToArray();

        shapes.Add(new Shape()
        {
            Chars = shapeLines
        });

        i += shapeLines.Length;
    }
    else if (input[i].Contains('x'))
    {
        var split = input[i].Split(':');
        var size = split[0].Split('x');

        regions.Add(new Region()
        {
            Width = int.Parse(size[0]),
            Height = int.Parse(size[1]),
            ShapeQuantities = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList()
        });
    }
}

foreach (var region in regions)
{
    if (!region.IsPossibleToFit(shapes))
    {
        Console.WriteLine($"{region.Width}x{region.Height}: No");
    }
    else
    {
        Console.WriteLine($"{region.Width}x{region.Height}: Yes");
        result += 1;
    }
}

Console.WriteLine(result);

class Shape
{
    public char[][] Chars { get; set; }
    public int Size => Chars.Sum(w => w.Count(h => h == '#'));
}

class Region
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<int> ShapeQuantities { get; set; }

    internal bool IsPossibleToFit(List<Shape> shapes)
    {
        var totalSize = 0L;

        for (int i = 0; i < ShapeQuantities.Count; i++)
        {
            totalSize += shapes[i].Size * ShapeQuantities[i];
        }

        if (totalSize > (Width * Height))
        {
            // The total number of blocks exceeds the space.
            return false;
        }

        return true;
    }
}
