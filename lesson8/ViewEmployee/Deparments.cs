using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEmployee
{
    public class Deparments: INotifyPropertyChanged
    {
        public Deparments()
        {
            this.Employee = new ObservableCollection<Employee>();
        }
        protected string _DepName;
        public int Id { get; set; }
        public string DepName { get=>_DepName;
            set {
                _DepName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DepName"));
            }
        }

        public ObservableCollection<Employee> Employee { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
