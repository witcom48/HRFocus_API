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
    public class cls_ctMTEmpDetail
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTEmpDetail() { }

        public string getMessage() { return this.Message.Replace("tbMTEmpMain", "").Replace("cls_ctMTEmpDetail", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTEmpDetail> getData(string condition)
        {
            List<cls_MTEmpDetail> list_model = new List<cls_MTEmpDetail>();
            cls_MTEmpDetail model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                
                obj_str.Append("CompID");   
                obj_str.Append(", EmpID");
                obj_str.Append(", ISNULL(BirthDay, '01/01/2900') AS BirthDay");
                obj_str.Append(", ISNULL(Sex, '') AS Sex");
                obj_str.Append(", ISNULL(Height, '') AS Height");
                obj_str.Append(", ISNULL(Weight, '') AS Weight");
                obj_str.Append(", ISNULL(MaritalStatus, '') AS MaritalStatus");
                obj_str.Append(", ISNULL(MilitaryStatus, '') AS MilitaryStatus");
                obj_str.Append(", ISNULL(LanguageID, '') AS LanguageID");
                obj_str.Append(", ISNULL(BloodID, '') AS BloodID");
                obj_str.Append(", ISNULL(NationID, '') AS NationID");
                obj_str.Append(", ISNULL(OriginID, '') AS OriginID");
                obj_str.Append(", ISNULL(CardNo, '') AS CardNo");
                obj_str.Append(", ISNULL(CardNoIssueDate, '01/01/2900') AS CardNoIssueDate");
                obj_str.Append(", ISNULL(CardNoExpireDate, '01/01/2900') AS CardNoExpireDate");
                obj_str.Append(", ISNULL(SocialNo, '') AS SocialNo");
                obj_str.Append(", ISNULL(SocialIssueDate, '') AS SocialIssueDate");
                obj_str.Append(", ISNULL(SociaExpireDate, '') AS SocialExpireDate");
                obj_str.Append(", ISNULL(SSOStatusSent, '') AS SSOStatusSent");
                obj_str.Append(", ISNULL(PassporIssueDate, '') AS PassportIssueDate");
                obj_str.Append(", ISNULL(PassporExpireDate, '') AS PassportExpireDate");
                obj_str.Append(", ISNULL(TaxNo, '') AS TaxNo");
                obj_str.Append(", ISNULL(TaxIssueDate, '01/01/2900') AS TaxIssueDate");
                obj_str.Append(", ISNULL(TaxExpireDate, '01/01/2900') AS TaxExpireDate");
                obj_str.Append(", ISNULL(PFNo, '') AS PFNo");
                obj_str.Append(", ISNULL(PFType, '') AS PFType");
                obj_str.Append(", ISNULL(PFEnterDate, '01/01/2900') AS PFEnterDate");
                obj_str.Append(", ISNULL(PFStartDate, '01/01/2900') AS PFStartDate");
                obj_str.Append(", ISNULL(PFPerComp, '0') AS PFPerComp");
                obj_str.Append(", ISNULL(PFPerEmp, '0') AS PFPerEmp");               
 
                obj_str.Append(" FROM tbMTEmpMain");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY CompID, EmpID");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTEmpDetail();

                    model.CompID = dr["CompID"].ToString();
                    model.EmpID = dr["EmpID"].ToString();
                    model.BirthDay = Convert.ToDateTime(dr["BirthDay"]);
                    model.Sex = dr["Sex"].ToString();
                    model.Height = Convert.ToDouble(dr["Height"]);
                    model.Weight = Convert.ToDouble(dr["Weight"]);
                    model.MaritalStatus = dr["MaritalStatus"].ToString();
                    model.MilitaryStatus = dr["MilitaryStatus"].ToString();
                    model.LanguageID = dr["LanguageID"].ToString();
                    model.BloodID = dr["BloodID"].ToString();
                    model.NationID = dr["NationID"].ToString();
                    model.OriginID = dr["OriginID"].ToString();
                    model.ReligionID = dr["ReligionID"].ToString();
                    model.CardNo = dr["CardNo"].ToString();
                    model.CardNoIssueDate = Convert.ToDateTime(dr["CardNoIssueDate"]);
                    model.CardNoExpireDate = Convert.ToDateTime(dr["CardNoExpireDate"]);
                    model.SocialNo = dr["SocialNo"].ToString();
                    model.SocialIssueDate = Convert.ToDateTime(dr["SocialIssueDate"]);
                    model.SocialExpireDate = Convert.ToDateTime(dr["SocialExpireDate"]);
                    model.SSOStatusSent = dr["SSOStatusSent"].ToString();
                    model.PassportNo = dr["PassportNo"].ToString();
                    model.PassportIssueDate = Convert.ToDateTime(dr["PassportIssueDate"]);
                    model.PassportExpireDate = Convert.ToDateTime(dr["PassportExpireDate"]);
                    model.TaxNo = dr["TaxNo"].ToString();
                    model.TaxIssueDate = Convert.ToDateTime(dr["TaxIssueDate"]);
                    model.TaxExpireDate = Convert.ToDateTime(dr["TaxExpireDate"]);
                    model.PFNo = dr["PFNo"].ToString();
                    model.PFType = dr["PFType"].ToString();
                    model.PFEnterDate = Convert.ToDateTime(dr["PFEnterDate"]);
                    model.PFStartDate = Convert.ToDateTime(dr["PFStartDate"]);
                    model.PFPerComp = Convert.ToDouble(dr["PFPerComp"]);
                    model.PFPerEmp = Convert.ToDouble(dr["PFPerEmp"]);                                                         
             
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Emp detail.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTEmpDetail> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND CompID='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND EmpID='" + emp + "'";
            
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
                Message = "ERROR::(Emp detail.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }        
       
        public bool update(cls_MTEmpDetail model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE tbMTEmpMain SET ");

                obj_str.Append(" BirthDay=@BirthDay ");
                obj_str.Append(", Sex=@Sex ");
                obj_str.Append(", Height=@Height ");
                obj_str.Append(", Weight=@Weight ");
                obj_str.Append(", MaritalStatus=@MaritalStatus ");
                obj_str.Append(", MilitaryStatus=@MilitaryStatus ");
                obj_str.Append(", LanguageID=@LanguageID ");
                obj_str.Append(", BloodID=@BloodID ");
                obj_str.Append(", NationID=@NationID ");
                obj_str.Append(", OriginID=@OriginID ");
                obj_str.Append(", ReligionID=@ReligionID ");
                obj_str.Append(", CardNo=@CardNo ");
                obj_str.Append(", CardNoIssueDate=@CardNoIssueDate ");
                obj_str.Append(", CardNoExpireDate=@CardNoExpireDate ");
                obj_str.Append(", SocialNo=@SocialNo ");
                obj_str.Append(", SocialIssueDate=@SocialIssueDate ");
                obj_str.Append(", SociaExpireDate=@SocialExpireDate ");
                obj_str.Append(", SSOStatusSent=@SSOStatusSent ");
                obj_str.Append(", PassportNo=@PassportNo ");
                obj_str.Append(", PassporIssueDate=@PassportIssueDate ");
                obj_str.Append(", PassporExpireDate=@PassportExpireDate ");
                obj_str.Append(", TaxNo=@TaxNo ");
                obj_str.Append(", TaxIssueDate=@TaxIssueDate ");
                obj_str.Append(", TaxExpireDate=@TaxExpireDate ");
                obj_str.Append(", PFNo=@PFNo ");
                obj_str.Append(", PFType=@PFType ");
                obj_str.Append(", PFEnterDate=@PFEnterDate ");
                obj_str.Append(", PFStartDate=@PFStartDate ");
                obj_str.Append(", PFPerComp=@PFPerComp ");
                obj_str.Append(", PFPerEmp=@PFPerEmp ");
               
                obj_str.Append(" WHERE CompID=@CompID ");
                obj_str.Append(" AND EmpID=@EmpID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@BirthDay", SqlDbType.DateTime); obj_cmd.Parameters["@BirthDay"].Value = model.BirthDay;
                obj_cmd.Parameters.Add("@Sex", SqlDbType.VarChar); obj_cmd.Parameters["@Sex"].Value = model.Sex;
                obj_cmd.Parameters.Add("@Height", SqlDbType.Decimal); obj_cmd.Parameters["@Height"].Value = model.Height;
                obj_cmd.Parameters.Add("@Weight", SqlDbType.Decimal); obj_cmd.Parameters["@Weight"].Value = model.Weight;
                obj_cmd.Parameters.Add("@MaritalStatus", SqlDbType.VarChar); obj_cmd.Parameters["@MaritalStatus"].Value = model.MaritalStatus;
                obj_cmd.Parameters.Add("@MilitaryStatus", SqlDbType.VarChar); obj_cmd.Parameters["@MilitaryStatus"].Value = model.MilitaryStatus;
                obj_cmd.Parameters.Add("@LanguageID", SqlDbType.VarChar); obj_cmd.Parameters["@LanguageID"].Value = model.LanguageID;
                obj_cmd.Parameters.Add("@BloodID", SqlDbType.VarChar); obj_cmd.Parameters["@BloodID"].Value = model.BloodID;
                obj_cmd.Parameters.Add("@NationID", SqlDbType.VarChar); obj_cmd.Parameters["@NationID"].Value = model.NationID;
                obj_cmd.Parameters.Add("@OriginID", SqlDbType.VarChar); obj_cmd.Parameters["@OriginID"].Value = model.OriginID;
                obj_cmd.Parameters.Add("@ReligionID", SqlDbType.VarChar); obj_cmd.Parameters["@ReligionID"].Value = model.ReligionID;
                obj_cmd.Parameters.Add("@CardNo", SqlDbType.VarChar); obj_cmd.Parameters["@CardNo"].Value = model.CardNo;
                obj_cmd.Parameters.Add("@CardNoIssueDate", SqlDbType.DateTime ); obj_cmd.Parameters["@CardNoIssueDate"].Value = model.CardNoIssueDate;
                obj_cmd.Parameters.Add("@CardNoExpireDate", SqlDbType.DateTime); obj_cmd.Parameters["@CardNoExpireDate"].Value = model.CardNoExpireDate;
                obj_cmd.Parameters.Add("@SocialNo", SqlDbType.VarChar); obj_cmd.Parameters["@SocialNo"].Value = model.SocialNo;
                obj_cmd.Parameters.Add("@SocialIssueDate", SqlDbType.DateTime); obj_cmd.Parameters["@SocialIssueDate"].Value = model.SocialIssueDate;
                obj_cmd.Parameters.Add("@SocialExpireDate", SqlDbType.DateTime); obj_cmd.Parameters["@SocialExpireDate"].Value = model.SocialExpireDate;
                obj_cmd.Parameters.Add("@SSOStatusSent", SqlDbType.VarChar); obj_cmd.Parameters["@SSOStatusSent"].Value = model.SSOStatusSent;
                obj_cmd.Parameters.Add("@PassportNo", SqlDbType.VarChar); obj_cmd.Parameters["@PassportNo"].Value = model.PassportNo;
                obj_cmd.Parameters.Add("@PassportIssueDate", SqlDbType.DateTime); obj_cmd.Parameters["@PassportIssueDate"].Value = model.PassportIssueDate;
                obj_cmd.Parameters.Add("@PassportExpireDate", SqlDbType.DateTime); obj_cmd.Parameters["@PassportExpireDate"].Value = model.PassportExpireDate;
                obj_cmd.Parameters.Add("@TaxNo", SqlDbType.VarChar); obj_cmd.Parameters["@TaxNo"].Value = model.TaxNo;
                obj_cmd.Parameters.Add("@TaxIssueDate", SqlDbType.DateTime); obj_cmd.Parameters["@TaxIssueDate"].Value = model.TaxIssueDate;
                obj_cmd.Parameters.Add("@TaxExpireDate", SqlDbType.DateTime); obj_cmd.Parameters["@TaxExpireDate"].Value = model.TaxExpireDate;
                obj_cmd.Parameters.Add("@PFNo", SqlDbType.VarChar); obj_cmd.Parameters["@PFNo"].Value = model.PFNo;
                obj_cmd.Parameters.Add("@PFType", SqlDbType.VarChar); obj_cmd.Parameters["@PFType"].Value = model.PFType;
                obj_cmd.Parameters.Add("@PFEnterDate", SqlDbType.DateTime); obj_cmd.Parameters["@PFEnterDate"].Value = model.PFEnterDate;
                obj_cmd.Parameters.Add("@PFStartDate", SqlDbType.DateTime); obj_cmd.Parameters["@PFStartDate"].Value = model.PFStartDate;
                obj_cmd.Parameters.Add("@PFPerComp", SqlDbType.Decimal); obj_cmd.Parameters["@PFPerComp"].Value = model.PFPerComp;
                obj_cmd.Parameters.Add("@PFPerEmp", SqlDbType.Decimal); obj_cmd.Parameters["@PFPerEmp"].Value = model.PFPerEmp;

                obj_cmd.Parameters.Add("@CompID", SqlDbType.VarChar); obj_cmd.Parameters["@CompID"].Value = model.CompID;
                obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar); obj_cmd.Parameters["@EmpID"].Value = model.EmpID;
                
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emp detail.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
