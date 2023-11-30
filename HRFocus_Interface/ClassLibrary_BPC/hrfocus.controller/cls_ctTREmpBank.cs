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
    public class cls_ctTREmpBank
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpBank() { }

        public string getMessage() { return this.Message.Replace("tbTREmpBank", "").Replace("cls_ctTREmpBank", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpBank> getData(string condition)
        {
            List<cls_TREmpBank> list_model = new List<cls_TREmpBank>();
            cls_TREmpBank model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", BankID");
                obj_str.Append(", BankAccNo");
                obj_str.Append(", BankAccName");
                obj_str.Append(", PayฺBankPercent");
                obj_str.Append(", PayCashPercent");
                obj_str.Append(", ReasonID");
                obj_str.Append(", ReasonDesc");
                obj_str.Append(", EmpType");

                obj_str.Append(" FROM tbTREmpBank");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);
                      

                obj_str.Append(" ORDER BY CompID, EmpID");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpBank();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.BankID = dr["BankID"].ToString();
                    model.BankAccNo = dr["BankAccNo"].ToString();
                    model.BankAccName = dr["BankAccName"].ToString();
                    model.PayBankPercent = Convert.ToDouble(dr["PayBankPercent"]);
                    model.PayCashPercent = Convert.ToDouble(dr["PayCashPercent"]);
                                                   
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Bank.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpBank> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string bank)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM tbTREmpBank");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND BankID='" + bank + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Bank.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp, string bank)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbTREmpBank");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND BankID='" + bank + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Bank.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpBank model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID, model.BankID))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbTREmpBank");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", BankID ");
                obj_str.Append(", BankAccNo ");
                obj_str.Append(", BankAccName ");
                obj_str.Append(", PayฺBankPercent ");
                obj_str.Append(", PayCashPercent ");            
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @BankID ");
                obj_str.Append(", @BankAccNo ");
                obj_str.Append(", @BankAccName ");
                obj_str.Append(", @PayBankPercent ");
                obj_str.Append(", @PayCashPercent ");               
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@BankID", SqlDbType.VarChar); obj_cmd.Parameters["@BankID"].Value = model.BankID;
                obj_cmd.Parameters.Add("@BankAccNo", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccNo"].Value = model.BankAccNo;
                obj_cmd.Parameters.Add("@BankAccName", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccName"].Value = model.BankAccName;
                obj_cmd.Parameters.Add("@PayBankPercent", SqlDbType.Decimal); obj_cmd.Parameters["@PayBankPercent"].Value = model.PayBankPercent;
                obj_cmd.Parameters.Add("@PayCashPercent", SqlDbType.Decimal); obj_cmd.Parameters["@PayCashPercent"].Value = model.PayCashPercent;               
                     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Bank.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpBank model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbTREmpBank SET ");

                obj_str.Append(" BankAccNo=@BankAccNo ");
                obj_str.Append(", BankAccName=@BankAccName ");
                obj_str.Append(", PayฺBankPercent=@PayBankPercent ");
                obj_str.Append(", PayCashPercent=@PayCashPercent ");               

                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND BankID=@BankID ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                
                obj_cmd.Parameters.Add("@BankAccNo", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccNo"].Value = model.BankAccNo;
                obj_cmd.Parameters.Add("@BankAccName", SqlDbType.VarChar); obj_cmd.Parameters["@BankAccName"].Value = model.BankAccName;
                obj_cmd.Parameters.Add("@PayBankPercent", SqlDbType.Decimal); obj_cmd.Parameters["@PayBankPercent"].Value = model.PayBankPercent;
                obj_cmd.Parameters.Add("@PayCashPercent", SqlDbType.Decimal); obj_cmd.Parameters["@PayCashPercent"].Value = model.PayCashPercent;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@BankID", SqlDbType.VarChar); obj_cmd.Parameters["@BankID"].Value = model.BankID;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Bank.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
