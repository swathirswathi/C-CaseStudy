using CarConnect.Model;

namespace CarConnect.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetAllReservations();
        Reservation GetReservationById(int id);
        Reservation GetReservationsByCustomerId(int customerId);
        void CreateReservation(Reservation reservation);
        void CancelReservation(int id);
        void UpdateReservation(int r_id, string status);
        void CalculateTotalCost(int vehicleId, int reservationid);
    }
}
