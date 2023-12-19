namespace CarConnect.Exception
{
    internal class ReservationNotFoundException : ApplicationException
    {
        public ReservationNotFoundException()
        {

        }
        public ReservationNotFoundException(string message) : base(message)
        {

        }
    }
}
