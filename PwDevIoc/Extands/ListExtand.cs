using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public static class ListExtand
    {
        public static void AddWithout<T>(this List<T> list, T t)
        {
            if (!list.Contains(t))
            {
                list.Add(t);
            }
        }
    }
}
