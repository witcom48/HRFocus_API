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
    public class cls_ctMTPart
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTPart() { }

        public string getMessage() { return this.Message.Replace("tbMTPart", "").Replace("cls_ctMTPart", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTPart> getData(string condition)
        {
            List<cls_MTPart> list_model = new List<cls_MTPart>();
            cls_MTPart model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", LevelID");
                obj_str.Append(", PartID");
                obj_str.Append(", PartNameT");
                obj_str.Append(", PartNameE");
                obj_str.Append(", PartRef");
                obj_str.Append(", LevelRef"); 

                obj_str.Append(" FROM tbMTPart");
                obj_str.Append(" WHERE 1=1");
                                
                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, LevelID, PartID");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTPart();

                    model.CompID = dr["CompID"].ToString();
                    model.LevelID = dr["LevelID"].ToString();
                    model.PartID = dr["PartID"].ToString();
                    model.PartNameT = dr["PartNameT"].ToString();
                    model.PartNameE = dr["PartNameE"].ToString();
                    model.PartRef = dr["PartRef"].ToString();
                    model.LevelRef = dr["LevelRef"].ToString();
                    
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Part.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTPart> getDataByFillter(string com, string level)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!level.Equals(""))
                strCondition += " AND LevelID='" + level + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string level, string part)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PartID");
                obj_str.Append(" FROM tbMTPart");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND PartID='" + part + "'");
                obj_str.Append(" AND LevelID='" + level + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Part.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string level, string part)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbMTPart");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND LevelID='" + level + "'");
                obj_str.Append(" AND PartID='" + part + "'");
                                                             
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Part.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTPart model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.LevelID, model.PartID))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbMTPart");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", LevelID ");
                obj_str.Append(", PartID ");
                obj_str.Append(", PartNameT ");
                obj_str.Append(", PartNameE ");
                obj_str.Append(", PartRef ");
                obj_str.Append(", LevelRef ");              
                obj_str.Append(" )");
                
                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @LevelID ");
                obj_str.Append(", @PartID ");
                obj_str.Append(", @PartNameT ");
                obj_str.Append(", @PartNameE ");
                obj_str.Append(", @PartRef ");
                obj_str.Append(", @LevelRef ");            
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@LevelID", SqlDbType.VarChar); obj_cmd.Parameters["@LevelID"].Value = model.LevelID;
                obj_cmd.Parameters.Add("@PartID", SqlDbType.VarChar); obj_cmd.Parameters["@PartID"].Value = model.PartID;
                obj_cmd.Parameters.Add("@PartNameT", SqlDbType.VarChar); obj_cmd.Parameters["@PartNameT"].Value = model.PartNameT;
                obj_cmd.Parameters.Add("@PartNameE", SqlDbType.VarChar); obj_cmd.Parameters["@PartNameE"].Value = model.PartNameE;
                obj_cmd.Parameters.Add("@PartRef", SqlDbType.VarChar); obj_cmd.Parameters["@PartRef"].Value = model.PartRef;
                obj_cmd.Parameters.Add("@LevelRef", SqlDbType.VarChar); obj_cmd.Parameters["@LevelRef"].Value = model.LevelRef;
                     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Part.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTPart model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbMTPart SET ");

                obj_str.Append(" PartNameT=@PartNameT ");
                obj_str.Append(", PartNameE=@PartNameE ");
                obj_str.Append(", PartRef=@PartRef ");
                obj_str.Append(", LevelRef=@LevelRef ");
              
                               
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND LevelID=@LevelID ");
                obj_str.Append(" AND PartID=@PartID ");
                                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                
                obj_cmd.Parameters.Add("@PartNameT", SqlDbType.VarChar); obj_cmd.Parameters["@PartNameT"].Value = model.PartNameT;
                obj_cmd.Parameters.Add("@PartNameE", SqlDbType.VarChar); obj_cmd.Parameters["@PartNameE"].Value = model.PartNameE;
                obj_cmd.Parameters.Add("@PartRef", SqlDbType.VarChar); obj_cmd.Parameters["@PartRef"].Value = model.PartRef;
                obj_cmd.Parameters.Add("@LevelRef", SqlDbType.VarChar); obj_cmd.Parameters["@LevelRef"].Value = model.LevelRef;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@LevelID", SqlDbType.VarChar); obj_cmd.Parameters["@LevelID"].Value = model.LevelID;
                obj_cmd.Parameters.Add("@PartID", SqlDbType.VarChar); obj_cmd.Parameters["@PartID"].Value = model.PartID;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Part.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
