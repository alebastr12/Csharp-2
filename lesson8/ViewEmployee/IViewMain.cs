using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEmployee
{
    interface IViewMain
    {
        Employee curEmp { get; }
        Deparments curDep { get; }
        ObservableCollection<Deparments> contextDep { set; }
        ObservableCollection<Employee> contextEmp { set; }
    }
}
