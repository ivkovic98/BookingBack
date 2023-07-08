using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Exceptions
{
    public class RoomNumberConflict :Exception
    {
        public RoomNumberConflict(string message) : base(message)
        {
            Source = ErrorSource.Conflict.ToString();
        }
    }
}
