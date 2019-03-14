using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace DBEmployee
{
    /// <summary>
    /// Логика взаимодействия для EditDep.xaml
    /// </summary>
    public partial class EditDep : Window
    {
        public DataRow resultRow { get; set; }
        public EditDep(DataRow row)
        {
            InitializeComponent();
            resultRow = row;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepEdit.Text = resultRow["DepName"].ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            resultRow["DepName"] = DepEdit.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
