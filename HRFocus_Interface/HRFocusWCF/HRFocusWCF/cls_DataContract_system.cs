using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HRFocusWCF
{
    public class cls_DataContract_system
    {
    }

    [DataContract]
    public class Credentials
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
    

    [DataContract]
    public class InputEmpSalary
    {        
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string PeriodStart { get; set; }
        [DataMember]
        public double Salary { get; set; }
        [DataMember]
        public double IncAmount { get; set; }
        [DataMember]
        public double IncPercent { get; set; }
        [DataMember]
        public double PreviousSalary { get; set; }
        [DataMember]
        public string ReasonID { get; set; }
        [DataMember]
        public string ReasonDesc { get; set; }
        [DataMember]
        public string EmpType { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

    }

    [DataContract]
    public class InputEmpBank
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string BankID { get; set; }
        [DataMember]
        public string BankAccNo { get; set; }
        [DataMember]
        public string BankAccName { get; set; }
        [DataMember]
        public double PayBankPercent { get; set; }
        [DataMember]
        public double PayCashPercent { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }
    }

    [DataContract]
    public class InputEmpBenefit
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string AllowanceDeductID { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public string PayCondition { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public Boolean PayAtFirstPeriod { get; set; }
        [DataMember]
        public string PayType { get; set; }
        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

    }

    [DataContract]
    public class InputEmpPart
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string PartEntDate { get; set; }
        [DataMember]
        public string Level01 { get; set; }
        [DataMember]
        public string Level02 { get; set; }
        [DataMember]
        public string Level03 { get; set; }
        [DataMember]
        public string Level04 { get; set; }
        [DataMember]
        public string Level05 { get; set; }
        [DataMember]
        public string Level06 { get; set; }
        [DataMember]
        public string Level07 { get; set; }
        [DataMember]
        public string Level08 { get; set; }
        [DataMember]
        public string Level09 { get; set; }
        [DataMember]
        public string Level10 { get; set; }
        [DataMember]
        public string IsActive { get; set; }
        [DataMember]
        public string CommentP { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

    }

    [DataContract]
    public class InputEmpPosition
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string PositionID { get; set; }
        [DataMember]
        public string PositionDate { get; set; }
        [DataMember]
        public string ReasonID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string JobLevelID { get; set; }
        [DataMember]
        public string JDID { get; set; }
        [DataMember]
        public string UpdateDate { get; set; }
        [DataMember]
        public string PositionName2 { get; set; }
        [DataMember]
        public string JobDescDate { get; set; }
        [DataMember]
        public string PositionID2 { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }
    }

    [DataContract]
    public class InputEmpMain
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string EmpIDRef { get; set; }
        [DataMember]
        public string BranchID { get; set; }
        [DataMember]
        public string InitialID { get; set; }
        [DataMember]
        public string EmpFName { get; set; }
        [DataMember]
        public string EmpLName { get; set; }
        [DataMember]
        public string EmpFNameT { get; set; }
        [DataMember]
        public string EmpLNameT { get; set; }
        [DataMember]
        public string EmpNickname { get; set; }
        [DataMember]
        public string EmpType { get; set; }
        [DataMember]
        public string EmpStatus { get; set; }
        [DataMember]
        public string WorkStartDate { get; set; }
        [DataMember]
        public string ProbationEndDate { get; set; }
        [DataMember]
        public int ProbationLimit { get; set; }
        [DataMember]
        public int PayPerBank { get; set; }
        [DataMember]
        public int PayPerCash { get; set; }
        [DataMember]
        public string BankAccNoComp { get; set; }
        [DataMember]
        public bool ResignStatus { get; set; }
        [DataMember]
        public string ResignDate { get; set; }
        [DataMember]
        public string ResignReasonID { get; set; }
        [DataMember]
        public string ResignDetial { get; set; }
        [DataMember]
        public double HrsPerDay { get; set; }
       
        [DataMember]
        public string ModifiedBy { get; set; }
    }

    [DataContract]
    public class InputEmpDetail
    {

        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string EmpID { get; set; }
        [DataMember]
        public string BirthDay { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string MilitaryStatus { get; set; }
        [DataMember]
        public string LanguageID { get; set; }
        [DataMember]
        public string BloodID { get; set; }
        [DataMember]
        public string NationID { get; set; }
        [DataMember]
        public string OriginID { get; set; }
        [DataMember]
        public string ReligionID { get; set; }
        [DataMember]
        public string CardNo { get; set; }
        [DataMember]
        public string CardNoIssueDate { get; set; }
        [DataMember]
        public string CardNoExpireDate { get; set; }
        [DataMember]
        public string SocialNo { get; set; }
        [DataMember]
        public string SocialIssueDate { get; set; }
        [DataMember]
        public string SocialExpireDate { get; set; }
        [DataMember]
        public string SSOStatusSent { get; set; }
        [DataMember]
        public string PassportNo { get; set; }
        [DataMember]
        public string PassportIssueDate { get; set; }
        [DataMember]
        public string PassportExpireDate { get; set; }
        [DataMember]
        public string TaxNo { get; set; }
        [DataMember]
        public string TaxIssueDate { get; set; }
        [DataMember]
        public string TaxExpireDate { get; set; }
        [DataMember]
        public string PFNo { get; set; }
        [DataMember]
        public string PFType { get; set; }
        [DataMember]
        public string PFEnterDate { get; set; }
        [DataMember]
        public string PFStartDate { get; set; }
        [DataMember]
        public double PFPerComp { get; set; }
        [DataMember]
        public double PFPerEmp { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
        

    }

    [DataContract]
    public class ResponseData
    {
        [DataMember(Order = 0)]
        public string token { get; set; }
        [DataMember(Order = 1)]
        public bool authenticated { get; set; }
        [DataMember(Order = 2)]
        public string employeeId { get; set; }
        [DataMember(Order = 3)]
        public string firstname { get; set; }

        [DataMember(Order = 8)]
        public DateTime timestamp { get; set; }
        [DataMember(Order = 9)]
        public string userName { get; set; }
    }


    [DataContract]
    public class InputPosition
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string PositionID { get; set; }
        [DataMember]
        public string PositionNameT { get; set; }
        [DataMember]
        public string PositionNameE { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
    }

    [DataContract]
    public class InputPart
    {
        [DataMember]
        public string CompID { get; set; }
        [DataMember]
        public string LevelID { get; set; }
        [DataMember]
        public string PartID { get; set; }
        [DataMember]
        public string PartNameT { get; set; }
        [DataMember]
        public string PartNameE { get; set; }
        [DataMember]
        public string PartRef { get; set; }
        [DataMember]
        public string LevelRef { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<T> data { get; set; }
    }

    public class MTPosition
    {
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string PositionID { get; set; }
        public string PositionNameT { get; set; }
        public string PositionNameE { get; set; }
    }
    public class EmployeePositionModel
    {
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string PositionID { get; set; }
        public string PositionDate { get; set; }
        public string PositionNameE { get; set; }
        public string PositionNameT { get; set; }
        public string ReasonID { get; set; }
        public string ReasonNameE { get; set; }
        public string ReasonNameT { get; set; }
    }
    public class Employee
    {
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string BranchID { get; set; }
        public string InitialID { get; set; }
        public string EmpFName { get; set; }
        public string EmpLName { get; set; }
        public string EmpFNameT { get; set; }
        public string EmpLNameT { get; set; }
        public string EmpNickname { get; set; }
        public string EmpType { get; set; }
        public string EmpStatus { get; set; }
        public string WorkStartDate { get; set; }
        public string ProbationEndDate { get; set; }
        public int ProbationLimit { get; set; }
        public double HrsPerDay { get; set; }
        public bool ResignStatus { get; set; }
        public string ResignDate { get; set; }
        public string ResignReasonID { get; set; }
        public string ResignDetial { get; set; }
    }


    public class IncomeDeductInput
    {
        public List<IncomeDeduct> data { get; set; }
    }
    public class IncomeDeduct
    {
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string PaidCode { get; set; }
        public string PaidType { get; set; }
        public string PeriodID { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string YearID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public string PayType { get; set; }
        public string Description { get; set; }

        public bool Insert_Update { get; set; }

    }

    public class tbPOLPeriodic
    {
        public string CompID { get; set; }
        public string PeriodID { get; set; }
        public string PeriodYear { get; set; }
        public string EmpType { get; set; }
        public string PeriodNameT { get; set; }
        public string PeriodNameE { get; set; }
        public string PaymentDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool? ClosePR { get; set; }
        public bool? CloseTA { get; set; }
    }

}