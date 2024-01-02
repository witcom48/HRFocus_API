using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_MTEmpMain
    {
        public cls_MTEmpMain() { }
         
        public string CompID { get; set; }	
        public string EmpID	 { get; set; }
        public string EmpIDRef { get; set; }	
        public string BranchID { get; set; }	
        public string InitialID { get; set; }	
        public string EmpFName { get; set; }	
        public string EmpLName { get; set; }	
        public string EmpFNameT { get; set; }	
        public string EmpLNameT { get; set; }	
        public string EmpNickname { get; set; }	
        public string EmpType { get; set; }
        public string EmpStatus	 { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime ProbationEndDate { get; set; }	
        public int ProbationLimit { get; set; }	
        public int PayPerBank { get; set; }
        public int PayPerCash { get; set; }	
        public string BankAccNoComp { get; set; }	
        public bool ResignStatus { get; set; }
        public DateTime ResignDate { get; set; }	
        public string ResignReasonID { get; set; }	
        public string ResignDetial { get; set; }
        public double HrsPerDay { get; set; }
        public string CardNo { get; set; }
        public DateTime BirthDay { get; set; }
        public string PreTel { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

    }
}
