using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Helpers;
using Tura.AddressBook.Services.Validation;

namespace NetBankaGold.Services.Core.Validation
{
    public class PersonalValidator : AbstractValidator<PersonalModel>, IBaseValidator
    {

        public new ValidationResult Validate(PersonalModel model)
        {
            return  base.Validate(model);
        }

        public PersonalValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull().WithMessage("Name field cannot be empty").WithErrorCode("MissingDataField");
        }

        public bool CheckEmail(string email)
        {

            bool isValid = IsValidEmail(email);
            if (!isValid)
            {
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                return RegexHelper.EmailIsValid(emailaddress);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
