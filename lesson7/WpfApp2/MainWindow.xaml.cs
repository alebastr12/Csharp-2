using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        mydatabaseEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new mydatabaseEntities();
            
            var res = db.Employee.Where((e) => e.Deparments.DepName.Equals("Department_1"));
            //dg.DataContext = res.ToList();
            db.Employee.Load();
            db.Deparments.Load();
            cb.ItemsSource = db.Deparments.Local;
            dg.DataContext = db.Employee.ToList();

            cb.SelectionChanged += Cb_SelectionChanged;
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg.DataContext = db.Employee.Where(delegate (Employee emp) { return emp.Deparments.Id == ((cb.SelectedItem as Deparments)?.Id ?? 0); }).ToList();
        }
    }
}
