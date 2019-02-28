using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workers
{
    /// <summary>
    /// Класс сотрудника с фиксированным окладом
    /// </summary>
    class FixWorker : BaseWorker
    {
        /// <summary>
        /// Зарплата
        /// </summary>
        public int payment { get; set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="fio">Имя</param>
        /// <param name="payment">Зарплата</param>
        public FixWorker(string fio, int payment):base(fio)
        {
            this.payment = payment;
        }
        /// <summary>
        /// Реализация абстрактоного методадля расчета зарплаты
        /// </summary>
        /// <returns></returns>
        public override double GetSalary()
        {
            return payment;
        }
    }
}
