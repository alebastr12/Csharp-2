using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDB
{
    /// <summary>
    /// Класс работника
    /// </summary>
    [Serializable]
    class Employee
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string surname { get; set; }
        /// <summary>
        /// Уникальный номер сотрудника
        /// </summary>
        public int ID { get; private set; }
        protected static int LastID = 0;
        /// <summary>
        /// Должность
        /// </summary>
        protected string _position;
        public string position {
            set {
                if (value == string.Empty) {
                    throw new ArgumentNullException("Должность не может быть пустой.");
                }
                _position = value;
            }
            get => _position;
        }
        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime birthday { get; private set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="pos">Должность</param>
        /// <param name="birthday">День родения</param>
        public Employee(string name, string surname, string pos, DateTime birthday)
        {
            if (name == string.Empty || name == null) throw new ArgumentNullException("Имя не может быть пустым.");
            if (surname == string.Empty || surname == null) throw new ArgumentNullException("Фамилия не может быть пустой.");
            this.name = name;
            this.surname = surname;
            this.position = pos;
            this.birthday = birthday;
            LastID++;
            ID = LastID;
        }
        /// <summary>
        /// Переопределенный метод преобразования в строковое представение
        /// </summary>
        /// <returns>Строковое представление объекта</returns>
        public override string ToString()
        {
            return $"{name} {surname} {position} Таб. № {ID} Дата рожд. {birthday.ToShortDateString()}";
        }
        /// <summary>
        /// Переопределенный виртуальный метод проверки эквиваленстности с объектом
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Истина, если эквивалентны, иначе ложь</returns>
        public override bool Equals(object obj)
        {
            if (obj!=null && obj is Employee)
            {
                Employee t = obj as Employee;
                return t.name.Equals(name) && t.surname.Equals(surname) && t.position.Equals(position) && t.birthday.Equals(birthday); //без ID
            }
            else
                return false;
        }

    }
}
