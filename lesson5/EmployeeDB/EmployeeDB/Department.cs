using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDB
{
    /// <summary>
    /// Класс департамента. Включает коллекцию сотрудников и наследуется от двух интерфейсов 
    /// ICollection<Employee>, IList<Employee> для работы с классом как с коллекцией
    /// </summary>
    class Department : ICollection<Employee>, IList<Employee>
    {
        private List<Employee> EmpList;
        protected string _DepName;
        public string DepName {
            get => _DepName;
            private set
            {
                if (value != string.Empty)
                {
                    _DepName = value;
                }
                else
                {
                    throw new ArgumentException("Имя департамента не может быть пустым.");
                }
            }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название департамента</param>
        public Department(string name)
        {
            DepName = name;
            EmpList = new List<Employee>();
        }
        public int Count => EmpList.Count();

        public bool IsReadOnly => false;

        public Employee this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(Employee item)
        {
            EmpList.Add(item);
        }

        public void Clear()
        {
            EmpList.Clear();
        }

        public bool Contains(Employee item)
        {
            return EmpList.Contains(item);
        }

        public void CopyTo(Employee[] array, int arrayIndex)
        {
            EmpList.CopyTo(array,arrayIndex);
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return EmpList.GetEnumerator();
        }

        public bool Remove(Employee item)
        {
            return EmpList.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return EmpList.GetEnumerator();
        }

        public int IndexOf(Employee item)
        {
            return EmpList.IndexOf(item);
        }

        public void Insert(int index, Employee item)
        {
            EmpList.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            EmpList.RemoveAt(index);
        }
        public override string ToString()
        {
            return _DepName;
        }
    }
}
