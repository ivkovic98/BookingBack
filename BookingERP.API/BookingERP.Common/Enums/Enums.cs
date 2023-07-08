namespace BookingERP.Common.Enums
{
    public class Enums
    {
        public enum UserRole
        {
            Guest = 1,
            Manager = 2,
            Admin = 3,
        }

        public enum ErrorSource
        {
            Request,
            Authentication,
            Authorization,
            Database,
            Application,
            Conflict
        }
    }
}
