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
    public class cls_ctSYSLogApi
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSLogApi() { }

        public string getMessage() { return this.Message.Replace("tbSYSLogAPI", "").Replace("cls_ctSYSLogApi", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSLogApi> getData(string condition)
        {
            List<cls_SYSLogApi> list_model = new List<cls_SYSLogApi>();
            cls_SYSLogApi model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                                
                obj_str.Append("CompID");
                obj_str.Append(", APIID");
                obj_str.Append(", APIType");
                obj_str.Append(", APIStatus");
                obj_str.Append(", APIDetail");
                obj_str.Append(", CreateBy");
                obj_str.Append(", CreateDate");               

                obj_str.Append(" FROM tbSYSLogAPI");
                obj_str.Append(" WHERE 1=1");
                
                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, CreateDate DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSLogApi();

                    model.CompID = dr["CompID"].ToString();
                    model.APIID = dr["APIID"].ToString();
                    model.APIType = dr["APIType"].ToString();
                    model.APIStatus = dr["APIStatus"].ToString();
                    model.APIDetail = dr["APIDetail"].ToString();
                    model.CreateBy = dr["CreateBy"].ToString();
                    model.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                                                                                            
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(LogApi.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSLogApi> getDataByFillter(string com, string type, DateTime from, DateTime to)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!type.Equals(""))
                strCondition += " AND APIType='" + type + "'";

            strCondition += " AND (CreateDate BETWEEN '" + from.ToString("MM/dd/yyyy") + "' AND '" + to.ToString("MM/dd/yyyy") + "')";
            
            return this.getData(strCondition);
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ISNULL(APIID, 1) ");
                obj_str.Append(" FROM tbSYSLogAPI");
                obj_str.Append(" order by CAST(APIID AS INT) desc ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(LogApi.getNextID)" + ex.ToString();
            }

            return intResult;
        }
                
       
        public bool insert(cls_SYSLogApi model)
        {
            bool blnResult = false;
            try
            {
               
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

          
                obj_str.Append("INSERT INTO tbSYSLogAPI");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", APIID ");
                obj_str.Append(", APIType ");
                obj_str.Append(", APIStatus ");
                obj_str.Append(", APIDetail ");
                obj_str.Append(", CreateBy ");
                obj_str.Append(", CreateDate ");
                          
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @APIID ");
                obj_str.Append(", @APIType ");
                obj_str.Append(", @APIStatus ");
                obj_str.Append(", @APIDetail ");
                obj_str.Append(", @CreateBy ");
                obj_str.Append(", @CreateDate ");
                obj_str.Append(" )");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@APIID", SqlDbType.VarChar); obj_cmd.Parameters["@APIID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@APIType", SqlDbType.VarChar); obj_cmd.Parameters["@APIType"].Value = model.APIType;
                obj_cmd.Parameters.Add("@APIStatus", SqlDbType.VarChar); obj_cmd.Parameters["@APIStatus"].Value = model.APIStatus;
                obj_cmd.Parameters.Add("@APIDetail", SqlDbType.VarChar); obj_cmd.Parameters["@APIDetail"].Value = model.APIDetail;
                obj_cmd.Parameters.Add("@CreateBy", SqlDbType.VarChar); obj_cmd.Parameters["@CreateBy"].Value = model.CreateBy;
                obj_cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime); obj_cmd.Parameters["@CreateDate"].Value = DateTime.Now;
                                     
                obj_cmd.ExecuteNonQuery();
                                
                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(LogApi.insert)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
