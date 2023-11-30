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
    public class cls_ctMTPosition
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTPosition() { }

        public string getMessage() { return this.Message.Replace("tbMTPosition", "").Replace("cls_ctMTPosition", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTPosition> getData(string condition)
        {
            List<cls_MTPosition> list_model = new List<cls_MTPosition>();
            cls_MTPosition model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");             
                obj_str.Append(", PositionID");
                obj_str.Append(", PositionNameT");
                obj_str.Append(", PositionNameE");               

                obj_str.Append(" FROM tbMTPosition");
                obj_str.Append(" WHERE 1=1");
                                
                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, PositionID");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTPosition();

                    model.CompID = dr["CompID"].ToString();
                    model.PositionID = dr["PositionID"].ToString();
                    model.PositionNameT = dr["PositionNameT"].ToString();
                    model.PositionNameE = dr["PositionNameE"].ToString();
                                 
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Position.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTPosition> getDataByFillter(string com, string pos)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!pos.Equals(""))
                strCondition += " AND PositionID='" + pos + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string pos)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PositionID");
                obj_str.Append(" FROM tbMTPosition");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND PositionID='" + pos + "'");
                
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string pos)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbMTPosition");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND PositionID='" + pos + "'");
                                                             
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Position.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTPosition model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.PositionID))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbMTPosition");
                obj_str.Append(" (");
                obj_str.Append("CompID ");                
                obj_str.Append(", PositionID ");
                obj_str.Append(", PositionNameT ");
                obj_str.Append(", PositionNameE ");                
                obj_str.Append(" )");
                
                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @PositionID ");
                obj_str.Append(", @PositionNameT ");
                obj_str.Append(", @PositionNameE ");     
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;             
                obj_cmd.Parameters.Add("@PositionID", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID"].Value = model.PositionID;
                obj_cmd.Parameters.Add("@PositionNameT", SqlDbType.VarChar); obj_cmd.Parameters["@PositionNameT"].Value = model.PositionNameT;
                obj_cmd.Parameters.Add("@PositionNameE", SqlDbType.VarChar); obj_cmd.Parameters["@PositionNameE"].Value = model.PositionNameE;
                     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTPosition model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbMTPosition SET ");

                obj_str.Append(" PositionNameT=@PositionNameT ");
                obj_str.Append(", PositionNameE=@PositionNameE ");
                               
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND PositionID=@PositionID ");
                                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                                
                obj_cmd.Parameters.Add("@PositionNameT", SqlDbType.VarChar); obj_cmd.Parameters["@PositionNameT"].Value = model.PositionNameT;
                obj_cmd.Parameters.Add("@PositionNameE", SqlDbType.VarChar); obj_cmd.Parameters["@PositionNameE"].Value = model.PositionNameE;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@PositionID", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID"].Value = model.PositionID;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
