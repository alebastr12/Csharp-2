using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workers
{
    /// <summary>
    /// Класс на основе массива BAseWorker
    /// </summary>
    class WorkerArray: IEnumerable
    {
        protected BaseWorker[] workers;
        /// <summary>
        /// Текущий индекс элемента массива
        /// </summary>
        protected int _index = 0;
        /// <summary>
        /// Свойство индекса для чтения
        /// </summary>
        public int Index
        {
            get => _index;
        } 
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="n">Размер создаваемого массива</param>
        public WorkerArray(int n)
        {
            if (n > 0) workers = new BaseWorker[n];
            else throw new ArgumentOutOfRangeException("Значение должно быть больше 0!");
        }
        /// <summary>
        /// Свойство для чтения размера массива
        /// </summary>
        public int Length
        {
            get => workers.Length;
        }
        /// <summary>
        /// Метод сортировки по зарплате
        /// </summary>
        public void Sort()
        {
            Array.Sort(workers);
        }
        /// <summary>
        /// Метод добавления элемента массива
        /// </summary>
        /// <param name="worker"></param>
        public void Add(BaseWorker worker)
        {
            if (Index <= workers.Length)
            {
                workers[_index] = worker;
                _index++;
            }
            else
            {
                throw new IndexOutOfRangeException($"Невозможно добавить элемент, массив полон.");
            }
        }
        /// <summary>
        /// Реализация интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < workers.Length; i++)
                yield return workers[i];

        }
    }
}
