using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace DBEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapterEmp;
        SqlDataAdapter adapterDep;
        DataTable dtEmployee;
        DataTable dtDepartments;
        SqlCommand commandSelect;

        public MainWindow()
        {
            InitializeComponent();

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"C:\Users\Алкесандр\Documents\lesson7_alebastr.mdf",
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                Pooling=true
            };
            connection = new SqlConnection(connectionStringBuilder.ConnectionString);

            InitDBDep();
            InitDBEmp();

            dtEmployee = new DataTable();
            dtDepartments = new DataTable();

            adapterEmp.Fill(dtEmployee);
            adapterDep.Fill(dtDepartments);

            //Коддля начального заполнения таблиц
            //for (int i = 0; i < 100; i++)
            //{
            //    DataRow newRow = dtDepartments.NewRow();
            //    newRow["DepName"] = $"Департамент {i}";
            //    dtDepartments.Rows.Add(newRow);
            //    adapterDep.Update(dtDepartments);
            //    var depID = dtDepartments.Select().Where((e) => e["DepName"].Equals($"Департамент {i}")).Select((e)=>(int)e["Id"]);
            //    for (int j = 0; j < 1000; j++)
            //    {
            //        newRow = dtEmployee.NewRow();
            //        newRow["name"] = $"Имя_{j}";
            //        newRow["surname"] = $"Фамилия_{j}";
            //        newRow["position"] = $"Должность_{j}";
            //        newRow["birthday"] = DateTime.Now;
            //        newRow["depId"] = depID.First();
            //        dtEmployee.Rows.Add(newRow);
                    
            //    }
            //    adapterEmp.Update(dtEmployee);
            //}

            

            employeeDataGrid.DataContext = dtEmployee.DefaultView;
            DepBox.ItemsSource = dtDepartments.DefaultView;

            DepBox.SelectionChanged += DepBox_SelectionChanged;
            DelDep.Click += DelDep_Click;
            DelEmp.Click += DelEmp_Click;
            AddDep.Click += AddDep_Click;
            EditDep.Click += EditDep_Click;
            AddEmp.Click += AddEmp_Click;
            EditEmp.Click += EditEmp_Click;
        }

        private void EditEmp_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedIndex > -1)
            {
                DataRowView curRow = (DataRowView)employeeDataGrid.SelectedItem;
                curRow.BeginEdit();
                EditEmp editWindow = new EditEmp(curRow.Row, dtDepartments, DepBox.SelectedIndex);
                editWindow.ShowDialog();
                if (editWindow.DialogResult.Value)
                {
                    curRow.EndEdit();
                    adapterEmp.Update(dtEmployee);
                    dtEmployee.Clear();
                    adapterEmp.Fill(dtEmployee);
                }
                else
                {
                    curRow.CancelEdit();
                }
            }

        }

        private void AddEmp_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dtEmployee.NewRow();
            EditEmp editWindow = new EditEmp(newRow, dtDepartments, DepBox.SelectedIndex);
            editWindow.ShowDialog();

            if (editWindow.DialogResult.Value)
            {
                dtEmployee.Rows.Add(editWindow.resultRow);
                adapterEmp.Update(dtEmployee);
                dtEmployee.Clear();
                adapterEmp.Fill(dtEmployee);
            }
        }

        private void EditDep_Click(object sender, RoutedEventArgs e)
        {
            if (DepBox.SelectedIndex > -1)
            {
                DataRowView curRow = (DataRowView)DepBox.SelectedItem;
                curRow.BeginEdit();
                EditDep editWindow = new EditDep(curRow.Row);
                editWindow.ShowDialog();
                if (editWindow.DialogResult.Value)
                {
                    curRow.EndEdit();
                    adapterDep.Update(dtDepartments);
                } else
                {
                    curRow.CancelEdit();
                }
            }
        }

        private void AddDep_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dtDepartments.NewRow();
            EditDep editWindow = new EditDep(newRow);
            editWindow.ShowDialog();

            if ( editWindow.DialogResult.Value)
            {
                dtDepartments.Rows.Add(editWindow.resultRow);
                adapterDep.Update(dtDepartments);
            }
        }

        private void DelEmp_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedIndex > -1)
            {
                DataRowView curRow = (DataRowView)employeeDataGrid.SelectedItem;
                curRow.Row.Delete();
                adapterEmp.Update(dtEmployee);
            }
        }

        private void DelDep_Click(object sender, RoutedEventArgs e)
        {
            DataRowView curRow = (DataRowView)DepBox.SelectedItem;

            curRow?.Row?.Delete();
            adapterDep.Update(dtDepartments);
        }

        private void DepBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView curRow = (DataRowView)DepBox.SelectedItem;
            commandSelect.Parameters["@depId"].Value = curRow?.Row["Id"] ?? 0;
            dtEmployee.Clear();
            adapterEmp.Fill(dtEmployee);
        }

        public void InitDBDep()
        {
            adapterDep = new SqlDataAdapter();

            SqlCommand command= new SqlCommand("SELECT Id, DepName FROM Deparments"
                , connection);
            adapterDep.SelectCommand = command;

            command = new SqlCommand(@"INSERT INTO Deparments (DepName) VALUES (@DepName); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@DepName", SqlDbType.NVarChar, 50, "DepName");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterDep.InsertCommand = command;

            command = new SqlCommand(@"UPDATE Deparments SET DepName=@DepName WHERE Id=@Id", connection);
            command.Parameters.Add("@DepName", SqlDbType.NVarChar, 50, "DepName");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.UpdateCommand = command;

            command = new SqlCommand("DELETE FROM Deparments WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.DeleteCommand = command;
        }
        public void InitDBEmp()
        {
            
            adapterEmp = new SqlDataAdapter();

            commandSelect = new SqlCommand("SELECT Id, name, surname, position, birthday, depId FROM Employee WHERE depId=@depId"
                , connection);
            commandSelect.Parameters.AddWithValue("@depId", 0);
            adapterEmp.SelectCommand = commandSelect;
            

            SqlCommand command = new SqlCommand(@"INSERT INTO Employee (name, surname, position, birthday,depId) 
                VALUES (@name, @surname, @position, @birthday, @depId); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
            command.Parameters.Add("@surname", SqlDbType.NVarChar, 50, "surname");
            command.Parameters.Add("@position", SqlDbType.NVarChar, 100, "position");
            command.Parameters.Add("@birthday", SqlDbType.Date, 0, "birthday");
            command.Parameters.Add("@depId", SqlDbType.Int, 0, "depId");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterEmp.InsertCommand = command;

            command = new SqlCommand(@"UPDATE Employee SET name=@name, surname=@surname, position=@position, birthday=@birthday
                ,depId=@depID WHERE Id=@Id", connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
            command.Parameters.Add("@surname", SqlDbType.NVarChar, 50, "surname");
            command.Parameters.Add("@position", SqlDbType.NVarChar, 100, "position");
            command.Parameters.Add("@birthday", SqlDbType.Date, 0, "birthday");
            command.Parameters.Add("@depId", SqlDbType.Int, 0, "depId");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.UpdateCommand = command;

            command = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.DeleteCommand = command;
        }
    }
}
