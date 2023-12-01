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
    public class cls_ctTREmpPosition
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpPosition() { }

        public string getMessage() { return this.Message.Replace("tbTREmpPosition", "").Replace("cls_ctTREmpPosition", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpPosition> getData(string condition)
        {
            List<cls_TREmpPosition> list_model = new List<cls_TREmpPosition>();
            cls_TREmpPosition model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", PositionID");
                obj_str.Append(", PositionDate");
                obj_str.Append(", ISNULL(JDID, '') AS JDID");
                obj_str.Append(", ISNULL(Description, '') AS Description");
                obj_str.Append(", ISNULL(JobLevelID, '') AS JobLevelID");
                obj_str.Append(", ISNULL(UpdateDate, '01/01/2900') AS UpdateDate");
                obj_str.Append(", ISNULL(PositionName2, '') AS PositionName2");
                obj_str.Append(", ISNULL(JobDescDate, '01/01/2900') AS JobDescDate");
                obj_str.Append(", ISNULL(PositionID2, '') AS PositionID2");

                obj_str.Append(" FROM tbTREmpPosition");
                obj_str.Append(" WHERE 1=1");
                                
                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID, PositionDate DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpPosition();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.PositionID = dr["PositionID"].ToString();
                    model.PositionDate = Convert.ToDateTime(dr["PositionDate"]);
                    model.JDID = dr["JDID"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.JobLevelID = dr["JobLevelID"].ToString();
                    model.UpdateDate = Convert.ToDateTime(dr["UpdateDate"]);
                    model.PositionName2 = dr["PositionName2"].ToString();
                    model.JobDescDate = Convert.ToDateTime(dr["JobDescDate"]);
                    model.PositionID2 = dr["PositionID2"].ToString();
                                 
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Position.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpPosition> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";
            
            return this.getData(strCondition);
        }

        private List<cls_TREmpPosition> getData2(string condition)
        {
            List<cls_TREmpPosition> list_model = new List<cls_TREmpPosition>();
            cls_TREmpPosition model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                obj_str.Append("tbTREmpPosition.CompID, ");
                obj_str.Append("tbTREmpPosition.EmpID, ");
                obj_str.Append("tbTREmpPosition.PositionID, ");
                obj_str.Append("tbTREmpPosition.PositionDate, ");
                obj_str.Append("tbMTPosition.PositionNameE, ");
                obj_str.Append("tbMTPosition.PositionNameT, ");
                obj_str.Append("tbTREmpPosition.ReasonID, ");
                obj_str.Append("tbMTReason.ReasonNameE, ");
                obj_str.Append("tbMTReason.ReasonNameT ");
                obj_str.AppendLine("FROM tbTREmpPosition");
                obj_str.AppendLine("JOIN tbMTPosition ON tbMTPosition.CompID = tbTREmpPosition.CompID AND tbMTPosition.PositionID = tbTREmpPosition.PositionID");
                obj_str.AppendLine("JOIN tbMTReason ON tbMTReason.ReasonID = tbTREmpPosition.ReasonID AND tbMTReason.ReasonGroup = 'POS'");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpPosition();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.PositionID = dr["PositionID"].ToString();
                    model.PositionDate = Convert.ToDateTime(dr["PositionDate"]);
                    model.PositionNameE = dr["PositionNameE"].ToString();
                    model.PositionNameT = dr["PositionNameT"].ToString();
                    model.ReasonID = dr["ReasonID"].ToString();
                    model.ReasonNameE = dr["ReasonNameE"].ToString();
                    model.ReasonNameT = dr["ReasonNameT"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.getData2)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpPosition> getDataByFillter2(string com, string emp,string from, string to)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND tbTREmpPosition.CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND tbTREmpPosition.EmpID='" + emp + "'";

            if (!from.Equals("") && !to.Equals(""))
                strCondition += "AND tbTREmpPosition.PositionDate BETWEEN '" + Convert.ToDateTime(from).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(to).ToString("yyyy-MM-dd") + "'";

            return this.getData2(strCondition);
        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM tbTREmpPosition");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PositionDate='" + date.ToString("MM/dd/yyyy") + "'");
                                                
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

        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbTREmpPosition");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PositionDate='" + date.ToString("MM/dd/yyyy") + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Position.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpPosition model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID, model.PositionDate))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbTREmpPosition");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", PositionID ");
                obj_str.Append(", PositionDate ");
                obj_str.Append(", ReasonID ");
                obj_str.Append(", Description ");
                obj_str.Append(", JobLevelID ");
                obj_str.Append(", JDID ");
                obj_str.Append(", UpdateDate ");
                obj_str.Append(", PositionName2 ");
                obj_str.Append(", JobDescDate ");
                obj_str.Append(", PositionID2 ");
                obj_str.Append(" )");
                
                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @PositionID ");
                obj_str.Append(", @PositionDate ");
                obj_str.Append(", @ReasonID ");
                obj_str.Append(", @Description ");
                obj_str.Append(", @JobLevelID ");
                obj_str.Append(", @JDID ");
                obj_str.Append(", @UpdateDate ");
                obj_str.Append(", @PositionName2 ");
                obj_str.Append(", @JobDescDate ");
                obj_str.Append(", @PositionID2 ");      
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PositionID", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID"].Value = model.PositionID;
                obj_cmd.Parameters.Add("@PositionDate", SqlDbType.DateTime); obj_cmd.Parameters["@PositionDate"].Value = model.PositionDate;
                obj_cmd.Parameters.Add("@ReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonID"].Value = model.ReasonID;
                obj_cmd.Parameters.Add("@Description", SqlDbType.VarChar); obj_cmd.Parameters["@Description"].Value = model.Description;
                obj_cmd.Parameters.Add("@JobLevelID", SqlDbType.VarChar); obj_cmd.Parameters["@JobLevelID"].Value = model.JobLevelID;
                obj_cmd.Parameters.Add("@JDID", SqlDbType.VarChar); obj_cmd.Parameters["@JDID"].Value = model.JDID;
                obj_cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime); obj_cmd.Parameters["@UpdateDate"].Value = model.UpdateDate;
                obj_cmd.Parameters.Add("@PositionName2", SqlDbType.VarChar); obj_cmd.Parameters["@PositionName2"].Value = model.PositionName2;
                obj_cmd.Parameters.Add("@JobDescDate", SqlDbType.DateTime); obj_cmd.Parameters["@JobDescDate"].Value = model.JobDescDate;
                obj_cmd.Parameters.Add("@PositionID2", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID2"].Value = model.PositionID2;
                     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;

                //--
                this.doUpdateDefault(model.CompID, model.EmpID);
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpPosition model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbTREmpPosition SET ");

                obj_str.Append(" PositionID=@PositionID ");
                obj_str.Append(", ReasonID=@ReasonID ");
                obj_str.Append(", Description=@Description ");
                obj_str.Append(", JobLevelID=@JobLevelID ");
                obj_str.Append(", JDID=@JDID ");
                obj_str.Append(", UpdateDate=@UpdateDate ");
                obj_str.Append(", PositionName2=@PositionName2 ");
                obj_str.Append(", JobDescDate=@JobDescDate ");
                obj_str.Append(", PositionID2=@PositionID2 ");
                
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND PositionDate=@PositionDate ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                                
                obj_cmd.Parameters.Add("@PositionID", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID"].Value = model.PositionID;                
                obj_cmd.Parameters.Add("@ReasonID", SqlDbType.VarChar); obj_cmd.Parameters["@ReasonID"].Value = model.ReasonID;
                obj_cmd.Parameters.Add("@Description", SqlDbType.VarChar); obj_cmd.Parameters["@Description"].Value = model.Description;
                obj_cmd.Parameters.Add("@JobLevelID", SqlDbType.VarChar); obj_cmd.Parameters["@JobLevelID"].Value = model.JobLevelID;
                obj_cmd.Parameters.Add("@JDID", SqlDbType.VarChar); obj_cmd.Parameters["@JDID"].Value = model.JDID;
                obj_cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime); obj_cmd.Parameters["@UpdateDate"].Value = model.UpdateDate;
                obj_cmd.Parameters.Add("@PositionName2", SqlDbType.VarChar); obj_cmd.Parameters["@PositionName2"].Value = model.PositionName2;
                obj_cmd.Parameters.Add("@JobDescDate", SqlDbType.DateTime); obj_cmd.Parameters["@JobDescDate"].Value = model.JobDescDate;
                obj_cmd.Parameters.Add("@PositionID2", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID2"].Value = model.PositionID2;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PositionDate", SqlDbType.DateTime); obj_cmd.Parameters["@PositionDate"].Value = model.PositionDate;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;

                //--
                this.doUpdateDefault(model.CompID, model.EmpID);
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Position.update)" + ex.ToString();
            }

            return blnResult;
        }

        private void doUpdateDefault(string com, string emp)
        {
            try
            {
                //-- Step 1 Get Data Max date
                cls_TREmpPosition current = null;
                List<cls_TREmpPosition> list_model = this.getDataByFillter(com, emp);
                if (list_model.Count > 0)
                {
                    current = list_model[0];
                }

                if (current == null)
                    return;

                //-- Step 2 Update Current
                cls_ctConnection obj_conn = new cls_ctConnection();
                obj_conn.doConnect();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                
                obj_str.Append("UPDATE tbMTEmpMain SET ");
                obj_str.Append(" PositionID=@PositionID ");
                obj_str.Append(", PositionDate=@PositionDate ");               
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PositionID", SqlDbType.VarChar); obj_cmd.Parameters["@PositionID"].Value = current.PositionID;
                obj_cmd.Parameters.Add("@PositionDate", SqlDbType.DateTime); obj_cmd.Parameters["@PositionDate"].Value = current.PositionDate;
               
                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = com;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = emp;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

            }
            catch { }

        }
    }
}
