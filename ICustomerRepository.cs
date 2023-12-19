using CarConnect.Model;

namespace CarConnect.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        void RegisterCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer GetCustomerByUserName(string user);
        void DeleteCustomer(int id);
        int customerAuthenticate(string user, string pass);
        void UpdateCustomer(int id, string mail);
    }
}

