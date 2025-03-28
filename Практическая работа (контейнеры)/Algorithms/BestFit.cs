using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_.Algorithms;

public class BestFit
{
    public static List<List<int>> Run(List<Item> items, int M)
    {
        var containers = new List<List<Item>>();

        foreach (var item in items)
        {
            int bestIndex = -1;
            int minSpaceLeft = M;

            for (int i = 0; i < containers.Count; i++)
            {
                int currentWeight = containers[i].Sum(i => i.Weight);
                int spaceLeft = M - currentWeight;

                if (spaceLeft >= item.Weight && spaceLeft < minSpaceLeft)
                {
                    bestIndex = i;
                    minSpaceLeft = spaceLeft;
                }
            }

            if (bestIndex != -1)
            {
                containers[bestIndex].Add(item);
            }
            else
            {
                containers.Add(new List<Item> { item });
            }
        }

        return containers
        .Select(container => container.Select(item => item.Index).ToList())
        .ToList();
    }
}
