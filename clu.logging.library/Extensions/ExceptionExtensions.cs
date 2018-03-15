using System;
using System.Linq;

namespace clu.logging.library.Extensions
{
    public static class ExceptionExtentions
    {
        public static string ToExceptionMessage(this Exception exception)
        {
            if (exception == null)
            {
                return "";
            }

            if (exception is AggregateException)
            {
                AggregateException ae = (AggregateException)exception;
                ae = ae.Flatten();

                if (ae.InnerExceptions != null)
                {
                    string inners = string.Join(" - ", ae.InnerExceptions.Select(it => it.ToExceptionMessage()));

                    return $"[{exception.GetType()}]: {ae.Message} - {inners}";
                }
            }

            if (exception.InnerException != null)
            {
                return $"[{exception.GetType()}]: {exception.Message} - {exception.InnerException.ToExceptionMessage()}";
            }

            return $"[{exception.GetType()}]: {exception.Message}";
        }
    }
}
