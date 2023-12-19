/// <summary>
/// Summary description for Class1
/// </summary>

namespace CarConnect.Exception
{
    internal class InvalidInputException : ApplicationException
    {
        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}

