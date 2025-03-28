using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_;

public static class TestGenerator
{
    public static List<Item> GenerateItems(int count, int maxWeight)
    {
        var items = new List<Item>();
        var random = new Random();

        for (int i = 0; i < count; i++)
        {
            items.Add(new Item(random.Next(1, maxWeight + 1), i + 1));
        }

        return items;
    }
}
