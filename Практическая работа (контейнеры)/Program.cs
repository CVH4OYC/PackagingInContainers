using System.Diagnostics;
using Практическая_работа__контейнеры_;
using Практическая_работа__контейнеры_.Algorithms;
using Практическая_работа__контейнеры_.Helpers;
using Практическая_работа__контейнеры_.Models;

Console.WriteLine("Выберите режим работы программы:");
Console.WriteLine("1 - Решение задачи");
Console.WriteLine("2 - Экспериментальная часть");
Console.Write("Введите номер режима: ");
int mode = int.Parse(Console.ReadLine());

if (mode == 1)
{
    Solve();
}
else
{
    var experiment = new Experiment(maxTimeInMinutes: 3);
    experiment.RunExperiment();
}


static void Solve()
{
    var (items, M) = GetInputData();

    Console.WriteLine("\nРешение задачи упаковки:");

    var sw = Stopwatch.StartNew();
    var bruteForceSolution = BruteForceFF.Run(items, M);
    sw.Stop();
    DisplayResultHelper.DisplayResult("Brute Force решение", bruteForceSolution);
    Console.WriteLine($"Время выполнения Brute Force: {sw.ElapsedMilliseconds} мс\n");

    sw.Restart();
    var firstFitSolution = FirstFit.Run(items, M);
    sw.Stop();
    DisplayResultHelper.DisplayResult("First Fit решение", firstFitSolution);
    Console.WriteLine($"Время выполнения First Fit: {sw.ElapsedMilliseconds} мс\n");

    sw.Restart();
    var bestFitSolution = BestFit.Run(items, M);
    sw.Stop();
    DisplayResultHelper.DisplayResult("Best Fit решение", bestFitSolution);
    Console.WriteLine($"Время выполнения Best Fit: {sw.ElapsedMilliseconds} мс\n");
}

static (List<Item> items, int M) GetInputData()
{
    Console.WriteLine("Выберите источник данных:");
    Console.WriteLine("1. Ручной ввод");
    Console.WriteLine("2. Чтение из файла");
    Console.Write("Введите номер источника: ");
    int choice = int.Parse(Console.ReadLine());

    if (choice == 1)
    {
        Console.Write("Введите вместимость контейнера: ");
        int M = int.Parse(Console.ReadLine());

        Console.Write("Введите массы предметов: ");
        var input = Console.ReadLine();

        var separators = new char[] { ' ', ',', ';' };

        var items = input.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                         .Select((value, index) => new Item(int.Parse(value), index + 1))
                         .ToList();

        return (items, M);
    }
    else if (choice == 2)
    {
        Console.Write("Введите путь к файлу: ");
        string filePath = Console.ReadLine();

        var (Items, M) = DataInput.ReadDataFromFile(filePath);

        if (Items.Count == 0)
        {
            Console.WriteLine("Ошибка при чтении данных из файла. Проверьте содержимое файла.");
            Environment.Exit(1);
        }

        Console.WriteLine($"Данные успешно загружены из файла");
        return (Items, M);
    }
    else
    {
        Console.WriteLine("Некорректный выбор. Завершение программы.");
        Environment.Exit(1);
        return (new List<Item>(), 0);
    }
}
