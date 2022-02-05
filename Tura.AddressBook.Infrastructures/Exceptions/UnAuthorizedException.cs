using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class UnAuthorizedException : ApiExceptionBase
    {
        public UnAuthorizedException()
       : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.UNAUTHORIZED, Message = "Kimlik bilgileri doğrulanmadı" , Data = null } };
        }

        public UnAuthorizedException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.UNAUTHORIZED , Message = message, Data = null } };
        }

        public UnAuthorizedException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public UnAuthorizedException(String message, Exception innerException)
            : base(message, innerException)
        { 
        
            Errors = new Error[] { new Error { Code = ErrorCodes.UNAUTHORIZED , Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public UnAuthorizedException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.UNAUTHORIZED , Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;
    }
}
