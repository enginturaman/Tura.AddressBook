using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tura.AddressBook.Infrastructures.Helpers
{
    public class RegexHelper
    {
        public static bool EmailIsValid(string emailAddress)
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

            bool isValid = regex.IsMatch(emailAddress);

            return isValid;
        }
    }
}
