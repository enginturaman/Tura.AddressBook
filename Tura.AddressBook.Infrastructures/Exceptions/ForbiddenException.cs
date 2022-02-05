using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class ForbiddenException : ApiExceptionBase
    {
        public ForbiddenException()
      : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FORBIDDEN, Message = "Yetkisiz Giriş", Data = null } };
        }

        public ForbiddenException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FORBIDDEN, Message = message, Data = null } };
        }

        public ForbiddenException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public ForbiddenException(String message, Exception innerException)
            : base(message, innerException)
        {

            Errors = new Error[] { new Error { Code = ErrorCodes.FORBIDDEN, Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public ForbiddenException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.FORBIDDEN, Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;
    }
}
