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
    public class cls_ctTREmpSalary
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpSalary() { }

        public string getMessage() { return this.Message.Replace("tbTREmpSalary", "").Replace("cls_ctTREmpSalary", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpSalary> getData(string condition)
        {
            List<cls_TREmpSalary> list_model = new List<cls_TREmpSalary>();
            cls_TREmpSalary model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", PeriodStart");
                obj_str.Append(", Salary");
                obj_str.Append(", IncAmount");
                obj_str.Append(", IncPercent");
                obj_str.Append(", PreviousSalary");
                obj_str.Append(", ReasonID");
                obj_str.Append(", ReasonDesc");
                obj_str.Append(", EmpType");

                obj_str.Append(" FROM tbTREmpSalary");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID, PeriodStart DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpSalary();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.PeriodStart = Convert.ToDateTime(dr["PeriodStart"]);
                    model.Salary = Convert.ToDouble(dr["Salary"]);
                    model.IncAmount = Convert.ToDouble(dr["IncAmount"]);
                    model.IncPercent = Convert.ToDouble(dr["IncPercent"]);
                    model.PreviousSalary = Convert.ToDouble(dr["PreviousSalary"]);
                    model.ReasonID = dr["ReasonID"].ToString();
                    model.ReasonDesc = dr["ReasonDesc"].ToString();
                    model.EmpType = dr["EmpType"].ToString();
                                 
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Sal.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpSalary> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM tbTREmpSalary");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PeriodStart='" + date.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Sal.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbTREmpSalary");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PeriodStart='" + date.ToString("MM/dd/yyyy") + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Sal.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpSalary model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID, model.PeriodStart.Date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbTREmpSalary");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", PeriodStart ");
                obj_str.Append(", Salary ");
                obj_str.Append(", IncAmount ");
                obj_str.Append(", IncPercent ");
                obj_str.Append(", PreviousSalary ");
                obj_str.Append(", ReasonID ");
                obj_str.Append(", ReasonDesc ");
                obj_str.Append(", EmpType ");               
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @PeriodStart ");
                obj_str.Append(", @Salary ");
                obj_str.Append(", @IncAmount ");
                obj_str.Append(", @IncPercent ");
                obj_str.Append(", @PreviousSalary ");
                obj_str.Append(", @ReasonID ");
                obj_str.Append(", @ReasonDesc ");
                obj_str.Append(", @EmpType ");       
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PeriodStart", SqlDbType.DateTime); obj_cmd.Parameters["@PeriodStart"].Value = model.PeriodStart.Date;
                obj_cmd.Parameters.Add("@Salary", SqlDbType.Decimal); obj_cmd.Parameters["@Salary"].Value = model.Salary;
                obj_cmd.Parameters.Add("@IncAmount", SqlDbType.Decimal); obj_cmd.Parameters["@IncAmount"].Value = model.IncAmount;
                obj_cmd.Parameters.Add("@IncPercent", SqlDbType.Decimal); obj_cmd.Parameters["@IncPercent"].Value = model.IncPercent;
                obj_cmd.Parameters.Add("@PreviousSalary", SqlDbType.Decimal); obj_cmd.Parameters["@PreviousSalary"].Value = model.PreviousSalary;
                obj_cmd.Parameters.Add("@ReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonID"].Value = model.ReasonID;
                obj_cmd.Parameters.Add("@ReasonDesc", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonDesc"].Value = model.ReasonDesc;
                obj_cmd.Parameters.Add("@EmpType", SqlDbType.VarChar); obj_cmd.Parameters["@EmpType"].Value = model.EmpType;
                     
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

        public bool update(cls_TREmpSalary model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbTREmpSalary SET ");

                obj_str.Append(" Salary=@Salary ");
                obj_str.Append(", IncAmount=@IncAmount ");
                obj_str.Append(", IncPercent=@IncPercent ");
                obj_str.Append(", PreviousSalary=@PreviousSalary ");
                obj_str.Append(", ReasonID=@ReasonID ");
                obj_str.Append(", ReasonDesc=@ReasonDesc ");
                obj_str.Append(", EmpType=@EmpType ");         

                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND PeriodStart=@PeriodStart ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@Salary", SqlDbType.Decimal); obj_cmd.Parameters["@Salary"].Value = model.Salary;
                obj_cmd.Parameters.Add("@IncAmount", SqlDbType.Decimal); obj_cmd.Parameters["@IncAmount"].Value = model.IncAmount;
                obj_cmd.Parameters.Add("@IncPercent", SqlDbType.Decimal); obj_cmd.Parameters["@IncPercent"].Value = model.IncPercent;
                obj_cmd.Parameters.Add("@PreviousSalary", SqlDbType.Decimal); obj_cmd.Parameters["@PreviousSalary"].Value = model.PreviousSalary;
                obj_cmd.Parameters.Add("@ReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonID"].Value = model.ReasonID;
                obj_cmd.Parameters.Add("@ReasonDesc", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonDesc"].Value = model.ReasonDesc;
                obj_cmd.Parameters.Add("@EmpType", SqlDbType.VarChar); obj_cmd.Parameters["@EmpType"].Value = model.EmpType;
                
                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PeriodStart", SqlDbType.DateTime); obj_cmd.Parameters["@PeriodStart"].Value = model.PeriodStart.Date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Sal.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
