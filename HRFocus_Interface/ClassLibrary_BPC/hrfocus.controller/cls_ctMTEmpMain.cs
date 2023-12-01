using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctMTEmpMain
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTEmpMain() { }

        public string getMessage() { return this.Message.Replace("tbMTEmpMain", "").Replace("cls_ctMTEmpMain", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTEmpMain> getData(string condition)
        {
            List<cls_MTEmpMain> list_model = new List<cls_MTEmpMain>();
            cls_MTEmpMain model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", EmpIDRef");
                obj_str.Append(", BranchID");
                obj_str.Append(", InitialID");
                obj_str.Append(", EmpFName");
                obj_str.Append(", EmpLName");
                obj_str.Append(", EmpFNameT");
                obj_str.Append(", EmpLNameT");
                obj_str.Append(", EmpNickname");
                obj_str.Append(", EmpType");
                obj_str.Append(", EmpStatus");
                obj_str.Append(", WorkStartDate");
                obj_str.Append(", ProbationEndDate");
                obj_str.Append(", ProbationLimit");     
                obj_str.Append(", PayPerBank");
                obj_str.Append(", PayPerCash");
                obj_str.Append(", BankAccNoComp");     
                obj_str.Append(", HrsPerDay");
                obj_str.Append(", ISNULL(ResignStatus, 0) AS ResignStatus");
                obj_str.Append(", ISNULL(ResignDate, '') AS ResignDate");
                obj_str.Append(", ISNULL(ResignReasonID, '') AS ResignReasonID");
                obj_str.Append(", ISNULL(ResignDetial, '') AS ResignDetial");    
 
                obj_str.Append(" FROM tbMTEmpMain");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTEmpMain();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.EmpIDRef = dr["EmpIDRef"].ToString();
                    model.BranchID = dr["BranchID"].ToString();
                    model.InitialID = dr["InitialID"].ToString();
                    model.EmpFName = dr["EmpFName"].ToString();
                    model.EmpLName = dr["EmpLName"].ToString();
                    model.EmpFNameT = dr["EmpFNameT"].ToString();
                    model.EmpLNameT = dr["EmpLNameT"].ToString();
                    model.EmpNickname = dr["EmpNickname"].ToString();
                    model.EmpType = dr["EmpType"].ToString();
                    model.EmpStatus = dr["EmpStatus"].ToString();
                    model.WorkStartDate = Convert.ToDateTime(dr["WorkStartDate"]);
                    model.ProbationEndDate = Convert.ToDateTime(dr["ProbationEndDate"]);
                    model.ProbationLimit = Convert.ToInt32(dr["ProbationLimit"]);
                    model.PayPerBank = Convert.ToInt32(dr["PayPerBank"]);
                    model.PayPerCash = Convert.ToInt32(dr["PayPerCash"]);
                    model.BankAccNoComp = dr["BankAccNoComp"].ToString();
                    model.ResignStatus = Convert.ToBoolean(dr["ResignStatus"]);
                    model.ResignDate = Convert.ToDateTime(dr["ResignDate"]);
                    model.ResignReasonID = dr["ResignReasonID"].ToString();
                    model.ResignDetial = dr["ResignDetial"].ToString();
                    model.HrsPerDay = Convert.ToDouble(dr["HrsPerDay"]);
             
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Emp.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTEmpMain> getDataByFillter(string com, string emp,string from ,string to,string status,string type)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";

            if (!from.Equals("") && !to.Equals(""))
                strCondition += " AND (WorkStartDate BETWEEN '" + Convert.ToDateTime(from).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(to).ToString("yyyy-MM-dd") + "')";

            if (status.Equals("Y"))
                strCondition += " AND ResignStatus=" + 1 + "";

            if (status.Equals("N"))
                strCondition += " AND ResignStatus=" + 0 + "";

            if (type.Equals("M") || type.Equals("D"))
                strCondition += " AND EmpType='" + type + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM tbMTEmpMain");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emp.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbMTEmpMain");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emp.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTEmpMain model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbMTEmpMain");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", EmpIDRef ");
                obj_str.Append(", BranchID ");
                obj_str.Append(", InitialID ");
                obj_str.Append(", EmpFName ");
                obj_str.Append(", EmpLName ");
                obj_str.Append(", EmpFNameT ");
                obj_str.Append(", EmpLNameT ");
                obj_str.Append(", EmpNickname ");
                obj_str.Append(", EmpType ");
                obj_str.Append(", EmpStatus ");
                obj_str.Append(", WorkStartDate ");
                obj_str.Append(", ProbationEndDate ");
                obj_str.Append(", ProbationLimit ");
                obj_str.Append(", PayPerBank ");
                obj_str.Append(", PayPerCash ");
                obj_str.Append(", BankAccNoComp ");
                obj_str.Append(", ResignStatus ");
                obj_str.Append(", ResignDate ");
                obj_str.Append(", ResignReasonID ");
                obj_str.Append(", ResignDetial ");
                obj_str.Append(", HrsPerDay ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @EmpIDRef ");
                obj_str.Append(", @BranchID ");
                obj_str.Append(", @InitialID ");
                obj_str.Append(", @EmpFName ");
                obj_str.Append(", @EmpLName ");
                obj_str.Append(", @EmpFNameT ");
                obj_str.Append(", @EmpLNameT ");
                obj_str.Append(", @EmpNickname ");
                obj_str.Append(", @EmpType ");
                obj_str.Append(", @EmpStatus ");
                obj_str.Append(", @WorkStartDate ");
                obj_str.Append(", @ProbationEndDate ");
                obj_str.Append(", @ProbationLimit ");
                obj_str.Append(", @PayPerBank ");
                obj_str.Append(", @PayPerCash ");
                obj_str.Append(", @BankAccNoComp ");
                obj_str.Append(", @ResignStatus ");
                obj_str.Append(", @ResignDate ");
                obj_str.Append(", @ResignReasonID ");
                obj_str.Append(", @ResignDetial ");
                obj_str.Append(", @HrsPerDay ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@EmpIDRef", SqlDbType.VarChar); obj_cmd.Parameters["@EmpIDRef"].Value = model.EmpIDRef;
                obj_cmd.Parameters.Add("@BranchID", SqlDbType.VarChar); obj_cmd.Parameters["@BranchID"].Value = model.BranchID;
                obj_cmd.Parameters.Add("@InitialID", SqlDbType.VarChar); obj_cmd.Parameters["@InitialID"].Value = model.InitialID;
                obj_cmd.Parameters.Add("@EmpFName", SqlDbType.VarChar); obj_cmd.Parameters["@EmpFName"].Value = model.EmpFName;
                obj_cmd.Parameters.Add("@EmpLName", SqlDbType.VarChar); obj_cmd.Parameters["@EmpLName"].Value = model.EmpLName;
                obj_cmd.Parameters.Add("@EmpFNameT", SqlDbType.VarChar); obj_cmd.Parameters["@EmpFNameT"].Value = model.EmpFNameT;
                obj_cmd.Parameters.Add("@EmpLNameT", SqlDbType.VarChar); obj_cmd.Parameters["@EmpLNameT"].Value = model.EmpLNameT;
                obj_cmd.Parameters.Add("@EmpNickname", SqlDbType.VarChar); obj_cmd.Parameters["@EmpNickname"].Value = model.EmpNickname;
                obj_cmd.Parameters.Add("@EmpType", SqlDbType.VarChar); obj_cmd.Parameters["@EmpType"].Value = model.EmpType;
                obj_cmd.Parameters.Add("@EmpStatus", SqlDbType.VarChar); obj_cmd.Parameters["@EmpStatus"].Value = model.EmpStatus;
                obj_cmd.Parameters.Add("@WorkStartDate", SqlDbType.DateTime); obj_cmd.Parameters["@WorkStartDate"].Value = model.WorkStartDate;
                obj_cmd.Parameters.Add("@ProbationEndDate", SqlDbType.DateTime); obj_cmd.Parameters["@ProbationEndDate"].Value = model.ProbationEndDate;
                obj_cmd.Parameters.Add("@ProbationLimit", SqlDbType.Decimal); obj_cmd.Parameters["@ProbationLimit"].Value = model.ProbationLimit;
                obj_cmd.Parameters.Add("@PayPerBank", SqlDbType.Decimal); obj_cmd.Parameters["@PayPerBank"].Value = model.PayPerBank;
                obj_cmd.Parameters.Add("@PayPerCash", SqlDbType.Decimal); obj_cmd.Parameters["@PayPerCash"].Value = model.PayPerCash;
                obj_cmd.Parameters.Add("@BankAccNoComp", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccNoComp"].Value = model.BankAccNoComp;
                obj_cmd.Parameters.Add("@ResignStatus", SqlDbType.Bit); obj_cmd.Parameters["@ResignStatus"].Value = model.ResignStatus;
                obj_cmd.Parameters.Add("@ResignDate", SqlDbType.DateTime); obj_cmd.Parameters["@ResignDate"].Value = model.ResignDate;
                obj_cmd.Parameters.Add("@ResignReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ResignReasonID"].Value = model.ResignReasonID;
                obj_cmd.Parameters.Add("@ResignDetial", SqlDbType.VarChar); obj_cmd.Parameters["@ResignDetial"].Value = model.ResignDetial;
                obj_cmd.Parameters.Add("@HrsPerDay", SqlDbType.Decimal); obj_cmd.Parameters["@HrsPerDay"].Value = model.HrsPerDay;
       
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emp.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTEmpMain model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbMTEmpMain SET ");

                obj_str.Append(" EmpIDRef=@EmpIDRef ");
                obj_str.Append(", BranchID=@BranchID ");
                obj_str.Append(", InitialID=@InitialID ");
                obj_str.Append(", EmpFName=@EmpFName ");
                obj_str.Append(", EmpLName=@EmpLName ");
                obj_str.Append(", EmpFNameT=@EmpFNameT ");
                obj_str.Append(", EmpLNameT=@EmpLNameT ");
                obj_str.Append(", EmpNickname=@EmpNickname ");
                obj_str.Append(", EmpType=@EmpType ");
                obj_str.Append(", EmpStatus=@EmpStatus ");
                obj_str.Append(", WorkStartDate=@WorkStartDate ");
                obj_str.Append(", ProbationEndDate=@ProbationEndDate ");
                obj_str.Append(", ProbationLimit=@ProbationLimit ");
                obj_str.Append(", PayPerBank=@PayPerBank ");
                obj_str.Append(", PayPerCash=@PayPerCash ");
                obj_str.Append(", BankAccNoComp=@BankAccNoComp ");
                obj_str.Append(", ResignStatus=@ResignStatus ");
                obj_str.Append(", ResignDate=@ResignDate ");
                obj_str.Append(", ResignReasonID=@ResignReasonID ");
                obj_str.Append(", ResignDetial=@ResignDetial ");
                obj_str.Append(", HrsPerDay=@HrsPerDay ");

                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                
                obj_cmd.Parameters.Add("@EmpIDRef", SqlDbType.VarChar); obj_cmd.Parameters["@EmpIDRef"].Value = model.EmpIDRef;
                obj_cmd.Parameters.Add("@BranchID", SqlDbType.VarChar); obj_cmd.Parameters["@BranchID"].Value = model.BranchID;
                obj_cmd.Parameters.Add("@InitialID", SqlDbType.VarChar); obj_cmd.Parameters["@InitialID"].Value = model.InitialID;
                obj_cmd.Parameters.Add("@EmpFName", SqlDbType.VarChar); obj_cmd.Parameters["@EmpFName"].Value = model.EmpFName;
                obj_cmd.Parameters.Add("@EmpLName", SqlDbType.VarChar); obj_cmd.Parameters["@EmpLName"].Value = model.EmpLName;
                obj_cmd.Parameters.Add("@EmpFNameT", SqlDbType.VarChar); obj_cmd.Parameters["@EmpFNameT"].Value = model.EmpFNameT;
                obj_cmd.Parameters.Add("@EmpLNameT", SqlDbType.VarChar); obj_cmd.Parameters["@EmpLNameT"].Value = model.EmpLNameT;
                obj_cmd.Parameters.Add("@EmpNickname", SqlDbType.VarChar); obj_cmd.Parameters["@EmpNickname"].Value = model.EmpNickname;
                obj_cmd.Parameters.Add("@EmpType", SqlDbType.VarChar); obj_cmd.Parameters["@EmpType"].Value = model.EmpType;
                obj_cmd.Parameters.Add("@EmpStatus", SqlDbType.VarChar); obj_cmd.Parameters["@EmpStatus"].Value = model.EmpStatus;
                obj_cmd.Parameters.Add("@WorkStartDate", SqlDbType.DateTime); obj_cmd.Parameters["@WorkStartDate"].Value = model.WorkStartDate;
                obj_cmd.Parameters.Add("@ProbationEndDate", SqlDbType.DateTime); obj_cmd.Parameters["@ProbationEndDate"].Value = model.ProbationEndDate;
                obj_cmd.Parameters.Add("@ProbationLimit", SqlDbType.Decimal); obj_cmd.Parameters["@ProbationLimit"].Value = model.ProbationLimit;
                obj_cmd.Parameters.Add("@PayPerBank", SqlDbType.Decimal); obj_cmd.Parameters["@PayPerBank"].Value = model.PayPerBank;
                obj_cmd.Parameters.Add("@PayPerCash", SqlDbType.Decimal); obj_cmd.Parameters["@PayPerCash"].Value = model.PayPerCash;
                obj_cmd.Parameters.Add("@BankAccNoComp", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccNoComp"].Value = model.BankAccNoComp;
                obj_cmd.Parameters.Add("@ResignStatus", SqlDbType.Bit); obj_cmd.Parameters["@ResignStatus"].Value = model.ResignStatus;
                obj_cmd.Parameters.Add("@ResignDate", SqlDbType.DateTime); obj_cmd.Parameters["@ResignDate"].Value = model.ResignDate;
                obj_cmd.Parameters.Add("@ResignReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ResignReasonID"].Value = model.ResignReasonID;
                obj_cmd.Parameters.Add("@ResignDetial", SqlDbType.VarChar); obj_cmd.Parameters["@ResignDetial"].Value = model.ResignDetial;
                obj_cmd.Parameters.Add("@HrsPerDay", SqlDbType.Decimal); obj_cmd.Parameters["@HrsPerDay"].Value = model.HrsPerDay;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emp.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
