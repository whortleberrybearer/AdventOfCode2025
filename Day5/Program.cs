using System.Linq;

var input = File.ReadAllLines("Input.txt");

var freshIngredientIds = input.TakeWhile(x => x != string.Empty).ToArray();
var availableIngredientIds = input.Skip(freshIngredientIds.Length + 1).Select(i => long.Parse(i)).ToArray();

var result = 0;

var freshIngredientRanges = new List<(long, long)>();

foreach (var ingredientIds in freshIngredientIds)
{
    var split = ingredientIds.Split('-');

    freshIngredientRanges.Add((long.Parse(split[0]), long.Parse(split[1])));
}

freshIngredientRanges = freshIngredientRanges.OrderBy(r => r.Item1).ToList();

foreach (var ingredientId in availableIngredientIds)
{
    foreach (var ingredientRange in freshIngredientRanges)
    {
        if (ingredientRange.Item1 > ingredientId)
        {
            break;
        }

        if ((ingredientId >= ingredientRange.Item1) && (ingredientId <= ingredientRange.Item2))
        {
            Console.WriteLine($"Ingredient: {ingredientId} is in range {ingredientRange.Item1}-{ingredientRange.Item2}");

            result++;
            break;
        }
    }
}

Console.WriteLine(result);