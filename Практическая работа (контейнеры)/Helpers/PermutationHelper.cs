namespace Практическая_работа__контейнеры_.Helpers;

class PermutationHelper
{
    public static List<List<T>> GetPermutations<T>(List<T> item, List<List<T>> list = null, List<T> current = null)
    {
        if (list == null)
            list = new List<List<T>>();
        if (current == null)
            current = new List<T>();
        if (item.Count == 0)
        {
            list.Add(current);
            return list;
        }
        for (int i = 0; i < item.Count; i++)
        {
            List<T> lst = new List<T>(item);
            lst.RemoveAt(i);
            var newlst = new List<T>(current);
            newlst.Add(item[i]);
            GetPermutations(lst, list, newlst);
        }
        return list;
    }
}
