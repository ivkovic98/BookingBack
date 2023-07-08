using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Exceptions
{
    public class ResourceNotFoundException :Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
            Source = ErrorSource.Database.ToString();
        }
    }
}
