using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class NotAcceptableException : ApiExceptionBase
    {
        public NotAcceptableException()
      : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTACCEPTABLE, Message = "Kabul Edilmeyen İstek", Data = null } };
        }

        public NotAcceptableException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTACCEPTABLE, Message = message, Data = null } };
        }
        public NotAcceptableException(Error[] errors)
            : base(errors)

        {
            Errors = errors;
        }

        public NotAcceptableException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public NotAcceptableException(String message, Exception innerException)
            : base(message, innerException)
        {

            Errors = new Error[] { new Error { Code = ErrorCodes.NOTACCEPTABLE, Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public NotAcceptableException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTACCEPTABLE, Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NotAcceptable;
    }
}
