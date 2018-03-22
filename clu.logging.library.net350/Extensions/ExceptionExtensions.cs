using System;

namespace clu.logging.library.net350.Extensions
{
    public static class ExceptionExtentions
    {
        public static string ToExceptionMessage(this Exception exception)
        {
            if (exception == null)
            {
                return "";
            }

            if (exception.InnerException != null)
            {
                return $"[{exception.GetType()}]: {exception.Message} - {exception.InnerException.ToExceptionMessage()}";
            }

            return $"[{exception.GetType()}]: {exception.Message}";
        }
    }
}
