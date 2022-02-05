using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{

    public class FoundException : ApiExceptionBase
    {
        public FoundException()
      : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FOUND, Message = "Kayıt Mevcut", Data = null } };
        }

        public FoundException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FOUND, Message = message, Data = null } };
        }

        public FoundException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public FoundException(String message, Exception innerException)
            : base(message, innerException)
        {

            Errors = new Error[] { new Error { Code = ErrorCodes.FOUND, Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public FoundException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FOUND, Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Found;
    }
}
