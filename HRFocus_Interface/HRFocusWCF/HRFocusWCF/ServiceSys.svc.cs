using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ClassLibrary_BPC.hrfocus.controller;
using ClassLibrary_BPC.hrfocus.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Drawing;
using AntsCode.Util;
using System.Web.Security;
using HRFocusWCF;
using System.Security.Permissions;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;

namespace HRFocusWCF
{

    
    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]

    

    public class ServiceSys : IServiceSys
    {
        static string MessageNotAuthen = "No authorization header was provided";
        static string UserAuthen = "admin";
        static string PwdAuthen = "2022*";

        public CompositeType GetDataUsingDataContract(CompositeType modelposite)
        {
            if (modelposite == null)
            {
                throw new ArgumentNullException("modelposite");
            }
            if (modelposite.BoolValue)
            {
                modelposite.StringValue += "Suffix";
            }
            return modelposite;
        }
        
        #region Sal
        public string getEmpSalaryList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpSalary objSal = new cls_ctTREmpSalary();
            List<cls_TREmpSalary> listSal = objSal.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listSal.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpSalary model in listSal)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("PeriodStart", model.PeriodStart);
                    json.Add("Salary", model.Salary);
                    json.Add("IncAmount", model.IncAmount);
                    json.Add("IncPercent", model.IncPercent);
                    json.Add("PreviousSalary", model.PreviousSalary);
                    json.Add("ReasonID", model.ReasonID);
                    json.Add("ReasonDesc", model.ReasonDesc);
                    json.Add("EmpType", model.EmpType);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpSalary(InputEmpSalary input)
        {
            JObject output = new JObject();

            try
            {
                JObject json = new JObject();
                JArray array = new JArray();
                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("PeriodStart", input.PeriodStart);
                json.Add("Salary", input.Salary);
                json.Add("IncAmount", input.IncAmount);
                json.Add("IncPercent", input.IncPercent);
                json.Add("PreviousSalary", input.PreviousSalary);
                json.Add("ReasonID", input.ReasonID);
                json.Add("ReasonDesc", input.ReasonDesc);
                json.Add("EmpType", input.EmpType);
                array.Add(json);
                output["value"] = array;


                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    //output["result"] = "0";
                    //output["result_text"] = MessageNotAuthen;

                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;
                    
                    this.doRecordLog(input.CompID, "SAL", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                cls_ctTREmpSalary objSal = new cls_ctTREmpSalary();
                cls_TREmpSalary model = new cls_TREmpSalary();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.PeriodStart = Convert.ToDateTime(input.PeriodStart);
                model.Salary = input.Salary;
                model.IncAmount = input.IncAmount;
                model.IncPercent = input.IncPercent;
                model.PreviousSalary = input.PreviousSalary;
                model.ReasonID = input.ReasonID;
                model.ReasonDesc = input.ReasonDesc;
                model.EmpType = input.EmpType;


                bool blnResult = objSal.insert(model);

                if (blnResult)
                {
                    //output["result"] = "1";
                    //output["result_text"] = "success 1 rows";

                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";


                    this.doRecordLog(input.CompID, "SAL", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    //output["result"] = "2";
                    //output["result_text"] = objSal.getMessage();
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "SAL", "2", objSal.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                //output["result"] = "0";
                //output["result_text"] = ex.ToString();

                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "SAL", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmpSalary(InputEmpSalary input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpSalary objSal = new cls_ctTREmpSalary();

                bool blnResult = objSal.delete(input.CompID, input.CompID, Convert.ToDateTime(input.PeriodStart));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objSal.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion
        
        #region Bank
        public string getEmpBankList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpBank objSal = new cls_ctTREmpBank();
            List<cls_TREmpBank> listSal = objSal.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listSal.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpBank model in listSal)
                {
                    JObject json = new JObject(); 

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("BankID", model.BankID);
                    json.Add("BankAccNo", model.BankAccNo);
                    json.Add("BankAccName", model.BankAccName);
                    json.Add("PayBankPercent", model.PayBankPercent);
                    json.Add("PayCashPercent", model.PayCashPercent);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpBank(InputEmpBank input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "BNK", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("BankID", input.BankID);
                json.Add("BankAccNo", input.BankAccNo);
                json.Add("BankAccName", input.BankAccName);
                json.Add("PayBankPercent", input.PayBankPercent);
                json.Add("PayCashPercent", input.PayCashPercent);
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;
                                
                cls_ctTREmpBank objBank = new cls_ctTREmpBank();
                cls_TREmpBank model = new cls_TREmpBank();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.BankID = input.BankID;
                model.BankAccNo = input.BankAccNo;
                model.BankAccName = input.BankAccName;
                model.PayBankPercent = input.PayBankPercent;
                model.PayCashPercent = input.PayCashPercent;

                bool blnResult = objBank.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "BNK", "1", "success 1 rows", input.ModifiedBy);

                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "BNK", "2", objBank.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "BNK", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmpBank(InputEmpBank input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpBank objBank = new cls_ctTREmpBank();

                bool blnResult = objBank.delete(input.CompID, input.CompID, input.BankID);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBank.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Part
        public string getEmpPartList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpPart objPart = new cls_ctTREmpPart();
            List<cls_TREmpPart> listPart = objPart.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listPart.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpPart model in listPart)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("PartEntDate", model.PartEntDate);
                    json.Add("Level01", model.Level01);
                    json.Add("Level02", model.Level02);
                    json.Add("Level03", model.Level03);
                    json.Add("Level04", model.Level04);
                    json.Add("Level05", model.Level05);
                    json.Add("Level06", model.Level06);
                    json.Add("Level07", model.Level07);
                    json.Add("Level08", model.Level08);
                    json.Add("Level09", model.Level09);
                    json.Add("Level10", model.Level10);
     
                    json.Add("IsActive", model.IsActive);
                    json.Add("CommentP", model.CommentP);
        
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpPart(InputEmpPart input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "PRT", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("PartEntDate", input.PartEntDate);
                json.Add("Level01", input.Level01);
                json.Add("Level02", input.Level02);
                json.Add("Level03", input.Level03);
                json.Add("Level04", input.Level04);
                json.Add("Level05", input.Level05);
                json.Add("Level06", input.Level06);
                json.Add("Level07", input.Level07);
                json.Add("Level08", input.Level08);
                json.Add("Level09", input.Level09);
                json.Add("Level10", input.Level10);
                json.Add("IsActive", input.IsActive);
                json.Add("CommentP", input.CommentP);
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;


                cls_ctTREmpPart objPart = new cls_ctTREmpPart();
                cls_TREmpPart model = new cls_TREmpPart();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.PartEntDate = Convert.ToDateTime(input.PartEntDate);
                model.Level01 = input.Level01;
                model.Level02 = input.Level02;
                model.Level03 = input.Level03;
                model.Level04 = input.Level04;
                model.Level05 = input.Level05;
                model.Level06 = input.Level06;
                model.Level07 = input.Level07;
                model.Level08 = input.Level08;
                model.Level09 = input.Level09;
                model.Level10 = input.Level10;

                model.IsActive = input.IsActive;
                model.CommentP = input.CommentP;

                bool blnResult = objPart.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "PRT", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "PRT", "2", objPart.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "PRT", "0", ex.ToString(), input.ModifiedBy);

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmpPart(InputEmpPart input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpPart objPart = new cls_ctTREmpPart();

                bool blnResult = objPart.delete(input.CompID, input.CompID, Convert.ToDateTime(input.PartEntDate));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPart.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Position
        public string getEmpPositionList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpPosition objPart = new cls_ctTREmpPosition();
            List<cls_TREmpPosition> listPart = objPart.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listPart.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpPosition model in listPart)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("PositionID", model.PositionID);
                    json.Add("PositionDate", model.PositionDate);
                    json.Add("ReasonID", model.ReasonID);
                    json.Add("Description", model.Description);
                    json.Add("JobLevelID", model.JobLevelID);
                    json.Add("JDID", model.JDID);
                    json.Add("UpdateDate", model.UpdateDate);
                    json.Add("PositionName2", model.PositionName2);
                    json.Add("JobDescDate", model.JobDescDate);
                    json.Add("PositionID2", model.PositionID2);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpPosition(InputEmpPosition input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "POS", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("PositionID", input.PositionID);
                json.Add("PositionDate", input.PositionDate);
                json.Add("ReasonID", input.ReasonID);
                json.Add("Description", input.Description);
                json.Add("JobLevelID", input.JobLevelID);
                json.Add("JDID", input.JDID);
                json.Add("UpdateDate", input.UpdateDate);
                json.Add("PositionName2", input.PositionName2);
                json.Add("JobDescDate", input.JobDescDate);
                json.Add("PositionID2", input.PositionID2);
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctTREmpPosition objPosition = new cls_ctTREmpPosition();
                cls_TREmpPosition model = new cls_TREmpPosition();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.PositionID = input.PositionID;
                model.PositionDate = Convert.ToDateTime(input.PositionDate);
                
                model.ReasonID = input.ReasonID;
                model.Description = input.Description;
                model.JobLevelID = input.JobLevelID;
                model.JDID = input.JDID;
                model.UpdateDate =  Convert.ToDateTime(input.UpdateDate);
                model.PositionName2 = input.PositionName2;
                model.JobDescDate =  Convert.ToDateTime(input.JobDescDate);
                model.PositionID2 = input.PositionID2;

                bool blnResult = objPosition.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "POS", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "POS", "2", objPosition.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "POS", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmpPosition(InputEmpPosition input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpPosition objPosition = new cls_ctTREmpPosition();

                bool blnResult = objPosition.delete(input.CompID, input.CompID, Convert.ToDateTime(input.PositionDate));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPosition.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Benefit
        public string getEmpBenefitList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpBenefit objBenefit = new cls_ctTREmpBenefit();
            List<cls_TREmpBenefit> listBenefit = objBenefit.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listBenefit.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpBenefit model in listBenefit)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("AllowanceDeductID", model.AllowanceDeductID);
                    json.Add("Amount", model.Amount);
                    json.Add("PayCondition", model.PayCondition);
                    json.Add("StartDate", model.StartDate);
                    json.Add("EndDate", model.EndDate);
                    json.Add("PayAtFirstPeriod", model.PayAtFirstPeriod);
                    json.Add("PayType", model.PayType);
                    json.Add("Reason", model.Reason);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpBenefit(InputEmpBenefit input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {

                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "BNF", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("AllowanceDeductID", input.AllowanceDeductID);
                json.Add("Amount", input.Amount);
                json.Add("PayCondition", input.PayCondition);
                json.Add("StartDate", input.StartDate);
                json.Add("EndDate", input.EndDate);
                json.Add("PayAtFirstPeriod", input.PayAtFirstPeriod);
                json.Add("PayType", input.PayType);
                json.Add("Reason", input.Reason);
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctTREmpBenefit objBenefit = new cls_ctTREmpBenefit();
                cls_TREmpBenefit model = new cls_TREmpBenefit();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.AllowanceDeductID = input.AllowanceDeductID;
                model.Amount = input.Amount;
                model.PayCondition = input.PayCondition;        
                model.StartDate = Convert.ToDateTime(input.StartDate);
                model.EndDate = Convert.ToDateTime(input.EndDate);
                model.PayAtFirstPeriod = input.PayAtFirstPeriod;
                model.PayType = input.PayType;
                model.Reason = input.Reason;

                bool blnResult = objBenefit.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "BNF", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "BNF", "2", objBenefit.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "BNF", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmpBenefit(InputEmpBenefit input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpBenefit objBenefit = new cls_ctTREmpBenefit();

                bool blnResult = objBenefit.delete(input.CompID, input.EmpID, input.AllowanceDeductID, Convert.ToDateTime(input.StartDate), Convert.ToDateTime(input.EndDate));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBenefit.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Emp
        public string getEmpList(string com, string emp)
        {
            JObject output = new JObject();

           

            cls_ctMTEmpMain objEmp = new cls_ctMTEmpMain();
            List<cls_MTEmpMain> listEmp = objEmp.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listEmp.Count > 0)
            {
                int index = 1;

                foreach (cls_MTEmpMain model in listEmp)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("EmpIDRef", model.EmpIDRef);
                    json.Add("BranchID", model.BranchID);
                    json.Add("InitialID", model.InitialID);
                    json.Add("EmpFName", model.EmpFName);
                    json.Add("EmpLName", model.EmpLName);
                    json.Add("EmpFNameT", model.EmpFNameT);
                    json.Add("EmpLNameT", model.EmpLNameT);
                    json.Add("EmpNickname", model.EmpNickname);
                    json.Add("EmpType", model.EmpType);
                    json.Add("EmpStatus", model.EmpStatus);
                    json.Add("WorkStartDate", model.WorkStartDate);
                    json.Add("ProbationEndDate", model.ProbationEndDate);
                    json.Add("ProbationLimit", model.ProbationLimit);
                    json.Add("PayPerBank", model.PayPerBank);
                    json.Add("PayPerCash", model.PayPerCash);
                    json.Add("BankAccNoComp", model.BankAccNoComp);
                    json.Add("ResignStatus", model.ResignStatus);
                    json.Add("ResignDate", model.ResignDate);
                    json.Add("ResignReasonID", model.ResignReasonID);
                    json.Add("ResignDetial", model.ResignDetial);
                    json.Add("HrsPerDay", model.HrsPerDay);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }
            

            return output.ToString(Formatting.None);
        }
        public string doManageEmp(InputEmpMain input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "EMP", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("EmpIDRef", input.EmpIDRef);
                json.Add("BranchID", input.BranchID);
                json.Add("InitialID", input.InitialID);
                json.Add("EmpFName", input.EmpFName);
                json.Add("EmpLName", input.EmpLName);
                json.Add("EmpFNameT", input.EmpFNameT);
                json.Add("EmpLNameT", input.EmpLNameT);
                json.Add("EmpNickname", input.EmpNickname);
                json.Add("EmpType", input.EmpType);
                json.Add("EmpStatus", input.EmpStatus);
                json.Add("WorkStartDate", input.WorkStartDate);
                json.Add("ProbationEndDate", input.ProbationEndDate);
                json.Add("ProbationLimit", input.ProbationLimit);
                json.Add("PayPerBank", input.PayPerBank);
                json.Add("PayPerCash", input.PayPerCash);
                json.Add("BankAccNoComp", input.BankAccNoComp);
                json.Add("ResignStatus", input.ResignStatus);
                json.Add("ResignDate", input.ResignDate);
                json.Add("ResignReasonID", input.ResignReasonID);
                json.Add("ResignDetial", input.ResignDetial);
                json.Add("HrsPerDay", input.HrsPerDay);

                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctMTEmpMain objEmp = new cls_ctMTEmpMain();
                cls_MTEmpMain model = new cls_MTEmpMain();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.EmpIDRef = input.EmpIDRef;
                model.BranchID = input.BranchID;
                model.InitialID = input.InitialID;
                model.EmpFName = input.EmpFName;
                model.EmpLName = input.EmpLName;
                model.EmpFNameT = input.EmpFNameT;
                model.EmpLNameT = input.EmpLNameT;
                model.EmpNickname = input.EmpNickname;
                model.EmpType = input.EmpType;
                model.EmpStatus = input.EmpStatus;
                model.WorkStartDate = Convert.ToDateTime(input.WorkStartDate);
                model.ProbationEndDate = Convert.ToDateTime(input.ProbationEndDate);
                model.ProbationLimit = input.ProbationLimit;
                model.PayPerBank = input.PayPerBank;
                model.PayPerCash = input.PayPerCash;
                model.BankAccNoComp = input.BankAccNoComp;
                model.ResignStatus = input.ResignStatus;
                model.ResignDate = Convert.ToDateTime(input.ResignDate);
                model.ResignReasonID = input.ResignReasonID;
                model.ResignDetial = input.ResignDetial;
                model.HrsPerDay = input.HrsPerDay;
                
                bool blnResult = objEmp.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";
                    this.doRecordLog(input.CompID, "EMP", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "EMP", "2", objEmp.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "EMP", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteEmp(InputEmpMain input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTEmpMain objEmp = new cls_ctMTEmpMain();

                bool blnResult = objEmp.delete(input.CompID, input.CompID);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objEmp.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Emp detail
        public string getEmpDetailList(string token, string com, string emp)
        {
            JObject output = new JObject();

            cls_ctMTEmpDetail objEmp = new cls_ctMTEmpDetail();
            List<cls_MTEmpDetail> listEmp = objEmp.getDataByFillter(com, emp);

            JArray array = new JArray();

            if (listEmp.Count > 0)
            {
                int index = 1;

                foreach (cls_MTEmpDetail model in listEmp)
                {
                    JObject json = new JObject();

                    json.Add("CompID", model.CompID);
                    json.Add("EmpID", model.EmpID);
                    json.Add("BirthDay", model.BirthDay);
                    json.Add("Sex", model.Sex);
                    json.Add("Height", model.Height);
                    json.Add("Weight", model.Weight);
                    json.Add("MaritalStatus", model.MaritalStatus);
                    json.Add("MilitaryStatus", model.MilitaryStatus);
                    json.Add("LanguageID", model.LanguageID);
                    json.Add("BloodID", model.BloodID);
                    json.Add("NationID", model.NationID);
                    json.Add("OriginID", model.OriginID);
                    json.Add("ReligionID", model.ReligionID);
                    json.Add("CardNo", model.CardNo);
                    json.Add("CardNoIssueDate", model.CardNoIssueDate);
                    json.Add("CardNoExpireDate", model.CardNoExpireDate);
                    json.Add("SocialNo", model.SocialNo);
                    json.Add("SocialIssueDate", model.SocialIssueDate);
                    json.Add("SocialExpireDate", model.SocialExpireDate);
                    json.Add("SSOStatusSent", model.SSOStatusSent);
                    json.Add("PassportNo", model.PassportNo);
                    json.Add("PassportIssueDate", model.PassportIssueDate);
                    json.Add("PassportExpireDate", model.PassportExpireDate);
                    json.Add("TaxNo", model.TaxNo);
                    json.Add("TaxIssueDate", model.TaxIssueDate);
                    json.Add("TaxExpireDate", model.TaxExpireDate);
                    json.Add("PFNo", model.PFNo);
                    json.Add("PFType", model.PFType);
                    json.Add("PFEnterDate", model.PFEnterDate);
                    json.Add("PFStartDate", model.PFStartDate);
                    json.Add("PFPerComp", model.PFPerComp);
                    json.Add("PFPerEmp", model.PFPerEmp);                    

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageEmpDetail(InputEmpDetail input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "EMD", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("EmpID", input.EmpID);
                json.Add("BirthDay", input.BirthDay);
                json.Add("Sex", input.Sex);
                json.Add("Height", input.Height);
                json.Add("Weight", input.Weight);
                json.Add("MaritalStatus", input.MaritalStatus);
                json.Add("MilitaryStatus", input.MilitaryStatus);
                json.Add("LanguageID", input.LanguageID);
                json.Add("BloodID", input.BloodID);
                json.Add("NationID", input.NationID);
                json.Add("OriginID", input.OriginID);
                json.Add("ReligionID", input.ReligionID);
                json.Add("CardNo", input.CardNo);
                json.Add("CardNoIssueDate", input.CardNoIssueDate);
                json.Add("CardNoExpireDate", input.CardNoExpireDate);
                json.Add("SocialNo", input.SocialNo);
                json.Add("SocialIssueDate", input.SocialIssueDate);
                json.Add("SocialExpireDate", input.SocialExpireDate);
                json.Add("SSOStatusSent", input.SSOStatusSent);
                json.Add("PassportNo", input.PassportNo);
                json.Add("PassportIssueDate", input.PassportIssueDate);
                json.Add("PassportExpireDate", input.PassportExpireDate);
                json.Add("TaxNo", input.TaxNo);
                json.Add("TaxIssueDate", input.TaxIssueDate);
                json.Add("TaxExpireDate", input.TaxExpireDate);
                json.Add("PFNo", input.PFNo);
                json.Add("PFType", input.PFType);
                json.Add("PFEnterDate", input.PFEnterDate);
                json.Add("PFStartDate", input.PFStartDate);
                json.Add("PFPerComp", input.PFPerComp);
                json.Add("PFPerEmp", input.PFPerEmp);

                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctMTEmpDetail objEmp = new cls_ctMTEmpDetail();
                cls_MTEmpDetail model = new cls_MTEmpDetail();

                model.CompID = input.CompID;
                model.EmpID = input.EmpID;
                model.BirthDay = Convert.ToDateTime(input.BirthDay);
                model.Sex = input.Sex;
                model.Height = input.Height;
                model.Weight = input.Weight;
                model.MaritalStatus = input.MaritalStatus;
                model.MilitaryStatus = input.MilitaryStatus;
                model.LanguageID = input.LanguageID;
                model.BloodID = input.BloodID;
                model.NationID = input.NationID;
                model.OriginID = input.OriginID;
                model.ReligionID = input.ReligionID;
                model.CardNo = input.CardNo;
                model.CardNoIssueDate = Convert.ToDateTime(input.CardNoIssueDate);
                model.CardNoExpireDate = Convert.ToDateTime(input.CardNoExpireDate);
                model.SocialNo = input.SocialNo;
                model.SocialIssueDate = Convert.ToDateTime(input.SocialIssueDate);
                model.SocialExpireDate = Convert.ToDateTime(input.SocialExpireDate);
                model.SSOStatusSent = input.SSOStatusSent;
                model.PassportNo = input.PassportNo;
                model.PassportIssueDate = Convert.ToDateTime(input.PassportIssueDate);
                model.PassportExpireDate = Convert.ToDateTime(input.PassportExpireDate);
                model.TaxNo = input.TaxNo;
                model.TaxIssueDate = Convert.ToDateTime(input.TaxIssueDate);
                model.TaxExpireDate = Convert.ToDateTime(input.TaxExpireDate);
                model.PFNo = input.PFNo;
                model.PFType = input.PFType;
                model.PFEnterDate = Convert.ToDateTime(input.PFEnterDate);
                model.PFStartDate = Convert.ToDateTime(input.PFStartDate);
                model.PFPerComp = input.PFPerComp;
                model.PFPerEmp = input.PFPerEmp;
                
                bool blnResult = objEmp.update(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "EMD", "1", "success 1 rows", input.ModifiedBy);
                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "EMD", "2", objEmp.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "EMD", "0", ex.ToString(), input.ModifiedBy);

            }

            return output.ToString(Formatting.None);

        }
       
        #endregion

        public string doManagePosition(InputPosition input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "MPS", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("PositionID", input.PositionID);
                json.Add("PositionNameT", input.PositionNameT);
                json.Add("PositionNameE", input.PositionNameE);                
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctMTPosition objPosition = new cls_ctMTPosition();
                cls_MTPosition model = new cls_MTPosition();

                model.CompID = input.CompID;
                model.PositionID = input.PositionID;
                model.PositionNameT = input.PositionNameT;
                model.PositionNameE = input.PositionNameE;
                
                bool blnResult = objPosition.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "MPS", "1", "success 1 rows", input.ModifiedBy);

                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "MPS", "2", objPosition.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "MPS", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }

        public string doManagePart(InputPart input)
        {
            JObject output = new JObject();

            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (authHeader == null || !doVerify(authHeader.Substring(7)))
                {
                    output["error"] = "true";
                    output["message"] = MessageNotAuthen;

                    this.doRecordLog(input.CompID, "MPT", "0", MessageNotAuthen, input.ModifiedBy);

                    return output.ToString(Formatting.None);
                }

                JObject json = new JObject();

                json.Add("CompID", input.CompID);
                json.Add("LevelID", input.LevelID);
                json.Add("PartID", input.PartID);
                json.Add("PartNameT", input.PartNameT);
                json.Add("PartNameE", input.PartNameE);
                json.Add("PartRef", input.PartRef);
                json.Add("LevelRef", input.LevelRef);
                
                JArray array = new JArray();
                array.Add(json);
                output["value"] = array;

                cls_ctMTPart objPart = new cls_ctMTPart();
                cls_MTPart model = new cls_MTPart();

                model.CompID = input.CompID;
                model.LevelID = input.LevelID;
                model.PartID = input.PartID;
                model.PartNameT = input.PartNameT;
                model.PartNameE = input.PartNameE;
                model.PartRef = input.PartRef;
                model.LevelRef = input.LevelRef;
               
                bool blnResult = objPart.insert(model);

                if (blnResult)
                {
                    output["error"] = "false";
                    output["message"] = "Retrieved data successfully";

                    this.doRecordLog(input.CompID, "MPT", "1", "success 1 rows", input.ModifiedBy);

                }
                else
                {
                    output["error"] = "true";
                    output["message"] = "Retrieved data not successfully";

                    this.doRecordLog(input.CompID, "MPT", "2", objPart.getMessage(), input.ModifiedBy);
                }

            }
            catch (Exception ex)
            {
                output["error"] = "true";
                output["message"] = "Retrieved data not successfully";

                this.doRecordLog(input.CompID, "MPT", "0", ex.ToString(), input.ModifiedBy);
            }

            return output.ToString(Formatting.None);

        }

        public string getLog(string com, string fromdate, string todate)
        {
            JObject output = new JObject();

            cls_ctSYSLogApi objLog = new cls_ctSYSLogApi();
            List<cls_SYSLogApi> listLog = objLog.getDataByFillter(com, "", Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));

            JArray array = new JArray();

            if (listLog.Count > 0)
            {
                int index = 1;

                foreach (cls_SYSLogApi model in listLog)
                {
                    JObject json = new JObject();


                    json.Add("CompID", model.CompID);
                    json.Add("ID", model.APIID);
                    json.Add("Type", model.APIType);
                    json.Add("Status", model.APIStatus);
                    json.Add("Detail", model.APIDetail);
                    json.Add("CreateBy", model.CreateBy);
                    json.Add("CreateDate", model.CreateDate);
                
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }


        string UserToken = string.Empty;
        public string doAuthen(RequestData input)
        {
            JObject output = new JObject();

            if (input.usname.Equals(UserAuthen) && input.pwd.Equals(PwdAuthen))
            {
                RequestData aa = new RequestData();
                aa.usname = input.usname;
                aa.pwd = input.pwd;

                ResponseData bb = Login(aa);
                
                output["result"] = "1";
                output["result_text"] = bb.token;

            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "No access rights";
            }

            return output.ToString(Formatting.None);

        }


        private ResponseData Login(RequestData data)
        {

            Authen objAuthen = new Authen();

            string secureToken = objAuthen.GetJwt(data.usname, data.pwd);
            var response = new ResponseData
            {
                token = secureToken,
                authenticated = true,
                employeeId = data.usname,
                firstname = data.usname,
                timestamp = DateTime.Now,
                userName = data.usname
            };        


            return response;
        }

        private bool doVerify(string token)
        {
            try
            {
                Authen objAuthen = new Authen();

                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(token);

                var usr = decodedValue.Claims.Single(claim => claim.Type == "user_aabbcc");
                var pwd = decodedValue.Claims.Single(claim => claim.Type == "pass_qwer");
                var iat = decodedValue.Claims.Single(claim => claim.Type == "iat");


                if (usr.Value.Equals(UserAuthen) && pwd.Value.Equals(PwdAuthen))                
                {

                    if (objAuthen.doCheckExpireToken(iat.Value))
                        return true;
                    else
                        return false;

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        private bool doRecordLog(string com, string type, string status, string detail, string by)
        {
            bool blnResult = false;

            try
            {

                cls_ctSYSLogApi objLog = new cls_ctSYSLogApi();
                cls_SYSLogApi model = new cls_SYSLogApi();

                model.CompID = com;             
                model.APIType = type;
                model.APIStatus = status;
                model.APIDetail = detail;
                model.CreateBy = by;


                blnResult = objLog.insert(model);


            }
            catch (Exception ex)
            {
                blnResult = false;

            }

            return blnResult;

        }
    }

    public class RequestData
    {
        public RequestData() { }
        public string usname { get; set; }
        public string pwd { get; set; }
    }  

    
}
