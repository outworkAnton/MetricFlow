using System;

namespace MetricFlowExceptions
{
    public class FileException : Exception
    {
        public override string Message { get; }

        public FileException(string message)
        {
            Message = message;
        }
    }
}