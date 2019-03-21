using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewMain
    {
        Presenter p;
        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);
            DepBox.SelectionChanged += delegate { p.ChangeSelectedDep(); };
            AddEmp.Click += delegate { p.AddEmployee(); };
            AddDep.Click += delegate { p.AddDepartments(); };
            EditEmp.Click += delegate { p.EditEmployee(); };
            EditDep.Click += delegate { p.EditDepartments(); };
            DelDep.Click += delegate { p.deleteDepartments(); };
            DelEmp.Click += delegate { p.deleteEmployee(); };
            Reload.Click += delegate { p.Load(); };
        }

        public Employee curEmp { get => employeeDataGrid.SelectedItem as Employee; set => employeeDataGrid.SelectedItem = value; }

        public Deparments curDep {get=>DepBox.SelectedItem as Deparments; set=>DepBox.SelectedItem=value;}

        ObservableCollection<Deparments> IViewMain.contextDep { set => DepBox.ItemsSource=value; }
        ObservableCollection<Employee> IViewMain.contextEmp { set => employeeDataGrid.DataContext=value; }
    }
}
