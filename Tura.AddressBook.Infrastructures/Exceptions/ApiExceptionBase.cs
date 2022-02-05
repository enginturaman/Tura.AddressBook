using System;
using System.Net;
using System.Runtime.Serialization;

namespace Tura.AddressBook.Infrastructures.Exceptions
{
    public abstract class ApiExceptionBase : Exception
    {
        public ApiExceptionBase()
        {
        }

        public ApiExceptionBase(string message) : base(message)
        {
        }

        public ApiExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiExceptionBase(string code, string message, object data = null)
        {
        }
        public ApiExceptionBase(string message, object data = null)
        {
        }
        public ApiExceptionBase(Error[] errors)
        {
            Errors = errors;
        }

        public Error[] Errors { get; set; }

        protected abstract HttpStatusCode HttpStatusCode { get; }

        public int StatusCode => (int)HttpStatusCode;
    }

    public class ApiExceptionResponse
    {
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
