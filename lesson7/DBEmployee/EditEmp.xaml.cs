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
    /// Логика взаимодействия для EditEmp.xaml
    /// </summary>
    public partial class EditEmp : Window
    {
        public DataRow resultRow { get; set; }
        //DataTable tableDeps;
        int depIndex;

        public EditEmp(DataRow row, DataTable deps, int depsIndex)
        {
            InitializeComponent();
            resultRow = row;
            this.depIndex = depsIndex;
            //tableDeps = deps;
            DepBox.ItemsSource = deps.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextName.Text = resultRow["name"].ToString();
            TextSurame.Text = resultRow["surname"].ToString();
            TextPosition.Text = resultRow["position"].ToString();
            TextDate.Text = resultRow["birthday"].ToString();
            DepBox.SelectedIndex = depIndex;
            //DepBox.SelectedItem = tableDeps.Rows.Find(resultRow["depId"]);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DepBox.SelectedIndex > -1)
            {
                resultRow["name"] = TextName.Text;
                resultRow["surname"] = TextSurame.Text;
                resultRow["position"] = TextPosition.Text;
                if (!DateTime.TryParse(TextDate.Text, out _))
                {
                    MessageBox.Show("Укажите действительную дату.");
                    return;
                }
                resultRow["birthday"] = TextDate.Text;
                DataRowView dep = (DataRowView)DepBox.SelectedItem;
                resultRow["depId"] = dep.Row["Id"];
                DialogResult = true;
            } else
            {
                MessageBox.Show("Обязательно нужно выбрать департамент.");
                return;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
