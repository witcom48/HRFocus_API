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
    public class cls_ctMTPOLPeriodic
    {
         string Message = string.Empty;
        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTPOLPeriodic() { }
        public string getMessage() { return this.Message.Replace("cls_ctMTPOLPeriodic", "").Replace("cls_ctMTPOLPeriodic", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        public void AddPeriodicRecord(cls_MTPOLPeriodic record)
        {
            Obj_conn.doConnect();
            using (SqlConnection connection = Obj_conn.getConnection())
            {

                using (SqlCommand command = new SqlCommand("INSERT INTO tbPOLPeriodic VALUES (@CompID, @PeriodID, @PeriodYear, @EmpType, @PeriodNameT, @PeriodNameE, @PaymentDate, @FromDate, @ToDate, @ClosePR, @CloseTA, @CloseWE, @CloseTR, @SalaryQtyday, @SalaryResday, @SalaryProday, @CalculateType)", connection))
                {
                    SetParameters(command, record);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<cls_MTPOLPeriodic> GetPeriodicRecord(string compId, string periodId, string empType, string periodYear)
        {
            List<cls_MTPOLPeriodic> allItems = new List<cls_MTPOLPeriodic>();
             Obj_conn.doConnect();
            using (SqlConnection connection = Obj_conn.getConnection())
            {

                using (SqlCommand command = new SqlCommand(
    @"SELECT * FROM tbPOLPeriodic 
    WHERE (COALESCE(@CompID, '') = '' OR CompID = @CompID) AND
    (COALESCE(@PeriodID, '') = '' OR PeriodID = @PeriodID) AND
    (COALESCE(@EmpType, '') = '' OR EmpType = @EmpType) AND
    (COALESCE(@PeriodYear, '') = '' OR PeriodYear = @PeriodYear)", connection))
                {
                    command.Parameters.AddWithValue("@CompID", compId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PeriodID", periodId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@EmpType", empType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PeriodYear", periodYear ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cls_MTPOLPeriodic item = ReadPeriodicRecord(reader);
                            allItems.Add(item);
                        }
                    }
                }

                return allItems;
            }
        }

        public void UpdatePeriodicRecord(cls_MTPOLPeriodic record)
        {
             Obj_conn.doConnect();
            using (SqlConnection connection = Obj_conn.getConnection())
            {

                using (SqlCommand command = new SqlCommand("UPDATE tbPOLPeriodic SET PeriodNameT = @PeriodNameT, PeriodNameE = @PeriodNameE, PaymentDate = @PaymentDate, FromDate = @FromDate, ToDate = @ToDate, ClosePR = @ClosePR, CloseTA = @CloseTA, CloseWE = @CloseWE, CloseTR = @CloseTR, SalaryQtyday = @SalaryQtyday, SalaryResday = @SalaryResday, SalaryProday = @SalaryProday, CalculateType = @CalculateType WHERE CompID = @CompID AND PeriodID = @PeriodID AND EmpType = @EmpType AND PeriodYear = @PeriodYear", connection))
                {
                    SetParameters(command, record);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePeriodicRecord(string compId, string periodId, string empType, string periodYear)
        {
            Obj_conn.doConnect();
            using (SqlConnection connection = Obj_conn.getConnection())
            {

                using (SqlCommand command = new SqlCommand("DELETE FROM tbPOLPeriodic WHERE CompID = @CompID AND PeriodID = @PeriodID AND EmpType = @EmpType AND PeriodYear = @PeriodYear", connection))
                {
                    command.Parameters.AddWithValue("@CompID", compId);
                    command.Parameters.AddWithValue("@PeriodID", periodId);
                    command.Parameters.AddWithValue("@EmpType", empType);
                    command.Parameters.AddWithValue("@PeriodYear", periodYear);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void SetParameters(SqlCommand command, cls_MTPOLPeriodic record)
        {
            command.Parameters.AddWithValue("@CompID", record.CompID);
            command.Parameters.AddWithValue("@PeriodID", record.PeriodID);
            command.Parameters.AddWithValue("@PeriodYear", record.PeriodYear);
            command.Parameters.AddWithValue("@EmpType", record.EmpType);
            command.Parameters.AddWithValue("@PeriodNameT", record.PeriodNameT);
            command.Parameters.AddWithValue("@PeriodNameE", record.PeriodNameE);
            command.Parameters.AddWithValue("@PaymentDate", (object)record.PaymentDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@FromDate", (object)record.FromDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@ToDate", (object)record.ToDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@ClosePR", (object)record.ClosePR ?? DBNull.Value);
            command.Parameters.AddWithValue("@CloseTA", (object)record.CloseTA ?? DBNull.Value);
            command.Parameters.AddWithValue("@CloseWE", (object)record.CloseWE ?? DBNull.Value);
            command.Parameters.AddWithValue("@CloseTR", (object)record.CloseTR ?? DBNull.Value);
            command.Parameters.AddWithValue("@SalaryQtyday", (object)record.SalaryQtyday ?? DBNull.Value);
            command.Parameters.AddWithValue("@SalaryResday", (object)record.SalaryResday ?? DBNull.Value);
            command.Parameters.AddWithValue("@SalaryProday", (object)record.SalaryProday ?? DBNull.Value);
            command.Parameters.AddWithValue("@CalculateType", (object)record.CalculateType ?? DBNull.Value);
        }

        private cls_MTPOLPeriodic ReadPeriodicRecord(SqlDataReader reader)
        {
            return new cls_MTPOLPeriodic
            {
                CompID = reader["CompID"].ToString(),
                PeriodID = reader["PeriodID"].ToString(),
                PeriodYear = reader["PeriodYear"].ToString(),
                EmpType = reader["EmpType"].ToString(),
                PeriodNameT = reader["PeriodNameT"].ToString(),
                PeriodNameE = reader["PeriodNameE"].ToString(),
                PaymentDate = (DateTime)reader["PaymentDate"],
                FromDate = (DateTime)reader["FromDate"],
                ToDate = (DateTime)reader["ToDate"],
                ClosePR = reader["ClosePR"] == DBNull.Value ? null : (bool?)reader["ClosePR"],
                CloseTA = reader["CloseTA"] == DBNull.Value ? null : (bool?)reader["CloseTA"],
                CloseWE = reader["CloseWE"] == DBNull.Value ? null : (bool?)reader["CloseWE"],
                CloseTR = reader["CloseTR"] == DBNull.Value ? null : (bool?)reader["CloseTR"],
                SalaryQtyday = reader["SalaryQtyday"] == DBNull.Value ? null : (int?)reader["SalaryQtyday"],
                SalaryResday = reader["SalaryResday"] == DBNull.Value ? null : (int?)reader["SalaryResday"],
                SalaryProday = reader["SalaryProday"] == DBNull.Value ? null : (int?)reader["SalaryProday"],
                CalculateType = reader["CalculateType"] == DBNull.Value ? null : (bool?)reader["CalculateType"],
            };
        }
    }
}
