using System;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRPRDeductFixVar
    {
        public cls_TRPRDeductFixVar() { }
        public string CompID { get; set; }
        public string EmpID { get; set; }
        public string DeductID { get; set; }
        public string PeriodID { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string YearID { get; set; }
        public DateTime? Paydate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? QuantityAD { get; set; }
        public string PayType { get; set; }
        public string Note { get; set; }
    }
}
