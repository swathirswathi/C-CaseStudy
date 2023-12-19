namespace CarConnect.Model
{
    public class Reservation
    {
        int reservationId;
        int customerId;
        int vehicleId;
        DateTime startDate;
        DateTime endDate;
        float totalCost;
        string status;

        public int ReservationID
        {
            get { return reservationId; }
            set { reservationId = value; }
        }
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public int VehicleID
        {
            get { return vehicleId; }
            set { vehicleId = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public float TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        //public Reservation(int Id, int C_Id, int V_Id, DateTime S_Date, DateTime E_Date, float Total, string Status)
        //{

        //    reservationId = Id;
        //    customerId = C_Id;
        //    vehicleId = V_Id;
        //    startDate = S_Date;
        //    endDate = E_Date;
        //    totalCost = Total;
        //    status = Status;
        //}
        public override string ToString()
        {
            return $"ReservationId:{reservationId}, CustomerId:{customerId}, VehicleID:{vehicleId}, StartDate:{startDate}, EndDate:{endDate},TotalCost:{TotalCost},Status:{status}";
        }
    }
}
