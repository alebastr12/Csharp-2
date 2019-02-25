using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workers
{
    abstract class BaseWorker : IComparable
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Fio { get; }
        /// <summary>
        /// Уникальный ID сотрудника
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Статический член класса для расчета уникального ID сотрудника
        /// </summary>
        protected static int LastId = 0;
        
        public BaseWorker(string fio)
        {
            Fio = fio;
            LastId++;
            Id=LastId;
        }
        public abstract double GetSalary();
        /// <summary>
        /// Переопределенный метод для вывода в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Fio,-10} Id:{Id,-3} Зарплата:{GetSalary()}";
        }
        /// <summary>
        /// Реализация интерфейса IComparable
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
            return GetSalary().CompareTo((obj as BaseWorker).GetSalary());
        }
    }
}
