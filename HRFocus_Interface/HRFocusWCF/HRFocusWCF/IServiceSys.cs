using HRFocusWCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;

using System.Web;

namespace HRFocusWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceSys
    {


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getLog(string com, string fromdate, string todate);
              

        [OperationContract(Name = "doAuthen")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doAuthen(RequestData input);


        [OperationContract(Name = "doManageEmpBank")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpBank(InputEmpBank input);

        [OperationContract(Name = "doManageEmpSalary")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpSalary(InputEmpSalary input);

        [OperationContract(Name = "doManageEmpPart")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpPart(InputEmpPart input);

        [OperationContract(Name = "doManageEmpPosition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpPosition(InputEmpPosition input);

        [OperationContract(Name = "doManageEmpBenefit")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpBenefit(InputEmpBenefit input);

        [OperationContract(Name = "doManageEmp")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmp(InputEmpMain input);

        [OperationContract(Name = "doManageEmpDetail")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageEmpDetail(InputEmpDetail input);

        [OperationContract(Name = "doManagePosition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManagePosition(InputPosition input);

        [OperationContract(Name = "doManagePart")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManagePart(InputPart input);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PositionMaster?CompanyCode={com}&PositionCode={code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ApiResponse<MTPosition> PositionMasterList(string com, string code);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "EmployeeProfile?CompanyCode={com}&EmpType={type}&StartWorkFrom={from}&StartWorkTo={to}&ResignStatus={status}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ApiResponse<Employee> EmployeeProfileList(string com, string type, string from, string to, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "EmployeePosition?CompanyCode={com}&Fromdate={from}&Todate={to}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ApiResponse<EmployeePositionModel> EmployeePositionList(string com, string from, string to);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "IncomeDeduct", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ApiResponse<IncomeDeduct> IncomeDeductCreate(IncomeDeductInput input);
        
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }



}
