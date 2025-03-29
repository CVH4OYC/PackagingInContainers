using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Практическая_работа__контейнеры_.Algorithms;
using Практическая_работа__контейнеры_.Models;

namespace Практическая_работа__контейнеры_
{
    class ExperimentForGraphics
    {
        private const int CONTAINER_CAPACITY = 20;
        private const string CSV_FILE_PATH = "experiment_results.csv";  

        public ExperimentForGraphics()
        {
            if (!File.Exists(CSV_FILE_PATH))
            {
                File.WriteAllText(CSV_FILE_PATH, "Volume;FirstFitTime;BestFitTime\n");
            }
        }

        public void RunExperiment()
        {

            int[] testVolumes = Enumerable.Range(1, 1000).ToArray();

            Console.WriteLine("\nЗапуск серии тестов на различных объемах данных...");

            foreach (var volume in testVolumes)
            {
                RunTestsForVolume(volume);
            }
        }

        private void RunTestsForVolume(int volume)
        {
            const int NumberOfTests = 100;

            double totalTimeBF = 0;
            double totalTimeFF = 0;
            double totalTimeBruteForce = 0;

            for (int i = 0; i < NumberOfTests; i++)
            {
                Console.WriteLine($"Тестовый запуск {i + 1} на V = {volume}");
               var items = GenerateItemsWithFixedWeight(volume, 11);

                //var resultBrute = MeasureWithResult(() => BruteForceFF.Run(items, CONTAINER_CAPACITY));
                var resultFF = MeasureWithResult(() => FirstFit.Run(items, CONTAINER_CAPACITY));
                var resultBF = MeasureWithResult(() => BestFit.Run(items, CONTAINER_CAPACITY));

               // totalTimeBruteForce += resultBrute.Time;
                totalTimeFF += resultFF.Time;
                totalTimeBF += resultBF.Time;
            }

            double avgTimeBruteForce = totalTimeBruteForce / NumberOfTests;
            double avgTimeFF = totalTimeFF / NumberOfTests;
            double avgTimeBF = totalTimeBF / NumberOfTests;

            string csvLine = $"{volume};{avgTimeFF};{avgTimeBF}\n";
            File.AppendAllText(CSV_FILE_PATH, csvLine);

            Console.WriteLine($"\nДанные записаны в файл: {CSV_FILE_PATH}");
        }

        private (List<List<int>> Result, long Time) MeasureWithResult(Func<List<List<int>>> algorithm)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = algorithm();
            stopwatch.Stop();
            return (result, stopwatch.ElapsedMilliseconds);
        }
        private List<Item> GenerateItemsWithFixedWeight(int volume, int weight)
        {
            var items = new List<Item>();

            for (int i = 0; i < volume; i++)
            {
                items.Add(new Item(weight, i + 1));  
            }

            return items;
        }
    }
}
