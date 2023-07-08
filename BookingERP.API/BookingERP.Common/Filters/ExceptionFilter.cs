using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static BookingERP.Common.Enums.Enums;

namespace BookingERP.Common.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static readonly IReadOnlyDictionary<string, int> _errorCodes = new Dictionary<string, int>
        {
            { ErrorSource.Request.ToString(), 400 },
            { ErrorSource.Authentication.ToString(), 401 },
            { ErrorSource.Authorization.ToString(), 403 },
            { ErrorSource.Database.ToString(), 404 },
            { ErrorSource.Conflict.ToString(), 409 },
            { ErrorSource.Application.ToString(), 500 },
        };

        public void OnException(ExceptionContext context)
        {
            var errorResponse = GetErrorResponse(context);

            var result = new JsonResult(errorResponse)
            {
                StatusCode = errorResponse.Code,
                ContentType = "application/json"
            };

            context.Result = result;
        }

        private static ErrorResponse GetErrorResponse(ExceptionContext context)
        {
            var exception = context.Exception;

            ErrorResponse errorResponse = new()
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace
            };

            var isErrorCodeFound = _errorCodes.TryGetValue(exception.Source, out var errorCode);

            if (isErrorCodeFound)
            {
                errorResponse.Type = exception.Source;
                errorResponse.Code = errorCode;
            }
           
            return errorResponse;
        }
    }
}
