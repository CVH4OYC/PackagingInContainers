using System.Diagnostics;
using Практическая_работа__контейнеры_.Algorithms;
using Практическая_работа__контейнеры_.Helpers;

namespace Практическая_работа__контейнеры_;

public class Experiment
{
    private readonly int MaxTimeInMs;
    private const int CONTAINER_CAPACITY = 20;

    public Experiment(int maxTimeInMinutes = 3)
    {
        MaxTimeInMs = maxTimeInMinutes * 60 * 1000;
    }

    public void RunExperiment()
    {
        Console.WriteLine("Начался поиск оптимального V");
        int Vmax = FindMaxVolume(MaxTimeInMs);
        Console.WriteLine($"Определено значение Vmax: {Vmax}");

        int[] testVolumes = {  Vmax , Vmax / 2, Vmax / 3  };
        Console.WriteLine("\nЗапуск серии тестов на различных объемах данных...");

        foreach (var volume in testVolumes)
        {
            RunTestsForVolume(volume);
        }
    }

    private int FindMaxVolume(int maxTimeInMs)
    {
        int volume = 1;
        while (true)
        {
            Console.WriteLine($"V = {volume}");
            var items = TestGenerator.GenerateItems(volume, CONTAINER_CAPACITY);
            if (volume == 10)
                return volume;
            var timeBruteForce = MeasureWithResult(() => BruteForceFF.Run(items, CONTAINER_CAPACITY)).Time;
            var timeFF = MeasureWithResult(() => FirstFit.Run(items, CONTAINER_CAPACITY)).Time;
            var timeBF = MeasureWithResult(() => BestFit.Run(items, CONTAINER_CAPACITY)).Time;

            if (timeBruteForce > maxTimeInMs || timeFF > maxTimeInMs || timeBF > maxTimeInMs)
                return volume - 1;

            volume += 1;
        }
    }

    private void RunTestsForVolume(int volume)
    {
        const int NumberOfTests = 100;

        double totalTimeBF = 0;
        double totalTimeFF = 0;
        double totalTimeBruteForce = 0;

        int correctSolutionsFF = 0;
        int correctSolutionsBF = 0;

        double totalDeviationFF = 0;
        double totalDeviationBF = 0;

        for (int i = 0; i < NumberOfTests; i++)
        {
            Console.WriteLine($"Тестовый запуск {i + 1} на V = {volume}");
            var items = TestGenerator.GenerateItems(volume, CONTAINER_CAPACITY);

            var resultBrute = MeasureWithResult(() => BruteForceFF.Run(items, CONTAINER_CAPACITY));
            var resultFF = MeasureWithResult(() => FirstFit.Run(items, CONTAINER_CAPACITY));
            var resultBF = MeasureWithResult(() => BestFit.Run(items, CONTAINER_CAPACITY));

            totalTimeBruteForce += resultBrute.Time;
            totalTimeFF += resultFF.Time;
            totalTimeBF += resultBF.Time;

            int optimalSolution = resultBrute.Result.Count;
            int solutionFF = resultFF.Result.Count;
            int solutionBF = resultBF.Result.Count;

            if (solutionFF == optimalSolution)
                correctSolutionsFF++;
            if (solutionBF == optimalSolution)
                correctSolutionsBF++;

            totalDeviationFF += Math.Abs((double)(solutionFF - optimalSolution) / optimalSolution);
            totalDeviationBF += Math.Abs((double)(solutionBF - optimalSolution) / optimalSolution);
        }

        Console.WriteLine($"\nОбъем данных: {volume}");
        Console.WriteLine($"Среднее время Brute Force: {totalTimeBruteForce / NumberOfTests} мс");
        Console.WriteLine($"Среднее время First Fit: {totalTimeFF / NumberOfTests} мс");
        Console.WriteLine($"Среднее время Best Fit: {totalTimeBF / NumberOfTests} мс");

        Console.WriteLine($"Совпадение FF с оптимальным решением: {correctSolutionsFF * 100 / NumberOfTests}%");
        Console.WriteLine($"Совпадение BF с оптимальным решением: {correctSolutionsBF * 100 / NumberOfTests}%");

        Console.WriteLine($"Среднее отклонение FF от оптимального решения: {(totalDeviationFF / NumberOfTests) * 100:0.00}%");
        Console.WriteLine($"Среднее отклонение BF от оптимального решения: {(totalDeviationBF / NumberOfTests) * 100:0.00}%");
    }

    private (List<List<int>> Result, long Time) MeasureWithResult(Func<List<List<int>>> algorithm)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = algorithm();
        stopwatch.Stop();
        return (result, stopwatch.ElapsedMilliseconds);
    }
}
