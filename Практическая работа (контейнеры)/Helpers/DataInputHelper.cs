using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_.Helpers;

public class DataInput
{
    public static (List<Item> Items, int M) ReadDataFromFile(string filePath)
    {
        List<Item> items = new List<Item>();
        int M;

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            // Первая строка — вместимость контейнера (M)
            M = int.Parse(lines[0].Trim());

            // Вторая строка — массы предметов
            string[] itemStrings = lines[1].Trim().Split();
            items = itemStrings.Select((value, index) => new Item(int.Parse(value), index + 1)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении данных из файла: {ex.Message}");
            M = 0;
        }

        var itemsWeight = items.Select(i => i.Weight).ToList();
        Console.WriteLine($"Вместимость контейнера: {M}");
        Console.WriteLine("Массы предметов: " + string.Join(", ", itemsWeight));

        return (items, M);
    }
}
