using System;

namespace BusinessLogic.BusinessLogic.Contract.Exceptions
{
    public class NetworkException : Exception
    {
        public override string Message { get; }

        public NetworkException(string message)
        {
            Message = message;
        }
    }
}