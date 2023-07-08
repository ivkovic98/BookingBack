using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Exceptions
{
    public  class InvalidLoginException : Exception
    { 
        public InvalidLoginException(string message) : base(message)
        {
            Source = ErrorSource.Authentication.ToString();
        }
    }
}
