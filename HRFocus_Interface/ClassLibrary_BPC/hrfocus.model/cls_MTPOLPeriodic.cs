using System;

namespace ClassLibrary_BPC.hrfocus.model
{
   public class cls_MTPOLPeriodic
    {
        public cls_MTPOLPeriodic() { }
        public string CompID { get; set; }
        public string PeriodID { get; set; }
        public string PeriodYear { get; set; }
        public string EmpType { get; set; }
        public string PeriodNameT { get; set; }
        public string PeriodNameE { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? ClosePR { get; set; }
        public bool? CloseTA { get; set; }
        public bool? CloseWE { get; set; }
        public bool? CloseTR { get; set; }
        public int? SalaryQtyday { get; set; }
        public int? SalaryResday { get; set; }
        public int? SalaryProday { get; set; }
        public bool? CalculateType { get; set; }
    }
}
