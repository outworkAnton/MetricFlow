using System;

namespace BusinessLogic.Contract.Exceptions
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