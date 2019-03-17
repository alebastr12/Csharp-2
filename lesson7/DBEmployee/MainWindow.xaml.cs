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
using MySql.Data;
using MySql.Data.MySqlClient;


namespace DBEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection;
        MySqlDataAdapter adapterEmp;
        MySqlDataAdapter adapterDep;
        DataTable dtEmployee;
        DataTable dtDepartments;
        MySqlCommand commandSelect;

        

        public MainWindow()
        {
            InitializeComponent();

            //var connectionStringBuilder = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"(LocalDB)\MSSQLLocalDB",
            //    //InitialCatalog= "lesson7_alebastr",
            //    AttachDBFilename = @"C:\Users\Алкесандр\Documents\lesson7_alebastr.mdf",
            //    IntegratedSecurity = true,
            //    ConnectTimeout = 30,
            //    Pooling=true
            //};
            //server=mydatabase.czuq04mwza1u.eu-north-1.rds.amazonaws.com;user id=root;database=mydatabase;port=3306;persistsecurityinfo=True
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = @"mydatabase.czuq04mwza1u.eu-north-1.rds.amazonaws.com",
                //InitialCatalog= "lesson7_alebastr",
                UserID="root",
                Password="Y240690a",
                Database="mydatabase",
                //AttachDBFilename = @"C:\Users\Алкесандр\Documents\lesson7_alebastr.mdf",
                //IntegratedSecurity = true,
                PersistSecurityInfo=true,
                ConnectionTimeout = 30,
                CharacterSet = "UTF8",
                Pooling = true
            };
            connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            
            InitDBDep();
            InitDBEmp();

            dtEmployee = new DataTable();
            dtDepartments = new DataTable();

            adapterEmp.Fill(dtEmployee);
            adapterDep.Fill(dtDepartments);

            //Код для начального заполнения таблиц

            //for (int i = 0; i < 10; i++)
            //{
            //    DataRow newRow = dtDepartments.NewRow();
            //    newRow["DepName"] = $"Department_{i}";
            //    dtDepartments.Rows.Add(newRow);
            //    adapterDep.Update(dtDepartments);
            //    connection.Open();
            //    MySqlCommand selCom = new MySqlCommand($@"SELECT Id FROM Deparments WHERE DepName='Department_{i}'",connection);
            //    MySqlDataReader reader = selCom.ExecuteReader();
            //    reader.Read();
            //    int curDepId = reader.GetInt32("Id");
            //    connection.Close();
            //    //var depID = dtDepartments.Select().Where((e) => e["DepName"].Equals($"Department_{i}")).Select((e) => e["Id"]);
            //    Console.WriteLine($"ID добавленного департамента - {curDepId}");
            //    for (int j = 0; j < 100; j++)
            //    {
            //        newRow = dtEmployee.NewRow();
            //        newRow["name"] = $"Name_{j}";
            //        newRow["surname"] = $"Surname_{j}";
            //        newRow["position"] = $"Position_{j} Dep {i}";
            //        newRow["birthday"] = DateTime.Now;
            //        newRow["depId"] = curDepId;
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
            adapterDep = new MySqlDataAdapter();

            MySqlCommand command= new MySqlCommand("SELECT Id, DepName FROM Deparments"
                , connection);
            adapterDep.SelectCommand = command;

            command = new MySqlCommand(@"INSERT INTO Deparments (DepName) VALUES (@DepName);", connection); // SET @ID = @@IDENTITY;
            command.Parameters.Add("@DepName", MySqlDbType.VarChar, 50, "DepName");
            MySqlParameter param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterDep.InsertCommand = command;

            command = new MySqlCommand(@"UPDATE Deparments SET DepName=@DepName WHERE Id=@Id", connection);
            command.Parameters.Add("@DepName", MySqlDbType.VarChar, 50, "DepName");
            param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.UpdateCommand = command;

            command = new MySqlCommand("DELETE FROM Deparments WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.DeleteCommand = command;
        }
        public void InitDBEmp()
        {
            
            adapterEmp = new MySqlDataAdapter();

            commandSelect = new MySqlCommand("SELECT Id, name, surname, position, birthday, depId FROM Employee WHERE depId=@depId"
                , connection);
            commandSelect.Parameters.AddWithValue("@depId", 0);
            adapterEmp.SelectCommand = commandSelect;
            

            MySqlCommand command = new MySqlCommand(@"INSERT INTO Employee (name, surname, position, birthday,depId) 
                VALUES (@name, @surname, @position, @birthday, @depId); ", connection); //SET @ID = @@IDENTITY;
            command.Parameters.Add("@name", MySqlDbType.VarChar, 50, "name");
            command.Parameters.Add("@surname", MySqlDbType.VarChar, 50, "surname");
            command.Parameters.Add("@position", MySqlDbType.VarChar, 100, "position");
            command.Parameters.Add("@birthday", MySqlDbType.Date, 0, "birthday");
            command.Parameters.Add("@depId", MySqlDbType.Int64, 0, "depId");
            MySqlParameter param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterEmp.InsertCommand = command;

            command = new MySqlCommand(@"UPDATE Employee SET name=@name, surname=@surname, position=@position, birthday=@birthday
                ,depId=@depID WHERE Id=@Id", connection);
            command.Parameters.Add("@name", MySqlDbType.VarChar, 50, "name");
            command.Parameters.Add("@surname", MySqlDbType.VarChar, 50, "surname");
            command.Parameters.Add("@position", MySqlDbType.VarChar, 100, "position");
            command.Parameters.Add("@birthday", MySqlDbType.Date, 0, "birthday");
            command.Parameters.Add("@depId", MySqlDbType.Int64, 0, "depId");
            param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.UpdateCommand = command;

            command = new MySqlCommand("DELETE FROM Employee WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", MySqlDbType.Int64, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.DeleteCommand = command;
        }
    }
}
