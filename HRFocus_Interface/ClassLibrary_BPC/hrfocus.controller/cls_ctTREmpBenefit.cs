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
    public class cls_ctTREmpBenefit
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpBenefit() { }

        public string getMessage() { return this.Message.Replace("tbTREmpBenefit", "").Replace("cls_ctTREmpBenefit", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpBenefit> getData(string condition)
        {
            List<cls_TREmpBenefit> list_model = new List<cls_TREmpBenefit>();
            cls_TREmpBenefit model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", AllowanceDeductID");
                obj_str.Append(", Amount");
                obj_str.Append(", PayCondition");
                obj_str.Append(", StartDate");
                obj_str.Append(", EndDate");
                obj_str.Append(", PayAtFirstPeriod");
                obj_str.Append(", PayType");
                obj_str.Append(", Reason");

                obj_str.Append(" FROM tbTREmpBenefit");
                obj_str.Append(" WHERE 1=1");
                
                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID, StartDate DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpBenefit();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.AllowanceDeductID = dr["AllowanceDeductID"].ToString();
                    model.Amount = Convert.ToDouble(dr["Amount"]);
                    model.PayCondition = dr["PayCondition"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.PayAtFirstPeriod = Convert.ToBoolean(dr["PayAtFirstPeriod"]);
                    model.PayType = dr["PayType"].ToString();
                    model.Reason = dr["Reason"].ToString();
                                                                         
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Benefit.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpBenefit> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string id, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM tbTREmpBenefit");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND AllowanceDeductID='" + id + "'");
                obj_str.Append(" AND StartDate='" + date.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Benefit.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp, string id, DateTime start, DateTime end)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbTREmpBenefit");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND AllowanceDeductID='" + id + "'");
                obj_str.Append(" AND StartDate='" + start.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND EndDate='" + end.ToString("MM/dd/yyyy") + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Benefit.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpBenefit model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID, model.AllowanceDeductID, model.StartDate.Date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbTREmpBenefit");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", AllowanceDeductID ");
                obj_str.Append(", Amount ");
                obj_str.Append(", PayCondition ");
                obj_str.Append(", StartDate ");
                obj_str.Append(", EndDate ");
                obj_str.Append(", PayAtFirstPeriod ");
                obj_str.Append(", PayType ");
                obj_str.Append(", Reason ");               
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @AllowanceDeductID ");
                obj_str.Append(", @Amount ");
                obj_str.Append(", @PayCondition ");
                obj_str.Append(", @StartDate ");
                obj_str.Append(", @EndDate ");
                obj_str.Append(", @PayAtFirstPeriod ");
                obj_str.Append(", @PayType ");
                obj_str.Append(", @Reason ");       
                obj_str.Append(" )");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@AllowanceDeductID", SqlDbType.VarChar); obj_cmd.Parameters["@AllowanceDeductID"].Value = model.AllowanceDeductID;
                obj_cmd.Parameters.Add("@Amount", SqlDbType.Decimal); obj_cmd.Parameters["@Amount"].Value = model.Amount;
                obj_cmd.Parameters.Add("@PayCondition", SqlDbType.VarChar); obj_cmd.Parameters["@PayCondition"].Value = model.PayCondition;
                obj_cmd.Parameters.Add("@StartDate", SqlDbType.DateTime); obj_cmd.Parameters["@StartDate"].Value = model.StartDate;
                obj_cmd.Parameters.Add("@EndDate", SqlDbType.DateTime); obj_cmd.Parameters["@EndDate"].Value = model.EndDate;
                obj_cmd.Parameters.Add("@PayAtFirstPeriod", SqlDbType.Bit); obj_cmd.Parameters["@PayAtFirstPeriod"].Value = model.PayAtFirstPeriod;
                obj_cmd.Parameters.Add("@PayType", SqlDbType.VarChar); obj_cmd.Parameters["@PayType"].Value = model.PayType;
                obj_cmd.Parameters.Add("@Reason", SqlDbType.VarChar); obj_cmd.Parameters["@Reason"].Value = model.Reason;
                     
                obj_cmd.ExecuteNonQuery();
                                
                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Benefit.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpBenefit model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbTREmpBenefit SET ");

                obj_str.Append(" AllowanceDeductID=@AllowanceDeductID ");
                obj_str.Append(", Amount=@Amount ");
                obj_str.Append(", PayCondition=@PayCondition ");
                obj_str.Append(", PayAtFirstPeriod=@PayAtFirstPeriod ");
                obj_str.Append(", PayType=@PayType ");
                obj_str.Append(", Reason=@Reason ");                      

                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND StartDate=@StartDate ");
                obj_str.Append(" AND EndDate=@EndDate ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                
                obj_cmd.Parameters.Add("@AllowanceDeductID", SqlDbType.VarChar); obj_cmd.Parameters["@AllowanceDeductID"].Value = model.AllowanceDeductID;
                obj_cmd.Parameters.Add("@Amount", SqlDbType.Decimal); obj_cmd.Parameters["@Amount"].Value = model.Amount;
                obj_cmd.Parameters.Add("@PayCondition", SqlDbType.VarChar); obj_cmd.Parameters["@PayCondition"].Value = model.PayCondition;
                
                obj_cmd.Parameters.Add("@PayAtFirstPeriod", SqlDbType.Bit); obj_cmd.Parameters["@PayAtFirstPeriod"].Value = model.PayAtFirstPeriod;
                obj_cmd.Parameters.Add("@PayType", SqlDbType.VarChar); obj_cmd.Parameters["@PayType"].Value = model.PayType;
                obj_cmd.Parameters.Add("@Reason", SqlDbType.VarChar); obj_cmd.Parameters["@Reason"].Value = model.Reason;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@StartDate", SqlDbType.DateTime); obj_cmd.Parameters["@StartDate"].Value = model.StartDate;
                obj_cmd.Parameters.Add("@EndDate", SqlDbType.DateTime); obj_cmd.Parameters["@EndDate"].Value = model.EndDate;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Benefit.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
