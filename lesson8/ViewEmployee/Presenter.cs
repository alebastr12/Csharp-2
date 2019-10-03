using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace ViewEmployee
{
    class Presenter
    {
        //EditDep EditDepWindow;
        //EditEmp EditEmpWindow;
        IViewMain IMain;
        IViewDepEdit IDep;
        IViewEmpEdit IEmp;
        ObservableCollection<Deparments> db;
        HttpClient client;
        string apiurl = @"https://webapiemployee20190321121459.azurewebsites.net/";
        //string apiurl = @"http://localhost:63459/";

        public string ErrorString { get; set; }

        public Presenter(IViewMain view)
        {
            //EditDepWindow = new EditDep();
            //EditEmpWindow = new EditEmp();
            
            
            IMain = view;
            //db = new ObservableCollection<Deparments>();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (!Load())
            {
                MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// Обработчик выбора департамента
        /// </summary>
        public void ChangeSelectedDep()
        {
            IMain.contextEmp = IMain.curDep?.Employee;
        }
        /// <summary>
        /// Загрузка данных с веб сервиса
        /// </summary>
        /// <returns>Истина если успешно</returns>
        public bool Load()
        {
            HttpResponseMessage r = client.GetAsync(apiurl + "departments").Result;
            if (!r.IsSuccessStatusCode) return false;
            db = new ObservableCollection<Deparments>();
            db = JsonConvert.DeserializeObject<ObservableCollection<Deparments>>(r.Content.ReadAsStringAsync().Result);
            if (db.Count == 0) return false;
            foreach (var item in db)
            {
                r = client.GetAsync(apiurl + $"employeesofdep/{item.Id}").Result;
                if (!r.IsSuccessStatusCode) return false;
                item.Employee = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(r.Content.ReadAsStringAsync().Result);
            }
            IMain.contextDep = db;
            return true;
        }
        /// <summary>
        /// Отправляет запрос POST
        /// </summary>
        /// <typeparam name="T">тип</typeparam>
        /// <param name="Obj">ссылка на объект для отправки (результирующий объект занесется ту да же в случае успеха)</param>
        /// <param name="req">строка запроса к контроллеру</param>
        /// <returns>Истина, если ошибок нет, иначе ложь</returns>
        protected bool post<T>(ref T Obj, string req)
        {
            string jsonObj = JsonConvert.SerializeObject(Obj);
            StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            HttpResponseMessage r = client.PostAsync(apiurl + req, content).Result;
            if (!r.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось добавить объект.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Obj = JsonConvert.DeserializeObject<T>(r.Content.ReadAsStringAsync().Result);
            return true;
        }
        /// <summary>
        /// Отправляет запрос DELETE
        /// </summary>
        /// <param name="req">Строка для запроса к контролллеру</param>
        /// <returns>Истина, если ошибок нет, иначе ложь</returns>
        protected bool delete(string req, int id)
        {
            HttpResponseMessage r = client.DeleteAsync(apiurl+req+$"/{id}").Result;
            if (!r.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось удалить объект.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Обработчик добавления сотрудника
        /// </summary>
        public void AddEmployee()
        {
            EditEmp EditEmpWindow = new EditEmp();
            IEmp = EditEmpWindow;
            IEmp.depsList = db;
            IEmp.selectDep = IMain.curDep;
            EditEmpWindow.ShowDialog();
            if (EditEmpWindow.DialogResult.Value)
            {
                Employee newEmp = new Employee
                {
                    name = IEmp.NameEmp,
                    surname = IEmp.SurameEmp,
                    position = IEmp.PositionEmp,
                    birthday = IEmp.BirthdayEmp,
                    //Deparments=IEmp.selectDep,
                    depId = IEmp.selectDep.Id
                };
                if (post<Employee>(ref newEmp, $"addemployee"))
                {
                    IEmp.selectDep.Employee.Add(newEmp);
                }
            }
        }
        /// <summary>
        /// Обработчик удаления сотрудника
        /// </summary>
        public void deleteEmployee()
        {
            if (IMain.curEmp == null)
                return;
            if (delete("deleteemployee", IMain.curEmp.Id))
            {
                IMain.curDep.Employee.Remove(IMain.curEmp);
            }
        }
        /// <summary>
        /// Обработчик редактирования сотрудника
        /// </summary>
        public void EditEmployee()
        {
            if (IMain.curEmp == null)
                return;
            EditEmp EditEmpWindow = new EditEmp();
            IEmp = EditEmpWindow;
            IEmp.depsList = db;
            IEmp.selectDep = IMain.curDep;
            IEmp.NameEmp = IMain.curEmp.name;
            IEmp.SurameEmp = IMain.curEmp.surname;
            IEmp.PositionEmp = IMain.curEmp.position;
            IEmp.BirthdayEmp = IMain.curEmp.birthday;
            EditEmpWindow.ShowDialog();
            if (EditEmpWindow.DialogResult.Value)
            {
                Employee newEmp = new Employee
                {
                    name = IEmp.NameEmp,
                    surname = IEmp.SurameEmp,
                    position = IEmp.PositionEmp,
                    birthday = IEmp.BirthdayEmp,
                    //Deparments=IEmp.selectDep,
                    depId = IEmp.selectDep.Id
                };
                if (post<Employee>(ref newEmp, $"editemployee/{IMain.curEmp.Id}"))
                {
                    IMain.curDep.Employee.Remove(IMain.curEmp);
                    IEmp.selectDep.Employee.Add(newEmp);
                }
            }
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void deleteDepartments()
        {
            if (IMain.curDep == null)
                return;
            if (delete("deletedepartments", IMain.curDep.Id))
            {
                db.Remove(IMain.curDep);
            }
        }
        /// <summary>
        /// Редактирование департамента
        /// </summary>
        public void EditDepartments()
        {
            if (IMain.curDep == null)
                return;
            EditDep EditDepWindow = new EditDep();
            IDep = EditDepWindow;
            IDep.DepName = IMain.curDep.DepName;
            EditDepWindow.ShowDialog();
            if (EditDepWindow.DialogResult.Value)
            {
                Deparments newDep = new Deparments
                {
                    DepName = IDep.DepName
                };
                if (post<Deparments>(ref newDep, $"editdepartment/{IMain.curDep.Id}"))
                {
                    IMain.curDep.DepName = newDep.DepName;
                }
            }
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        public void AddDepartments()
        {
            EditDep EditDepWindow = new EditDep();
            IDep = EditDepWindow;
            EditDepWindow.ShowDialog();
            if (EditDepWindow.DialogResult.Value)
            {
                Deparments newDep = new Deparments
                {
                    DepName = IDep.DepName
                };
                if (post<Deparments>(ref newDep, $"adddepartments"))
                {
                    db.Add(newDep);
                }
            }
        }
        
    }
}

