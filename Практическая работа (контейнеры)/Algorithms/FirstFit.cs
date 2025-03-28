using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_.Algorithms;

public static class FirstFit
{
    public static List<List<int>> Run(List<Item> items, int containerCapacity)
    {
        var containers = new List<List<Item>>();

        foreach (var item in items)
        {
            bool placed = false;

            foreach (var container in containers)
            {
                if (container.Sum(i => i.Weight) + item.Weight <= containerCapacity)
                {
                    container.Add(item);
                    placed = true;
                    break;
                }
            }

            if (!placed)
            {
                containers.Add(new List<Item> { item });
            }
        }

        return containers
                .Select(container => container.Select(item => item.Index).ToList())
                .ToList();
    }

}
