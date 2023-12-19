using CarConnect.Model;

namespace CarConnect.Interfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int id);
        List<Vehicle> GetAvailableVehicles();
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(int v_id);
        void UpdateVehicle(int vehi_id, string color);
        double? GetDailyRateByVehicleId(int vehicleId);


    }
}
