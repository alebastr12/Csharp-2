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
        public EditEmp()
        {
            InitializeComponent();
        }

        public string NameEmp { get => TextName.Text; set => TextName.Text=value; }
        public string SurameEmp { get => TextSurame.Text; set => TextSurame.Text=value; }
        public string PositionEmp { get => TextPosition.Text; set => TextPosition.Text=value; }
        public string BirthdayEmp { get => TextDate.Text; set => TextDate.Text=value; }
        public ObservableCollection<Deparments> depsList { set => DepBox.ItemsSource=value; }
        public Deparments selectDep { get => DepBox.SelectedItem as Deparments; set => DepBox.SelectedItem=value; }
    }
}
