using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace listT
{
    
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ListOfInt = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                ListOfInt.Add(rnd.Next(1, 20));
            }
            foreach (var item in FreqMass(ListOfInt))
            {
                Console.WriteLine($"{item.Key} - {item.Value} вхождений");
            }
            Console.WriteLine();
            ///С помощью LINQ
            foreach(var item in ListOfInt.Distinct()) //берем только уникальные элементы
            {
                Console.WriteLine($"{item} - {ListOfInt.Where(a=>a==item).Count()} вхождений");
            }
            ///Задание 3
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };
            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            var d = dict.OrderBy(pair => pair.Value); //Свернуть обращение к OrderBy с использованием лямбда-выражения =>.

            //Развернуть обращение к OrderBy с использованием делегата 
            Func<KeyValuePair<string, int>,int> del = new Func<KeyValuePair<string, int>, int>(getKey);
            var b = dict.OrderBy(del);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }


            Console.ReadKey();
        }
        static int getKey(KeyValuePair<string, int> pair)
        {
            return pair.Value;
        }
        /// <summary>
        /// Метод подсчета частоты вхождения элемента для обобщенной коллекции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">коллекция</param>
        /// <returns>Словарь с частотным массивом</returns>
        static Dictionary<T,int> FreqMass<T>(List<T> list)
        {
            Dictionary<T,int> res = new Dictionary<T,int>();
            foreach (var item in list)
            {
                if (res.ContainsKey(item))
                    res[item]++;
                else res.Add(item, 1);
            }
            return res;
        }
    }
}
