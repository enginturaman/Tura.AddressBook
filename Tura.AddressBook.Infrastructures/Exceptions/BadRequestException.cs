using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class BadRequestException : ApiExceptionBase
    {
        public BadRequestException()
      : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.BADREQUEST, Message = "Geçersiz İstek", Data = null } };
        }

        public BadRequestException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.BADREQUEST, Message = message, Data = null } };
        }

        public BadRequestException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public BadRequestException(String message, Exception innerException)
            : base(message, innerException)
        {

            Errors = new Error[] { new Error { Code = ErrorCodes.BADREQUEST, Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public BadRequestException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.BADREQUEST, Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
