using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_MTEmpDetail
    {
        public cls_MTEmpDetail() { }
         
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public DateTime BirthDay { get; set; }
        public string Sex { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string MaritalStatus { get; set; }
        public string MilitaryStatus { get; set; }
        public string LanguageID { get; set; }
        public string BloodID { get; set; }
        public string NationID { get; set; }
        public string OriginID { get; set; }
        public string ReligionID { get; set; }
        public string CardNo { get; set; }
        public DateTime CardNoIssueDate { get; set; }
        public DateTime CardNoExpireDate { get; set; }	
        public string SocialNo { get; set; }
        public DateTime SocialIssueDate { get; set; }
        public DateTime SocialExpireDate { get; set; }
        public string SSOStatusSent { get; set; }	
        public string PassportNo { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public DateTime PassportExpireDate { get; set; }
        public string TaxNo { get; set; }
        public DateTime TaxIssueDate { get; set; }
        public DateTime TaxExpireDate { get; set; }	
        public string PFNo { get; set; }	
        public string PFType { get; set; }
        public DateTime PFEnterDate { get; set; }
        public DateTime PFStartDate { get; set; }	
        public double PFPerComp { get; set; }
        public double PFPerEmp { get; set; }	
        

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

    }
}
