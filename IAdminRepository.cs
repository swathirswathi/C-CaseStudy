using CarConnect.Model;

namespace CarConnect.Interfaces
{
    public interface IAdminRepository
    {
        int Authenticate(string username, string password);
        List<Admin> GetAllAdmin();
        Admin GetAdminById(int id);
        Admin GetAdminByUserName(string u_name);
        void RegisterAdmin(Admin admin);
        void UpdateAdmin(int a_id, string firstname);

        void DeleteAdmin(int id);


    }
}
