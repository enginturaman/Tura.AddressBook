using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class NotFoundException : ApiExceptionBase
    {
        public NotFoundException()
      : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTFOUND, Message = "Kayıt Bulunamadı", Data = null } };
        }

        public NotFoundException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTFOUND, Message = message, Data = null } };
        }

        public NotFoundException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public NotFoundException(String message, Exception innerException)
            : base(message, innerException)
        {

            Errors = new Error[] { new Error { Code = ErrorCodes.NOTFOUND, Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public NotFoundException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.NOTFOUND, Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;
    }
}
