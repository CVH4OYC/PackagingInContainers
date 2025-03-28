using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практическая_работа__контейнеры_.Models
{
    /// <summary>
    /// Элемент (предмет)
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Вес
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Номер предмета
        /// </summary>
        public int Index { get; set; }

        public Item(int weight, int index)
        {
            Weight = weight;
            Index = index;
        }
    }
}
