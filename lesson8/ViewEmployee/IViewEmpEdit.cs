using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ViewEmployee
{
    interface IViewEmpEdit
    {
        string NameEmp { get; set; }
        string SurameEmp { get; set; }
        string PositionEmp { get; set; }
        DateTime BirthdayEmp { get; set; }
        ObservableCollection<Deparments> depsList { set; }
        Deparments selectDep { get; set; }

    }
}
