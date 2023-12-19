using CarConnect.Exception;
using CarConnect.Interfaces;
using CarConnect.Model;
using CarConnect.Utility;
using System.Data.SqlClient;
namespace CarConnect.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public VehicleRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Vehicle";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.VehicleID = (int)reader["VehicleID"];
                        vehicle.Model = (string)reader["Model"];
                        vehicle.Make = (string)reader["Make"];
                        vehicle.Year = (int)reader["Year"];
                        vehicle.Color = (string)reader["Color"];
                        vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                        vehicle.Availability = (bool)reader["Availability"];
                        vehicle.DailyRate = (float)(decimal)reader["DailyRate"];
                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (InvalidInputException nodataex)
            {
                Console.WriteLine("No data found");
            }

            return vehicles;
        }

        #region GetVehicleById
        public Vehicle GetVehicleById(int vehi_id)
        {

            Vehicle vehicle = new Vehicle();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "select * from Vehicle where VehicleID=@V_iid";
                cmd.Parameters.AddWithValue("@V_iid", vehi_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle.VehicleID = (int)reader["VehicleID"];
                        vehicle.Model = (string)reader["Model"];
                        vehicle.Make = (string)reader["Make"];
                        vehicle.Year = (int)reader["Year"];
                        vehicle.Color = (string)reader["Color"];
                        vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                        vehicle.Availability = (bool)reader["Availability"];
                        vehicle.DailyRate = (float)(decimal)reader["DailyRate"];

                    }
                    return vehicle;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region GetAvailableVehicles
        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Vehicle WHERE Availability=1";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.VehicleID = (int)reader["VehicleID"];
                        vehicle.Model = (string)reader["Model"];
                        vehicle.Make = (string)reader["Make"];
                        vehicle.Year = (int)reader["Year"];
                        vehicle.Color = (string)reader["Color"];
                        vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                        vehicle.Availability = (bool)reader["Availability"];
                        vehicle.DailyRate = (float)(decimal)reader["DailyRate"];
                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (InvalidInputException nodataex)
            {
                Console.WriteLine("No data found");
            }

            return vehicles;
        }
        #endregion


        public void AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Vehicle(Make,Year ,Color,RegistrationNumber,Availability,DailyRate) values(@make,@year,@color,@reg_no,@availability,@daily_rate)";
                cmd.Parameters.AddWithValue("@make", vehicle.Make);
                cmd.Parameters.AddWithValue("@year", vehicle.Year);
                cmd.Parameters.AddWithValue("@color", vehicle.Color);
                cmd.Parameters.AddWithValue("@reg_no", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@availability", vehicle.Availability);
                cmd.Parameters.AddWithValue("@daily_rate", vehicle.DailyRate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int addVehicleStatus;
                try
                {
                    addVehicleStatus = cmd.ExecuteNonQuery();
                    Console.WriteLine(addVehicleStatus);
                    Console.WriteLine("Vehicle Added Successfully");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("duplicate key value"))
                    {
                        Console.WriteLine($"RegistrationNumber:{vehicle.RegistrationNumber} already exists");
                    }

                }
            }


        }


        public void RemoveVehicle(int v_id)
        {
            try
            {
                cmd.Parameters.Clear();
                var vehicleExists = GetVehicleById(v_id);
                if (vehicleExists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "delete from Reservation where VehicleID=@V_id";
                        cmd.Parameters.AddWithValue("@V_id", v_id);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int deleteVehicleStatus = cmd.ExecuteNonQuery();
                        cmd.CommandText = "delete from Vehicle where VehicleID=@VV_id";
                        cmd.Parameters.AddWithValue("@VV_id", v_id);
                        int deleteVehiclStatus = cmd.ExecuteNonQuery();
                        Console.WriteLine(deleteVehiclStatus);
                        Console.WriteLine("Vehicle deleted Successfully");
                    }

                }
                else
                {
                    throw new VehicleNotFoundException($"Vehicle ID:{v_id} doesn't exist");
                }
            }
            catch (VehicleNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void UpdateVehicle(int vehi_id, string color)
        {
            cmd.Parameters.Clear();
            try
            {
                var exists = GetVehicleById(vehi_id);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "update Vehicle set Color=@color where VehicleID=@V_id";
                        cmd.Parameters.AddWithValue("@V_id", vehi_id);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int updateVehicleStatus = cmd.ExecuteNonQuery();
                        exists.Color = color;
                        Console.WriteLine("Color Updated Successfully");
                    }
                }
                else
                {
                    throw new VehicleNotFoundException($"VehicleID:{vehi_id} not found");
                }
            }
            catch (VehicleNotFoundException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
        }
        public double? GetDailyRateByVehicleId(int vehicleId)
        {
            cmd.Parameters.Clear();
            var exists = GetVehicleById(vehicleId);
            if (exists != null)
            {
                Vehicle vehicle = new Vehicle();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    cmd.CommandText = "SELECT DailyRate FROM Vehicle WHERE VehicleID=@V_id";
                    cmd.Parameters.AddWithValue("@V_id", vehicleId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            vehicle.DailyRate = (float)(decimal)reader["DailyRate"];

                        }
                        return vehicle.DailyRate;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            else
            {
                return null;
            }

        }


    }
}
