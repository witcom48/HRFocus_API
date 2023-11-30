using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpSalary
    {
        public cls_TREmpSalary() { }
       
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public DateTime PeriodStart { get; set; }
        public double Salary { get; set; }
        public double IncAmount { get; set; }
        public double IncPercent { get; set; }
        public double PreviousSalary { get; set; }
        public string ReasonID { get; set; }
        public string ReasonDesc { get; set; }
        public string EmpType { get; set; }

    }
}
