using System;

namespace TVShowTracker.Webapi.Application.Services
{
    public static class ExceptionService
    {
        public static string GetExcetionMessage(Exception ex) 
        {
            if (ex == null)
                return null;

            string template = "Message: {0} -> Stack trace: {1}";
            string message = string.Format(template, ex.Message, ex.StackTrace?.ToString());

            Exception innerException = ex.InnerException;

            while (innerException != null) 
            {
                message = string.Concat(" | ", message, " ", string.Format(template, innerException.Message, innerException.StackTrace));
                innerException = innerException.InnerException;
            }

            return message;
        }
    }
}
