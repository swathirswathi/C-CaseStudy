namespace CarConnect.Model
{
    public class Vehicle
    {
        int vehicleId;
        string model;
        string make;
        int year;
        string color;
        string registrationNumber;
        bool availability;
        float dailyRate;

        public int VehicleID
        {
            get { return vehicleId; }
            set { vehicleId = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Make
        {
            get { return make; }
            set { make = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { registrationNumber = value; }
        }
        public bool Availability
        {
            get { return availability; }
            set { availability = value; }
        }
        public float DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }

        //public Vehicle(int v_id, string Model, string Make, int Year, string Color, string r_Number, bool Available, float Rate)
        //{
        //    vehicleId = v_id;
        //    model = Model;
        //    make = Make;
        //    year = Year;
        //    color = Color;
        //    registrationNumber = r_Number;
        //    availability = Available;
        //    dailyRate = Rate;
        //}
        public override string ToString()
        {
            return $"VehicleID:{vehicleId},Model:{model},Make:{make},Year:{year},Color:{color},RegistrationNumber:{registrationNumber},Availability:{availability},DailyRate:{dailyRate}";
        }
    }
}
