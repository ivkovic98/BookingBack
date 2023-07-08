using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Exceptions
{
    public class EmailNotUniqueException :Exception
    {
        public EmailNotUniqueException(string message) : base(message)
        {
            Source = ErrorSource.Conflict.ToString();
        }
    }
}
