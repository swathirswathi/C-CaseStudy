using CarConnect.Exception;
using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Interfaces;
using System.Data.SqlClient;
using CarConnect.Utility;
using System.Security.Principal;
using System.Net.NetworkInformation;
namespace CarConnect.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public ReservationRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        #region GetAllReservation
        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Reservation";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationID = (int)reader["ReservationID"];
                        reservation.CustomerID = (int)reader["CustomerID"];
                        reservation.VehicleID = (int)reader["VehicleID"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.TotalCost = (float)(decimal)reader["TotalCost"];
                        reservation.Status = (string)reader["Status"];
                        reservations.Add(reservation);
                    }
                }
            }
            catch (InvalidInputException nodataex)
            {
                Console.WriteLine("No data found");
            }

            return reservations;
        }
        #endregion

        #region GetReservationById
        public Reservation GetReservationById(int r_id)
        {

            Reservation reservation = new Reservation();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Reservation where ReservationID=@R_iid";
                cmd.Parameters.AddWithValue("@R_iid", r_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = (int)reader["ReservationID"];
                        reservation.CustomerID = (int)reader["CustomerID"];
                        reservation.VehicleID = (int)reader["VehicleID"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.TotalCost = (float)(decimal)reader["TotalCost"];
                        reservation.Status = (string)reader["Status"];

                    }
                    return reservation;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region GetReservationsByCustomerId
        public Reservation GetReservationsByCustomerId(int customer_id)
        {

            Reservation reservation = new Reservation();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Reservation where CustomerID=@C_id";
                cmd.Parameters.AddWithValue("@C_id", customer_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = (int)reader["ReservationID"];
                        reservation.CustomerID = (int)reader["CustomerID"];
                        reservation.VehicleID = (int)reader["VehicleID"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.TotalCost = (float)(decimal)reader["TotalCost"];

                    }
                    return reservation;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

   
        public void CreateReservation(Reservation reservation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Reservation(CustomerID,VehicleID,StartDate,EndDate,TotalCost,Status) values(@CustomerID,@VehicleID,@startDate,@endDate,@totalCost,@status)";
                cmd.Parameters.AddWithValue("@CustomerID",reservation.CustomerID);
                cmd.Parameters.AddWithValue("@VehicleID",reservation.VehicleID);
                cmd.Parameters.AddWithValue("@startDate",reservation.StartDate);
                cmd.Parameters.AddWithValue("@endDate",reservation.EndDate);
                cmd.Parameters.AddWithValue("@totalCost",reservation.TotalCost);
                cmd.Parameters.AddWithValue("@status",reservation.Status);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int createReservationStatus;
                try
                {
                    createReservationStatus = cmd.ExecuteNonQuery();
                    Console.WriteLine(createReservationStatus);
                    Console.WriteLine("Reservation Created Successfully");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Duplicate key value"))
                    {
                        Console.WriteLine($"CustomerID:{reservation.CustomerID} already exists");
                    }

                }
            }
        }
                
            public void CancelReservation(int r_id)
        {
            try
            {
                cmd.Parameters.Clear();
                var reservationexists = GetReservationById(r_id);
                if (reservationexists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "delete from Reservation where ReservationID=@R_id";
                        cmd.Parameters.AddWithValue("@R_id", r_id);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int deleteCustomerStatus = cmd.ExecuteNonQuery();
                        Console.WriteLine(deleteCustomerStatus);
                        Console.WriteLine("reservation deleted successfully");
                    }

                }
                else
                {
                    throw new ReservationNotFoundException($"reservation id:{r_id} doesn't exist");
                }
            }
            catch (ReservationNotFoundException ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }


        public void UpdateReservation(int r_id, string status)
        {
            cmd.Parameters.Clear();
            try
            {
                var exists = GetReservationById(r_id);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "update Reservation set Status=@status where ReservationID=@r_id";
                        cmd.Parameters.AddWithValue("@r_id", r_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int updateReservationStatus = cmd.ExecuteNonQuery();
                        exists.Status = status;
                        Console.WriteLine("Updated Successfully");
                    }
                }
                else
                {
                    throw new ReservationNotFoundException($"ReservationID:{r_id} not found");
                }
            }
            catch (ReservationNotFoundException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
        }

        public void CalculateTotalCost(int vehicleId, int reservationid)
        {
            int totalDays = 0;
            Reservation reservation = new Reservation();
            try
            {
                var exists = GetReservationById(reservationid);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "select datediff(day,StartDate,EndDate) AS dateDifference from Reservation where ReservationID=@R_iid";
                        cmd.Parameters.AddWithValue("@R_iid", reservationid);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                totalDays = (int)reader["datedifference"];
                            }
                        }
                    }
                    IVehicleRepository vehicleRepository = new VehicleRepository();
                    double? dailyRate = vehicleRepository.GetDailyRateByVehicleId(vehicleId);
                    if (dailyRate.HasValue)
                    {
                        double totalCost = totalDays * dailyRate.Value;
                        Console.WriteLine($"Total cost is {totalCost}");
                    }
                    
                }
                else
                {
                    throw new ReservationNotFoundException($"Reservation ID or Vehicle ID not found");
                }
            }
            catch (ReservationNotFoundException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

            
        

    }
    
}
