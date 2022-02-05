using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tura.AddressBook.Infrastructures.Exceptions;

namespace Tura.AddressBook.Services.Validation
{

    public class ValidationHandling : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var notFoundError = result.Errors.FirstOrDefault(x => x.ErrorCode == ErrorCodes.NOTFOUND);
                if (notFoundError != null)
                {
                    throw new NotFoundException(notFoundError.ErrorMessage);
                }

                if (result.Errors != null && result.Errors.Any())
                {
                    List<Error> errors = new List<Error>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(new Error
                        {
                            Code = (error.ErrorCode != null ? error.ErrorCode : ErrorCodes.NOTACCEPTABLE),
                            Data = string.Empty,
                            Message = error.ErrorMessage
                        });
                    }
                    throw new NotAcceptableException(errors.ToArray());
                }
            }

            return result;
        }
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}
