using CarConnect.Exception;
using CarConnect.Interfaces;
using CarConnect.Model;
using CarConnect.Utility;
using System.Data.SqlClient;

namespace CarConnect.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public AdminRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        #region Authenticate
        public int Authenticate(string username, string password)
        {
            try
            {
                var userExists = GetAdminByUserName(username);
                if (userExists != null)
                {
                    if (userExists.Password == password)
                    {
                        Console.WriteLine("Logged in successfully");
                        Console.ReadKey();
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Invalid password");
                        Console.ReadKey();
                        return 0;
                    }
                }
                else
                {
                    throw new AuthenticationException($"Invalid Username");
                }
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine($"Error : {e.Message}");
                return 0;
            }
        }
        #endregion

        #region GetAllAdmin
        public List<Admin> GetAllAdmin()
        {
            List<Admin> admins = new List<Admin>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Admin";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Admin admin = new Admin();
                        admin.AdminID = (int)reader["AdminID"];
                        admin.FirstName = (string)reader["FirstName"];
                        admin.LastName = (string)reader["LastName"];
                        admin.Email = (string)reader["Email"];
                        admin.PhoneNumber = (string)reader["PhoneNumber"];
                        admin.UserName = (string)reader["Username"];
                        admin.Password = (string)reader["Password"];
                        admin.Role = (string)reader["Role"];
                        admin.JoinDate = (DateTime)reader["JoinDate"];
                        admins.Add(admin);
                    }
                }
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("No data found");
            }

            return admins;
        }
        #endregion

        #region GetAdminById
        public Admin GetAdminById(int a_id)
        {

            Admin admin = new Admin();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Admin where AdminID=@A_iid";
                cmd.Parameters.AddWithValue("@A_iid", a_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminID = (int)reader["AdminID"];
                        admin.FirstName = (string)reader["FirstName"];
                        admin.LastName = (string)reader["LastName"];
                        admin.Email = (string)reader["Email"];
                        admin.PhoneNumber = (string)reader["PhoneNumber"];
                        admin.UserName = (string)reader["Username"];
                        admin.Password = (string)reader["Password"];
                        admin.Role = (string)reader["Role"];
                        admin.JoinDate = (DateTime)reader["JoinDate"]; ;

                    }
                    return admin;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region GetAdminByUserName
        public Admin GetAdminByUserName(string u_name)
        {
            Admin admin = new Admin();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Admin where Username=@username";
                cmd.Parameters.AddWithValue("@username", u_name);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminID = (int)reader["AdminID"];
                        admin.FirstName = (string)reader["FirstName"];
                        admin.LastName = (string)reader["LastName"];
                        admin.Email = (string)reader["Email"];
                        admin.PhoneNumber = (string)reader["PhoneNumber"];
                        admin.UserName = (string)reader["Username"];
                        admin.Password = (string)reader["Password"];
                        admin.Role = (string)reader["Role"];
                        admin.JoinDate = (DateTime)reader["JoinDate"]; ;

                    }
                    return admin;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region RegisterAdmin
        public void RegisterAdmin(Admin admin)
        {

            cmd.Parameters.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Admin(FirstName,LastName,Email,PhoneNumber,Username,Password,Role,JoinDate) values(@fName,@lName,@email,@phone,@user,@pw,@role,@join_date)";
                cmd.Parameters.AddWithValue("@fName", admin.FirstName);
                cmd.Parameters.AddWithValue("@lName", admin.LastName);
                cmd.Parameters.AddWithValue("@email", admin.Email);
                cmd.Parameters.AddWithValue("@phone", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@user", admin.UserName);
                cmd.Parameters.AddWithValue("@pw", admin.Password);
                cmd.Parameters.AddWithValue("@role", admin.Role);
                cmd.Parameters.AddWithValue("@join_date", admin.JoinDate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int registerAdminStatus;
                try
                {
                    int regsiterAdminStatus = cmd.ExecuteNonQuery();
                    Console.WriteLine(regsiterAdminStatus);
                    Console.WriteLine("Admin Added Successfully");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("duplicate key value"))
                    {
                        Console.WriteLine($"Username:{admin.UserName} already exists");
                    }

                }
            }

        }

        #endregion

        #region UpdateAdmin
        public void UpdateAdmin(int a_id, string firstname)
        {
            cmd.Parameters.Clear();
            try
            {
                var exists = GetAdminById(a_id);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "update Admin set FirstName=@fname where AdminID=@A_id";
                        cmd.Parameters.AddWithValue("@A_id", a_id);
                        cmd.Parameters.AddWithValue("@fname", firstname);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int updateCustomerStatus = cmd.ExecuteNonQuery();
                        exists.FirstName = firstname;
                        Console.WriteLine("FirstName Updated Successfully");
                    }

                }
                else
                {
                    throw new AdminNotFoundException($"Admin ID : {a_id} not found");
                }
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }


        }
        #endregion

        #region DeleteAdmin
        public void DeleteAdmin(int id)
        {
            try
            {
                cmd.Parameters.Clear();
                var adminExists = GetAdminById(id);
                if (adminExists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "delete from Admin where AdminID=@AA_id";
                        cmd.Parameters.AddWithValue("@AA_id", id);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int deleteAdminStatus = cmd.ExecuteNonQuery();
                        Console.WriteLine(deleteAdminStatus);
                        Console.WriteLine("admin deleted successfully");
                    }

                }
                else
                {
                    throw new AdminNotFoundException($"AdminID :{id} not found");
                }
            }
            catch (AdminNotFoundException e)
            {
                Console.WriteLine($"Error :{e.Message}");

            }
        }


        #endregion

    }
}
