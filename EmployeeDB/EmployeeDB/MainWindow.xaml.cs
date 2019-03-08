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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Department> ListOfDep;
        int editIndex=-1;
        int editIndexDep = -1;
        //Binding bind;

        public MainWindow()
        {
            InitializeComponent();
            ListOfDep = new List<Department>();
            ListOfDep.Add(new Department("Производственное управление"));
            ListOfDep.Add(new Department("ОКБ"));
            ListOfDep[0].Add(new Employee("Александр", "Бастраков", "Инженер", new DateTime(1988, 03, 09)));
            ListOfDep[1].Add(new Employee("Александр", "Иванов", "Инженер", new DateTime(1988, 03, 13)));

            DepCombo.ItemsSource = ListOfDep;
            DepComboEdit.ItemsSource = ListOfDep;
            ButtonSave.IsEnabled = false;
            ButtonCancel.IsEnabled = false;
            ButtonDepAdd.IsEnabled = false;
            //bind = new Binding();
            //bind.Source = ListOfDep[0];
            //ListEmployee.SetBinding(ListView.ItemsSourceProperty, bind);
            //ListEmployee.ItemsSource = ListOfDep[0];
        }

        private void DepCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepCombo.SelectedIndex>-1)
                ListEmployee.ItemsSource = ListOfDep[DepCombo.SelectedIndex];
        }

        private void ListEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListEmployee.SelectedIndex > -1)
            {
                NameText.Text = (ListEmployee.SelectedItem as Employee).name;
                SurnameText.Text = (ListEmployee.SelectedItem as Employee).surname;
                PosText.Text = (ListEmployee.SelectedItem as Employee).position;
                BirthdayText.Text = (ListEmployee.SelectedItem as Employee).birthday.ToShortDateString();
                DepComboEdit.SelectedIndex = DepCombo.SelectedIndex;
                ButtonSave.IsEnabled = true;
                ButtonCancel.IsEnabled = true;
                ButtonAdd.IsEnabled = false;
                editIndex = ListEmployee.SelectedIndex;
                editIndexDep = DepCombo.SelectedIndex;
            }
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DepComboEdit.SelectedIndex > -1)
            {
                if (AddEmployee())
                    ClearText();
            }
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DepCombo.SelectedIndex>-1 && ListEmployee.SelectedIndex > -1)
            {
                ListOfDep[DepCombo.SelectedIndex].Remove(ListEmployee.SelectedItem as Employee);
                ListEmployee.ItemsSource = null;
                ListEmployee.ItemsSource = ListOfDep[DepCombo.SelectedIndex];
                
            }
        }
        /// <summary>
        /// Добавить департамент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int index = DepCombo.SelectedIndex;
            ListOfDep.Add(new Department(DepText.Text));
            DepText.Clear();
            DepCombo.ItemsSource = null;
            DepComboEdit.ItemsSource = null;
            DepCombo.ItemsSource = ListOfDep;
            DepComboEdit.ItemsSource = ListOfDep;
            DepCombo.SelectedIndex = index;
        }
        /// <summary>
        /// Очистка полей ввода
        /// </summary>
        private void ClearText()
        {
            NameText.Clear();
            SurnameText.Clear();
            PosText.Clear();
            BirthdayText.Clear();
            DepComboEdit.SelectedIndex = -1;
        }
        /// <summary>
        /// Сохранение отредактированного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DepComboEdit.SelectedIndex > -1)
            {
                ListOfDep[editIndexDep].RemoveAt(editIndex);
                if (AddEmployee())
                    EndEdit();
            }

        }
        /// <summary>
        /// Метод добавляет сотрудника в список и обновляет источник данных для представления
        /// </summary>
        private bool AddEmployee()
        {
            bool AddSuc=true;
            try
            {
                ListOfDep[DepComboEdit.SelectedIndex].Add(new Employee(NameText.Text, SurnameText.Text, PosText.Text, Convert.ToDateTime(BirthdayText.Text)));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                AddSuc = false;
            }
            if (AddSuc)
            {
                if (DepComboEdit.SelectedIndex == DepCombo.SelectedIndex)
                {
                    ListEmployee.ItemsSource = null;
                    ListEmployee.ItemsSource = ListOfDep[DepComboEdit.SelectedIndex];
                }
                else DepCombo.SelectedIndex = DepComboEdit.SelectedIndex;
            }
            return AddSuc;
        }
        /// <summary>
        /// Отмена редактирования сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            EndEdit();
        }
        /// <summary>
        /// Завершение редактирования сотрудника
        /// </summary>
        private void EndEdit()
        {
            ButtonSave.IsEnabled = false;
            ButtonCancel.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            editIndex = -1;
            editIndexDep = -1;
            ClearText();
        }

        private void DepText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DepText.Text.Count() > 0)
            {
                ButtonDepAdd.IsEnabled = true;
            }
            else
            {
                ButtonDepAdd.IsEnabled = false;
            }
        }
    }
}
