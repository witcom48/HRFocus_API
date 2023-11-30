using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpBenefit
    {
        public cls_TREmpBenefit() { }
       
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string AllowanceDeductID { get; set; }
        public double Amount { get; set; }
        public string PayCondition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean PayAtFirstPeriod { get; set; }
        public string PayType { get; set; }
        public string Reason { get; set; }

    }
}
