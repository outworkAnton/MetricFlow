using System;

namespace BusinessLogic.BusinessLogic.Contract.Exceptions
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