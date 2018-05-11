using System;

namespace MetricFlowExceptions
{
    public class ServiceException : Exception
    {
        public override string Message { get; }

        public ServiceException(string message)
        {
            Message = message;
        }
    }
}