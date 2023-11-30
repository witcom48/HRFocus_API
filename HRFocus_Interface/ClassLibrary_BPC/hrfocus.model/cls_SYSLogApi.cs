using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSLogApi
    {
        public cls_SYSLogApi() { }
       
        public string CompID { get; set; }
        public string APIID { get; set; }
        public string APIType { get; set; }
        public string APIStatus { get; set; }
        public string APIDetail { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }       

    }
}
