using CarConnect.Exception;
using CarConnect.Interfaces;
using CarConnect.Model;
using CarConnect.Utility;
using System.Data.SqlClient;

namespace CarConnect.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        public string connectionString;
        SqlCommand cmd = null;
        public CustomerRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        #region Authenticate
        public int customerAuthenticate(string user, string pass)
        {
            try
            {
                var userExists = GetCustomerByUserName(user);
                if (userExists != null)
                {
                    if (userExists.Password == pass)
                    {
                        Console.WriteLine("Logged in Successfully");
                        Console.ReadKey();
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Password");
                        Console.ReadKey();
                        return 0;

                    }
                }
                else
                {
                    throw new AuthenticationException("Invalid username");

                }

            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
                return 0;
            }
        }
        #endregion

        #region GetAllCustomer
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Customer";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.Address = (string)reader["Address"];
                        customer.Username = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];
                        customers.Add(customer);
                    }
                }
            }
            catch (InvalidInputException nodataex)
            {
                Console.WriteLine("No data found");
            }

            return customers;
        }
        #endregion

        #region GetCustomerById

        public Customer GetCustomerById(int cust_id)
        {

            Customer customer = new Customer();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Customer where CustomerID=@C_iid";
                cmd.Parameters.AddWithValue("@C_iid", cust_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.Address = (string)reader["Address"];
                        customer.Username = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];

                    }
                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region GetCustomerByUserName
        public Customer GetCustomerByUserName(string user)
        {

            Customer customer = new Customer();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Customer where Username=@name";
                cmd.Parameters.AddWithValue("@name", user);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.Address = (string)reader["Address"];
                        customer.Username = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];

                    }
                    return customer;
                }
                else
                {
                    return null;
                }
            }

        }
        #endregion

        #region RegisterCustomer
        public void RegisterCustomer(Customer customer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Customer(FirstName,LastName,Email,PhoneNumber,Address,Username,Password,RegistrationDate) values(@fName,@lName,@email,@phone,@address,@user,@pw,@reg_date)";
                cmd.Parameters.AddWithValue("@fName", customer.FirstName);
                cmd.Parameters.AddWithValue("@lName", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@phone", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@user", customer.Username);
                cmd.Parameters.AddWithValue("@pw", customer.Password);
                cmd.Parameters.AddWithValue("@reg_date", customer.RegistrationDate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int registerCustomerStatus;
                try
                {
                    int regsiterCustomerStatus = cmd.ExecuteNonQuery();
                    Console.WriteLine(regsiterCustomerStatus);
                    Console.WriteLine("Customer Added Successfully");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("duplicate key value"))
                    {
                        Console.WriteLine($"Username:{customer.Username} already exists");
                    }

                }
            }
        }
        #endregion

        #region DeleteCustomer
        public void DeleteCustomer(int c_id)
        {
            try
            {
                cmd.Parameters.Clear();
                var customerExists = GetCustomerById(c_id);
                if (customerExists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "delete from Reservation where CustomerID=@CC_id";
                        cmd.Parameters.AddWithValue("@CC_id", c_id);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int deleteCustomerStatus = cmd.ExecuteNonQuery();
                        cmd.CommandText = "delete from Customer where CustomerID=@C_id";
                        cmd.Parameters.AddWithValue("@C_id", c_id);
                        int deleteCustomeStatus = cmd.ExecuteNonQuery();
                        Console.WriteLine(deleteCustomeStatus);
                        Console.WriteLine("Customer deleted Successfully");
                    }

                }
                else
                {
                    throw new CustomerNotFoundException($"CustomerID :{c_id} not found");
                }
            }
            catch (CustomerNotFoundException e)
            {
                Console.WriteLine($"Error :{e.Message}");
            }
        }
        #endregion

        #region UpdateCustomer
        public void UpdateCustomer(int id, string mail)
        {
            cmd.Parameters.Clear();
            try
            {
                var exists = GetCustomerById(id);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "update Customer set Email=@email where CustomerID=@CC_id";
                        cmd.Parameters.AddWithValue("@CC_id", id);
                        cmd.Parameters.AddWithValue("@email", mail);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int updateCustomerStatus = cmd.ExecuteNonQuery();
                        exists.Email = mail;
                        Console.WriteLine("Updated Successfully");
                    }

                }
                else
                {
                    throw new CustomerNotFoundException($"Customer ID : {id} not found");
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
        #endregion


    }
}


