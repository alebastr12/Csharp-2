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
using System.Windows.Shapes;

namespace ViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для EditEmp.xaml
    /// </summary>
    public partial class EditEmp : Window, IViewEmpEdit
    {
        Deparments curDep;
        public EditEmp()
        {
            InitializeComponent();
            Save.Click += Save_Click;
            Cancel.Click += Cancel_Click;
            DepBox.SelectionChanged += DepBox_SelectionChanged;
        }

        private void DepBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curDep=DepBox.SelectedItem as Deparments;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DepBox.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите департамент сотрудника", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!DateTime.TryParse(TextDate.Text, out _))
            {
                MessageBox.Show("Установите действительнуюдату", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
                DialogResult = true;
        }

        public string NameEmp { get => TextName.Text; set => TextName.Text=value; }
        public string SurameEmp { get => TextSurame.Text; set => TextSurame.Text=value; }
        public string PositionEmp { get => TextPosition.Text; set => TextPosition.Text=value; }
        public DateTime BirthdayEmp { get => Convert.ToDateTime(TextDate.Text); set => TextDate.Text=value.ToShortDateString(); }
        public ObservableCollection<Deparments> depsList { set => DepBox.ItemsSource=value; }
        public Deparments selectDep { get => curDep; set => DepBox.SelectedItem=value; }
    }
}
