using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workers
{
    /// <summary>
    /// Класс работника с повременной оплатой
    /// </summary>
    class TimeWorker : BaseWorker
    {
        /// <summary>
        /// Ставка повременного сотрудника
        /// </summary>
        public int rank { get; set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="fio">Имя</param>
        /// <param name="rank">Ставка</param>
        public TimeWorker(string fio, int rank):base(fio)
        {
            this.rank = rank;
        }
        /// <summary>
        /// Реаизация абстрактного класса для подсчета зарплаты
        /// </summary>
        /// <returns>Зарплата</returns>
        public override double GetSalary()
        {
            return 20.8 * 8 * rank;
        }

    }
}
