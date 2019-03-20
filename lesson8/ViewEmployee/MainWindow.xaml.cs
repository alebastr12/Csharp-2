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
        }

        public Employee curEmp => employeeDataGrid.SelectedItem as Employee;

        public Deparments curDep => DepBox.SelectedItem as Deparments;

        ObservableCollection<Deparments> IViewMain.contextDep { set => DepBox.ItemsSource=value; }
        ObservableCollection<Employee> IViewMain.contextEmp { set => employeeDataGrid.DataContext=value; }
    }
}
