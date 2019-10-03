using System;
using System.Collections.Generic;
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

namespace ViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для EditDep.xaml
    /// </summary>
    public partial class EditDep : Window,IViewDepEdit
    {
        public EditDep()
        {
            InitializeComponent();
            Save.Click += Save_Click;
            Cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DepEdit.Text == string.Empty)
            {
                MessageBox.Show("Имя департамента не может быть пустым", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                DialogResult = true;
            }
        }

        public string DepName { get => DepEdit.Text; set => DepEdit.Text=value; }
    }
}
