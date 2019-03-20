using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace ViewEmployee
{
    class Presenter
    {
        IViewMain IMain;
        IviewDepEdit IDep;
        IViewEmpEdit IEmp;
        ObservableCollection<Deparments> db;
        HttpClient client;
        string apiurl = @"http://localhost:63459/";

        public string ErrorString {get; set;}

        public Presenter(IViewMain view)
        {
            IMain = view;
            db = new ObservableCollection<Deparments>();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Load();
            IMain.contextDep = db;
        }
        public void ChangeSelectedDep()
        {
            IMain.contextEmp = IMain.curDep.Employee;
        }
        protected bool Load()
        {
            HttpResponseMessage r = client.GetAsync(apiurl + "departments").Result;
            if (!r.IsSuccessStatusCode) return false;
            db = JsonConvert.DeserializeObject<ObservableCollection<Deparments>>(r.Content.ReadAsStringAsync().Result);
            if (db.Count == 0) return false;
            foreach (var item in db)
            {
                r = client.GetAsync(apiurl + $"employeesofdep/{item.Id}").Result;
                if (!r.IsSuccessStatusCode) return false;
                item.Employee= JsonConvert.DeserializeObject<ObservableCollection<Employee>>(r.Content.ReadAsStringAsync().Result);
            }
            return true;
        }
    }
}
