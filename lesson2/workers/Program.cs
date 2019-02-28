using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workers
{
    class Program
    {
        static void Main(string[] args)
        { 
            //Первое задание, подпункты a,b,c
            //BaseWorker[] Workers= new BaseWorker[20];
            //Random rnd = new Random();
            //for (int i = 0; i < Workers.Length; i=i+2)
            //{
            //    Workers[i] = new TimeWorker($"Имя {i}", rnd.Next(10, 30));
            //    Workers[i + 1] = new FixWorker($"Имя {i+1}", rnd.Next(100, 500));
            //}
            //Array.Sort(Workers);
            //foreach (var item in Workers)
            //{
            //    Console.WriteLine(item);
            //}

            //Подпункт c
            WorkerArray Workers = new WorkerArray(20);
            Random rnd = new Random();
            for (int i = 0; i < Workers.Length; i = i + 2)
            {
                Workers.Add(new TimeWorker($"Имя {i}", rnd.Next(10, 30)));
                Workers.Add(new FixWorker($"Имя {i + 1}", rnd.Next(100, 500)));
            }
            Workers.Sort();
            foreach (var item in Workers)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
