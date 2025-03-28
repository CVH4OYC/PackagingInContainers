namespace Практическая_работа__контейнеры_.Helpers;

class DisplayResultHelper
{
    public static void DisplayResult(string algorithmName, List<List<int>> packing)
    {
        Console.WriteLine($"\n{algorithmName}:");
        for (int i = 0; i < packing.Count; i++)
        {
            Console.WriteLine($"Контейнер {i + 1}: {string.Join(", ", packing[i])}");
        }
    }
}
