using System;
using System.Net;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public class TooManyRequestException : ApiExceptionBase
    {
        public TooManyRequestException()
       : base()
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.TOOMANYREQUEST, Message = "İstek Limiti Aşıldı." , Data = null } };
        }

        public TooManyRequestException(String message)
            : base(message)

        {
            Errors = new Error[] { new Error { Code = ErrorCodes.TOOMANYREQUEST , Message = message, Data = null } };
        }

        public TooManyRequestException(string code, string message, object data = null) : base(code, message, data)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public TooManyRequestException(String message, Exception innerException)
            : base(message, innerException)
        { 
        
            Errors = new Error[] { new Error { Code = ErrorCodes.TOOMANYREQUEST , Message = $"{message} - Inner Ex : {innerException}", Data = null } };
        }

        public TooManyRequestException(string message, object data = null) : base(message, data)
        {
            Errors = new Error[] { new Error { Code = ErrorCodes.TOOMANYREQUEST , Message = message, Data = data } };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.TooManyRequests;
    }
}
