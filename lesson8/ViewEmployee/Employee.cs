using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEmployee
{
    public class Employee
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string position { get; set; }
        public System.DateTime birthday { get; set; }
        public int depId { get; set; }

        public virtual Deparments Deparments { get; set; }
    }
}
