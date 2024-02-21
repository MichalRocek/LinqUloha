using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMetody
{
    public static class PrintExtension
    {
        public static void Print<T>(this T data)
        {
            if (data is null) Console.WriteLine("null");
            else if (data is IEnumerable && data is not string) foreach (var element in (data as IEnumerable)!) element.Print();
            else Console.WriteLine(data);
        }
        public static void Print<T>(this T data, string before)
        {
            Console.WriteLine(before);
            data.Print();
        }
        public static void Print<T>(this T data, string before, string after)
        {
            Console.WriteLine(before);
            data.Print();
            Console.WriteLine(after);
        }
    }
}
