using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEmployee
{
    public class Deparments
    {
        public Deparments()
        {
            this.Employee = new ObservableCollection<Employee>();
        }

        public int Id { get; set; }
        public string DepName { get; set; }

        public ObservableCollection<Employee> Employee { get; set; }
    }
}
