namespace CarConnect.Exception
{
    internal class AdminNotFoundException : ApplicationException
    {
        public AdminNotFoundException()
        {

        }
        public AdminNotFoundException(string message) : base(message)
        {

        }
    }
}
