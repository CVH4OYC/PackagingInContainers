using Практическая_работа__контейнеры_.Helpers;
using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_.Algorithms;

public class BruteForceFF
{
    public static List<List<int>> Run(List<Item> items, int M)
    {
        var permutations = PermutationHelper.GetPermutations(items);
        List<List<int>> containers = new List<List<int>>();
        int minContainers = int.MaxValue;

        foreach (var perm in permutations)
        {
            var result = FirstFit.Run(perm.ToList(), M);
            if (result.Count < minContainers)
            {
                minContainers = result.Count;
                containers = result;
            }
        }

        return containers;
    }
}
