using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Filters
{
    public class ErrorResponse 
    {
        public string? Message { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        public string? StackTrace { get; set; }

        public ErrorResponse()
        {
            Code = 500;
            Type = ErrorSource.Application.ToString();
        }
    }
}
