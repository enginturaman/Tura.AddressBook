using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Helpers;
using Tura.AddressBook.Services.Validation;

namespace NetBankaGold.Services.Core.Validation
{
    public class PersonalContactValidator : AbstractValidator<PersonalContactModel>, IBaseValidator
    {
        public new ValidationResult Validate(PersonalContactModel model)
        {
            return base.Validate(model);
        }

        public PersonalContactValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).Must(CheckEmail).WithMessage("Email Format is incorrect").WithErrorCode("InvalidEmailFormat");
            RuleFor(x => x.PhoneNumber).Must(IsPhoneNbr).WithMessage("Phone Format is incorrect").WithErrorCode("InvalidPhoneNumber");
        }

        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
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
