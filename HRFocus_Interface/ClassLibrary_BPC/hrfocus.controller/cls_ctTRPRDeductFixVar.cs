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
   public class cls_ctTRPRDeductFixVar
    {
       string Message = string.Empty;
        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPRDeductFixVar() { }
        public string getMessage() { return this.Message.Replace("cls_ctTRPRDeductFixVar", "").Replace("cls_ctTRPRDeductFixVar", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }
        public List<cls_TRPRDeductFixVar> GetDeduction(string compId, string empId, string deductId, DateTime fromDate)
        {
            Obj_conn.doConnect();
            List<cls_TRPRDeductFixVar> allItems = new List<cls_TRPRDeductFixVar>();
            try
            {
                using (SqlConnection connection = Obj_conn.getConnection())
                {

                    string query = "SELECT * FROM tbTRPRDeductFixVar WHERE CompID = @CompID AND EmpID = @EmpID AND DeductID = @DeductID AND FromDate = @FromDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompID", compId);
                        command.Parameters.AddWithValue("@EmpID", empId);
                        command.Parameters.AddWithValue("@DeductID", deductId);
                        command.Parameters.AddWithValue("@FromDate", fromDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cls_TRPRDeductFixVar item = MapDataReaderToDeduction(reader);
                                allItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(GetDeduction)" + ex.ToString();
            }
            return allItems;
        }

        public bool AddDeduction(cls_TRPRDeductFixVar deduction)
        {
            try
            {
                Obj_conn.doConnect();

                if (RecordExists(Obj_conn.getConnection(), deduction.CompID, deduction.EmpID, deduction.DeductID, deduction.FromDate))
                {
                    // If the record exists, update it
                  return  UpdateDeduction(deduction);
                }

                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    string query = @"INSERT INTO tbTRPRDeductFixVar 
                             (CompID, EmpID, DeductID, PeriodID, PeriodFrom, PeriodTo, YearID, Paydate, 
                              FromDate, ToDate, Amount, QuantityAD, PayType, Note)
                             VALUES 
                             (@CompID, @EmpID, @DeductID, @PeriodID, @PeriodFrom, @PeriodTo, @YearID, @Paydate, 
                              @FromDate, @ToDate, @Amount, @QuantityAD, @PayType, @Note)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        MapDeductionToSqlCommandParameters(command, deduction);
                        command.ExecuteNonQuery();
                    }
                }

                // If the code reaches here without any exceptions, return true indicating success
                return true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(AddDeduction)" + ex.ToString();
                // Return false indicating failure
                return false;
            }
        }

        private bool RecordExists(SqlConnection connection, string compId, string empId, string deductId, DateTime fromDate)
        {
            string query = "SELECT COUNT(*) FROM tbTRPRDeductFixVar WHERE CompID = @CompID AND EmpID = @EmpID AND DeductID = @DeductID AND FromDate = @FromDate";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CompID", compId);
                command.Parameters.AddWithValue("@EmpID", empId);
                command.Parameters.AddWithValue("@DeductID", deductId);
                command.Parameters.AddWithValue("@FromDate", fromDate);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        public bool UpdateDeduction(cls_TRPRDeductFixVar deduction)
        {
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    string query = @"UPDATE tbTRPRDeductFixVar 
                             SET CompID = @CompID, EmpID = @EmpID, DeductID = @DeductID, 
                                 PeriodID = @PeriodID, PeriodFrom = @PeriodFrom, PeriodTo = @PeriodTo, 
                                 YearID = @YearID, Paydate = @Paydate, FromDate = @FromDate, 
                                 ToDate = @ToDate, Amount = @Amount, QuantityAD = @QuantityAD, 
                                 PayType = @PayType, Note = @Note
                             WHERE CompID = @CompID AND EmpID = @EmpID AND DeductID = @DeductID AND FromDate = @FromDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        MapDeductionToSqlCommandParameters(command, deduction);
                        command.ExecuteNonQuery();
                    }
                }

                // If the code reaches here without any exceptions, return true indicating success
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception (you can log it or take appropriate action)
                Message = "ERROR::(UpdateDeduction)" + ex.ToString();

                // Return false indicating failure
                return false;
            }
        }


        public bool DeleteDeduction(string compId, string empId, string deductId, DateTime fromDate)
        {
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    string query = "DELETE FROM tbTRPRDeductFixVar WHERE CompID = @CompID AND EmpID = @EmpID AND DeductID = @DeductID AND FromDate = @FromDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompID", compId);
                        command.Parameters.AddWithValue("@EmpID", empId);
                        command.Parameters.AddWithValue("@DeductID", deductId);
                        command.Parameters.AddWithValue("@FromDate", fromDate);

                        command.ExecuteNonQuery();
                    }
                }

                // If the code reaches here without any exceptions, return true indicating success
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception (you can log it or take appropriate action)
                Message = "ERROR::(DeleteDeduction)" + ex.ToString();

                // Return false indicating failure
                return false;
            }
        }


        private cls_TRPRDeductFixVar MapDataReaderToDeduction(SqlDataReader reader)
        {
            cls_TRPRDeductFixVar deduction = new cls_TRPRDeductFixVar();

            deduction.CompID = reader["CompID"].ToString();
            deduction.EmpID = reader["EmpID"].ToString();
            deduction.DeductID = reader["DeductID"].ToString();
            deduction.PeriodID = reader["PeriodID"].ToString();
            deduction.PeriodFrom = reader["PeriodFrom"].ToString();
            deduction.PeriodTo = reader["PeriodTo"].ToString();
            deduction.YearID = reader["YearID"].ToString();
            deduction.Paydate = reader["Paydate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Paydate"]);
            deduction.FromDate = Convert.ToDateTime(reader["FromDate"]);
            deduction.ToDate = Convert.ToDateTime(reader["ToDate"]);
            deduction.Amount = reader["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["Amount"]);
            deduction.QuantityAD = reader["QuantityAD"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["QuantityAD"]);
            deduction.PayType = reader["PayType"].ToString();
            deduction.Note = reader["Note"].ToString();

            return deduction;
        }

        private void MapDeductionToSqlCommandParameters(SqlCommand command, cls_TRPRDeductFixVar deduction)
        {
            command.Parameters.AddWithValue("@CompID", deduction.CompID);
            command.Parameters.AddWithValue("@EmpID", deduction.EmpID);
            command.Parameters.AddWithValue("@DeductID", deduction.DeductID);
            command.Parameters.AddWithValue("@PeriodID", deduction.PeriodID);
            command.Parameters.AddWithValue("@PeriodFrom", deduction.PeriodFrom ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PeriodTo", deduction.PeriodTo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@YearID", deduction.YearID);
            command.Parameters.AddWithValue("@Paydate", deduction.Paydate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FromDate", deduction.FromDate);
            command.Parameters.AddWithValue("@ToDate", deduction.ToDate);
            command.Parameters.AddWithValue("@Amount", deduction.Amount ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@QuantityAD", deduction.QuantityAD ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PayType", deduction.PayType);
            command.Parameters.AddWithValue("@Note", deduction.Note ?? (object)DBNull.Value);
        }

    }
}
