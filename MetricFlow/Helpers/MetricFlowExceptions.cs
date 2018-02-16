using System;

namespace MetricFlow.Helpers
{
    public class NetworkException : Exception
    {
        public override string Message { get; }

        public NetworkException(string message)
        {
            Message = message;
        }
    }

    public class ServiceException : Exception
    {
        public override string Message { get; }

        public ServiceException(string message)
        {
            Message = message;
        }
    }

    public class FileException : Exception
    {
        public override string Message { get; }

        public FileException(string message)
        {
            Message = message;
        }
    }
}