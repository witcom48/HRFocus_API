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
 public   class cls_ctTRPRAllOwDeducVarFix
    {
      string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPRAllOwDeducVarFix() {}
        public string getMessage() { return this.Message.Replace("tbTRPRAllOwDeducVarFix", "").Replace("tbTRPRAllOwDeducVarFix", "").Replace("line", ""); }

        public void dispose()
        {
            Obj_conn.doClose();
        }
     public bool AddAllOwDeducVarFix(cls_TRPRAllOwDeducVarFix item)
        {
            Obj_conn.doConnect();
            bool blnResult = false;
            if (RecordExists(Obj_conn.getConnection(), item.CompID, item.EmpID, item.AllwDeducID, item.FromDate, item.ToDate))
            {
                // If it exists, update the record
                return UpdateAllOwDeducVarFix(item);
            }
            try
            {
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    string query = @"INSERT INTO [dbo].[tbTRPRAllOwDeducVarFix]
                            VALUES (@CompID, @EmpID, @AllwDeducID, @PeriodID, @PeriodFrom, @PeriodTo, @YearID, @Paydate, @FromDate, @ToDate, @Amount, @QuantityAD, @MoneyPerPiece, @Piece, @PayType, @Note)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SetParameters(command, item);
                        command.ExecuteNonQuery();
                        blnResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(AddAllOwDeducVarFix)" + ex.ToString();
                blnResult = false;
            }
            return blnResult;
        }
     private bool RecordExists(SqlConnection connection, string compId, string empId, string allwDeducId, DateTime fromDate, DateTime toDate)
     {
         string query = @"SELECT COUNT(1) FROM [dbo].[tbTRPRAllOwDeducVarFix]
                    WHERE CompID = @CompID AND EmpID = @EmpID AND AllwDeducID = @AllwDeducID AND FromDate = @FromDate AND ToDate = @ToDate";
         try
         {
             using (SqlCommand command = new SqlCommand(query, connection))
             {
                 command.Parameters.AddWithValue("@CompID", compId);
                 command.Parameters.AddWithValue("@EmpID", empId);
                 command.Parameters.AddWithValue("@AllwDeducID", allwDeducId);
                 command.Parameters.AddWithValue("@FromDate", fromDate);
                 command.Parameters.AddWithValue("@ToDate", toDate);

                 int count = (int)command.ExecuteScalar();

                 return count > 0;
             }
         }
         catch (Exception ex)
         {
             Message = "ERROR::(RecordExists)" + ex.ToString();
         }
         return false;
     }
     public bool UpdateAllOwDeducVarFix(cls_TRPRAllOwDeducVarFix item)
        {
            bool blnResult = false;
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    

                    string query = @"UPDATE [dbo].[tbTRPRAllOwDeducVarFix]
                            SET Amount = @Amount, QuantityAD = @QuantityAD, MoneyPerPiece = @MoneyPerPiece, Piece = @Piece, PayType = @PayType, Note = @Note
                            WHERE CompID = @CompID AND EmpID = @EmpID AND AllwDeducID = @AllwDeducID AND FromDate = @FromDate AND ToDate = @ToDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SetParameters(command, item);
                        command.ExecuteNonQuery();
                        blnResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(UpdateAllOwDeducVarFix)" + ex.ToString();
                blnResult = true;
            }
            return blnResult;
        }

        public bool DeleteAllOwDeducVarFix(string compId, string empId, string allwDeducId, DateTime fromDate, DateTime toDate)
        {
            bool blnResult = false;
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    

                    string query = @"DELETE FROM [dbo].[tbTRPRAllOwDeducVarFix]
                            WHERE CompID = @CompID AND EmpID = @EmpID AND AllwDeducID = @AllwDeducID AND FromDate = @FromDate AND ToDate = @ToDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompID", compId);
                        command.Parameters.AddWithValue("@EmpID", empId);
                        command.Parameters.AddWithValue("@AllwDeducID", allwDeducId);
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);

                        command.ExecuteNonQuery();
                        blnResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(DeleteAllOwDeducVarFix)" + ex.ToString();
                blnResult = false;
            }
            return blnResult;
        }

        public List<cls_TRPRAllOwDeducVarFix> GetSingleAllOwDeducVarFix(string compId, string empId, string allwDeducId, DateTime fromDate, DateTime toDate)
        {
            List<cls_TRPRAllOwDeducVarFix> allItems = new List<cls_TRPRAllOwDeducVarFix>();
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    

                    string query = @"SELECT * FROM [dbo].[tbTRPRAllOwDeducVarFix]
                            WHERE CompID = @CompID AND EmpID = @EmpID AND AllwDeducID = @AllwDeducID AND FromDate = @FromDate AND ToDate = @ToDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompID", compId);
                        command.Parameters.AddWithValue("@EmpID", empId);
                        command.Parameters.AddWithValue("@AllwDeducID", allwDeducId);
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cls_TRPRAllOwDeducVarFix item = MapDataRowToAllOwDeducVarFix(reader);
                                allItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(GetSingleAllOwDeducVarFix)" + ex.ToString();
            }
            return allItems;
        }

        public List<cls_TRPRAllOwDeducVarFix> GetAllAllOwDeducVarFix()
        {
            List<cls_TRPRAllOwDeducVarFix> allItems = new List<cls_TRPRAllOwDeducVarFix>();
            try
            {
                Obj_conn.doConnect();
                using (SqlConnection connection = Obj_conn.getConnection())
                {
                    
                    string query = @"SELECT * FROM [dbo].[tbTRPRAllOwDeducVarFix]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cls_TRPRAllOwDeducVarFix item = MapDataRowToAllOwDeducVarFix(reader);
                                allItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(GetAllAllOwDeducVarFix)" + ex.ToString();
            }

            return allItems;
        }

        private void SetParameters(SqlCommand command, cls_TRPRAllOwDeducVarFix item)
        {
            command.Parameters.AddWithValue("@CompID", item.CompID);
            command.Parameters.AddWithValue("@EmpID", item.EmpID);
            command.Parameters.AddWithValue("@AllwDeducID", item.AllwDeducID);
            command.Parameters.AddWithValue("@PeriodID", item.PeriodID);
            command.Parameters.AddWithValue("@PeriodFrom", item.PeriodFrom ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PeriodTo", item.PeriodTo?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@YearID", item.YearID);
            command.Parameters.AddWithValue("@Paydate", item.Paydate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FromDate", item.FromDate);
            command.Parameters.AddWithValue("@ToDate", item.ToDate);
            command.Parameters.AddWithValue("@Amount", item.Amount ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@QuantityAD", item.QuantityAD ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@MoneyPerPiece", item.MoneyPerPiece ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Piece", item.Piece ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PayType", item.PayType ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Note", item.Note ?? (object)DBNull.Value);
        }

        private cls_TRPRAllOwDeducVarFix MapDataRowToAllOwDeducVarFix(SqlDataReader reader)
        {
            return new cls_TRPRAllOwDeducVarFix
            {
                CompID = reader["CompID"].ToString(),
                EmpID = reader["EmpID"].ToString(),
                AllwDeducID = reader["AllwDeducID"].ToString(),
                PeriodID = reader["PeriodID"].ToString(),
                PeriodFrom = reader["PeriodFrom"].ToString(),
                PeriodTo = reader["PeriodTo"].ToString(),
                YearID = reader["YearID"].ToString(),
                Paydate = reader["Paydate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["Paydate"],
                FromDate = (DateTime)reader["FromDate"],
                ToDate = (DateTime)reader["ToDate"],
                Amount = reader["Amount"] == DBNull.Value ? (decimal?)null : (decimal)reader["Amount"],
                QuantityAD = reader["QuantityAD"] == DBNull.Value ? (decimal?)null : (decimal)reader["QuantityAD"],
                MoneyPerPiece = reader["MoneyPerPiece"] == DBNull.Value ? (decimal?)null : (decimal)reader["MoneyPerPiece"],
                Piece = reader["Piece"] == DBNull.Value ? (decimal?)null : (decimal)reader["Piece"],
                PayType = reader["PayType"].ToString(),
                Note = reader["Note"].ToString()
            };
        }
    }
}
