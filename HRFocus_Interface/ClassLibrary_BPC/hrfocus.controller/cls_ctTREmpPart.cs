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
    public class cls_ctTREmpPart
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpPart() { }

        public string getMessage() { return this.Message.Replace("tbTREmpPart", "").Replace("cls_ctTREmpPart", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpPart> getData(string condition)
        {
            List<cls_TREmpPart> list_model = new List<cls_TREmpPart>();
            cls_TREmpPart model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");
                obj_str.Append(", EmpID");
                obj_str.Append(", PartEntDate");
                obj_str.Append(", Level01");
                obj_str.Append(", Level02");
                obj_str.Append(", Level03");
                obj_str.Append(", Level04");
                obj_str.Append(", Level05");
                obj_str.Append(", Level06");
                obj_str.Append(", Level07");
                obj_str.Append(", Level08");
                obj_str.Append(", Level09");
                obj_str.Append(", Level10");
               
                obj_str.Append(", IsActive");
                obj_str.Append(", CommentP");

                obj_str.Append(" FROM tbTREmpPart");
                obj_str.Append(" WHERE 1=1");


                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID, PartEntDate DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpPart();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.PartEntDate = Convert.ToDateTime(dr["PartEntDate"]);

                    model.Level01 = dr["Level01"].ToString();
                    model.Level02 = dr["Level02"].ToString();
                    model.Level03 = dr["Level03"].ToString();
                    model.Level04 = dr["Level04"].ToString();
                    model.Level05 = dr["Level05"].ToString();
                    model.Level06 = dr["Level06"].ToString();
                    model.Level07 = dr["Level07"].ToString();
                    model.Level08 = dr["Level08"].ToString();
                    model.Level09 = dr["Level09"].ToString();
                    model.Level10 = dr["Level10"].ToString();
                    model.IsActive = dr["IsActive"].ToString();
                    model.CommentP = dr["CommentP"].ToString();
                    
                                 
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Part.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpPart> getDataByFillter(string com, string emp)
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
                obj_str.Append(" FROM tbTREmpPart");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PartEntDate='" + date.ToString("MM/dd/yyyy") + "'");
                                                
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

        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM tbTREmpPart");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND CompID='" + com + "'");
                obj_str.Append(" AND EmpID='" + emp + "'");
                obj_str.Append(" AND PartEntDate='" + date.ToString("MM/dd/yyyy") + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Part.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpPart model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.CompID, model.EmpID, model.PartEntDate.Date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO tbTREmpPart");
                obj_str.Append(" (");
                obj_str.Append("CompID ");
                obj_str.Append(", EmpID ");
                obj_str.Append(", PartEntDate ");
                obj_str.Append(", Level01 ");
                obj_str.Append(", Level02 ");
                obj_str.Append(", Level03 ");
                obj_str.Append(", Level04 ");
                obj_str.Append(", Level05 ");
                obj_str.Append(", Level06 ");
                obj_str.Append(", Level07 ");
                obj_str.Append(", Level08 ");
                obj_str.Append(", Level09 ");
                obj_str.Append(", Level10 ");
                obj_str.Append(", IsActive ");
                obj_str.Append(", CommentP ");               
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@CompID ");
                obj_str.Append(", @EmpID ");
                obj_str.Append(", @PartEntDate ");
                obj_str.Append(", @Level01 ");
                obj_str.Append(", @Level02 ");
                obj_str.Append(", @Level03 ");
                obj_str.Append(", @Level04 ");
                obj_str.Append(", @Level05 ");
                obj_str.Append(", @Level06 ");
                obj_str.Append(", @Level07 ");
                obj_str.Append(", @Level08 ");
                obj_str.Append(", @Level09 ");
                obj_str.Append(", @Level10 ");
                obj_str.Append(", @IsActive ");
                obj_str.Append(", @CommentP ");        
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PartEntDate", SqlDbType.DateTime); obj_cmd.Parameters["@PartEntDate"].Value = model.PartEntDate.Date;
                obj_cmd.Parameters.Add("@Level01", SqlDbType.VarChar); obj_cmd.Parameters["@Level01"].Value = model.Level01;
                obj_cmd.Parameters.Add("@Level02", SqlDbType.VarChar); obj_cmd.Parameters["@Level02"].Value = model.Level02;
                obj_cmd.Parameters.Add("@Level03", SqlDbType.VarChar); obj_cmd.Parameters["@Level03"].Value = model.Level03;
                obj_cmd.Parameters.Add("@Level04", SqlDbType.VarChar); obj_cmd.Parameters["@Level04"].Value = model.Level04;
                obj_cmd.Parameters.Add("@Level05", SqlDbType.VarChar); obj_cmd.Parameters["@Level05"].Value = model.Level05;
                obj_cmd.Parameters.Add("@Level06", SqlDbType.VarChar); obj_cmd.Parameters["@Level06"].Value = model.Level06;
                obj_cmd.Parameters.Add("@Level07", SqlDbType.VarChar); obj_cmd.Parameters["@Level07"].Value = model.Level07;
                obj_cmd.Parameters.Add("@Level08", SqlDbType.VarChar); obj_cmd.Parameters["@Level08"].Value = model.Level08;
                obj_cmd.Parameters.Add("@Level09", SqlDbType.VarChar); obj_cmd.Parameters["@Level09"].Value = model.Level09;
                obj_cmd.Parameters.Add("@Level10", SqlDbType.VarChar); obj_cmd.Parameters["@Level10"].Value = model.Level10;
                obj_cmd.Parameters.Add("@IsActive", SqlDbType.VarChar); obj_cmd.Parameters["@IsActive"].Value = model.IsActive;
                obj_cmd.Parameters.Add("@CommentP", SqlDbType.VarChar); obj_cmd.Parameters["@CommentP"].Value = model.CommentP;
                     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;

                //--
                this.doUpdateDefault(model.CompID, model.EmpID);
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Part.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpPart model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbTREmpPart SET ");

                obj_str.Append(" Level01=@Level01 ");
                obj_str.Append(", Level02=@Level02 ");
                obj_str.Append(", Level03=@Level03 ");
                obj_str.Append(", Level04=@Level04 ");
                obj_str.Append(", Level05=@Level05 ");
                obj_str.Append(", Level06=@Level06 ");
                obj_str.Append(", Level07=@Level07 ");
                obj_str.Append(", Level08=@Level08 ");
                obj_str.Append(", Level09=@Level09 ");
                obj_str.Append(", Level10=@Level10 ");
                obj_str.Append(", IsActive=@IsActive ");
                obj_str.Append(", CommentP=@CommentP ");
                
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND PartEntDate=@PartEntDate ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                
                obj_cmd.Parameters.Add("@Level01", SqlDbType.VarChar); obj_cmd.Parameters["@Level01"].Value = model.Level01;
                obj_cmd.Parameters.Add("@Level02", SqlDbType.VarChar); obj_cmd.Parameters["@Level02"].Value = model.Level02;
                obj_cmd.Parameters.Add("@Level03", SqlDbType.VarChar); obj_cmd.Parameters["@Level03"].Value = model.Level03;
                obj_cmd.Parameters.Add("@Level04", SqlDbType.VarChar); obj_cmd.Parameters["@Level04"].Value = model.Level04;
                obj_cmd.Parameters.Add("@Level05", SqlDbType.VarChar); obj_cmd.Parameters["@Level05"].Value = model.Level05;
                obj_cmd.Parameters.Add("@Level06", SqlDbType.VarChar); obj_cmd.Parameters["@Level06"].Value = model.Level06;
                obj_cmd.Parameters.Add("@Level07", SqlDbType.VarChar); obj_cmd.Parameters["@Level07"].Value = model.Level07;
                obj_cmd.Parameters.Add("@Level08", SqlDbType.VarChar); obj_cmd.Parameters["@Level08"].Value = model.Level08;
                obj_cmd.Parameters.Add("@Level09", SqlDbType.VarChar); obj_cmd.Parameters["@Level09"].Value = model.Level09;
                obj_cmd.Parameters.Add("@Level10", SqlDbType.VarChar); obj_cmd.Parameters["@Level10"].Value = model.Level10;
                obj_cmd.Parameters.Add("@IsActive", SqlDbType.VarChar); obj_cmd.Parameters["@IsActive"].Value = model.IsActive;
                obj_cmd.Parameters.Add("@CommentP", SqlDbType.VarChar); obj_cmd.Parameters["@CommentP"].Value = model.CommentP;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                obj_cmd.Parameters.Add("@PartEntDate", SqlDbType.DateTime); obj_cmd.Parameters["@PartEntDate"].Value = model.PartEntDate.Date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;

                //--
                this.doUpdateDefault(model.CompID, model.EmpID);
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Part.update)" + ex.ToString();
            }

            return blnResult;
        }

        private void doUpdateDefault(string com, string emp)
        {
            try
            {
                //-- Step 1 Get Data Max date
                cls_TREmpPart current = null;
                List<cls_TREmpPart> list_model = this.getDataByFillter(com, emp);
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
                obj_str.Append("UPDATE tbTREmpPart SET IsActive='0'");
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");                
                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = com;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = emp;
                obj_cmd.ExecuteNonQuery();

                //
                obj_str = new System.Text.StringBuilder();
                obj_str.Append("UPDATE tbTREmpPart SET IsActive='1'");
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                obj_str.Append(" AND PartEntDate=@PartEntDate ");
                obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = com;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = emp;
                obj_cmd.Parameters.Add("@PartEntDate", SqlDbType.DateTime); obj_cmd.Parameters["@PartEntDate"].Value = current.PartEntDate.Date;
                obj_cmd.ExecuteNonQuery();

                //
                obj_str = new System.Text.StringBuilder();
                obj_str.Append("UPDATE tbMTEmpMain SET ");

                obj_str.Append(" Level01=@Level01 ");
                obj_str.Append(", Level02=@Level02 ");
                obj_str.Append(", Level03=@Level03 ");
                obj_str.Append(", Level04=@Level04 ");
                obj_str.Append(", Level05=@Level05 ");
                obj_str.Append(", Level06=@Level06 ");
                obj_str.Append(", Level07=@Level07 ");
                obj_str.Append(", Level08=@Level08 ");
                obj_str.Append(", Level09=@Level09 ");
                obj_str.Append(", Level10=@Level10 ");

                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");
                
                obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@Level01", SqlDbType.VarChar); obj_cmd.Parameters["@Level01"].Value = current.Level01;
                obj_cmd.Parameters.Add("@Level02", SqlDbType.VarChar); obj_cmd.Parameters["@Level02"].Value = current.Level02;
                obj_cmd.Parameters.Add("@Level03", SqlDbType.VarChar); obj_cmd.Parameters["@Level03"].Value = current.Level03;
                obj_cmd.Parameters.Add("@Level04", SqlDbType.VarChar); obj_cmd.Parameters["@Level04"].Value = current.Level04;
                obj_cmd.Parameters.Add("@Level05", SqlDbType.VarChar); obj_cmd.Parameters["@Level05"].Value = current.Level05;
                obj_cmd.Parameters.Add("@Level06", SqlDbType.VarChar); obj_cmd.Parameters["@Level06"].Value = current.Level06;
                obj_cmd.Parameters.Add("@Level07", SqlDbType.VarChar); obj_cmd.Parameters["@Level07"].Value = current.Level07;
                obj_cmd.Parameters.Add("@Level08", SqlDbType.VarChar); obj_cmd.Parameters["@Level08"].Value = current.Level08;
                obj_cmd.Parameters.Add("@Level09", SqlDbType.VarChar); obj_cmd.Parameters["@Level09"].Value = current.Level09;
                obj_cmd.Parameters.Add("@Level10", SqlDbType.VarChar); obj_cmd.Parameters["@Level10"].Value = current.Level10;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = com;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = emp;
               
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

            }
            catch { }

        }
    }
}
