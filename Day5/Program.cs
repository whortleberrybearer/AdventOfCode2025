var input = File.ReadAllLines("Input.txt");

var freshIngredientIds = input.TakeWhile(x => x != string.Empty).ToArray();
var availableIngredientIds = input.Skip(freshIngredientIds.Length + 1).Select(i => long.Parse(i)).ToArray();

var result = 0L;

var freshIngredientRanges = new List<(long, long)>();

foreach (var ingredientIds in freshIngredientIds)
{
    var split = ingredientIds.Split('-');

    freshIngredientRanges.Add((long.Parse(split[0]), long.Parse(split[1])));
}

freshIngredientRanges = freshIngredientRanges.OrderBy(r => r.Item1).ToList();

/*foreach (var ingredientId in availableIngredientIds)
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
}*/

var inRange = freshIngredientRanges.ToList();

for (var i = 1; i < inRange.Count; i++)
{
    for (var j = 0; j < i; j++)
    {
        // Check if can extend the end.
        if ((inRange[i].Item1 >= inRange[j].Item1) && (inRange[i].Item1 <= inRange[j].Item2))
        {
            // Start is in the previous range, so can extend the end of it to the current value.
            if (inRange[i].Item2 > inRange[j].Item2)
            {
                Console.WriteLine($"Range {inRange[i].Item1}-{inRange[i].Item2} extends {inRange[j].Item1}-{inRange[j].Item2}");

                var newRange = (inRange[j].Item1, inRange[i].Item2);
                inRange.RemoveAt(j);
                inRange.Insert(j, newRange);
            }

            inRange.RemoveAt(i);
            i--;

            break;
        }
    }
}

result = inRange.Select(r => (r.Item2 - r.Item1) + 1).Sum();

Console.WriteLine(result);