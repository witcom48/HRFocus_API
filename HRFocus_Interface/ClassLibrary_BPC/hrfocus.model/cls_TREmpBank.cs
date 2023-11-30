using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpBank
    {
        public cls_TREmpBank() { }
       
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string BankID { get; set; }
        public string BankAccNo { get; set; }
        public string BankAccName { get; set; }
        public double PayBankPercent { get; set; }
        public double PayCashPercent { get; set; }     

    }
}
