using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Generator
{
    public class PasswordGenerator
    {
        const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
        const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
        const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMERIC_CHARACTERS = "0123456789";
        const string SPECIAL_CHARACTERS = @"!#$%&*@\";
        const string SPACE_CHARACTER = " ";


        private readonly int minimumPasswordLength = PasswordGeneratorOptions.MINIMUM_PASSWORD_LENGTH;
        private readonly int maximumPasswordLength = PasswordGeneratorOptions.MAXIMUM_PASSWORD_LENGTH;

        private readonly bool includeLowercase;
        private readonly bool includeUppercase;
        private readonly bool includeNumeric;
        private readonly bool includeSpecialCharacters;
        private readonly bool includeSpaces;
        private readonly int lengthOfPassword = PasswordGeneratorOptions.MINIMUM_PASSWORD_LENGTH;

        public PasswordGenerator(PasswordGeneratorOptions options)
        {
            minimumPasswordLength = options.MinimumPasswordLength;
            maximumPasswordLength = options.MaximumPasswordLength;
            includeLowercase = options.IncludeLowercase;
            includeUppercase = options.IncludeUppercase;
            includeNumeric = options.IncludeNumeric;
            includeSpecialCharacters = options.IncludeSpecialCharacters;
            includeSpaces = options.IncludeSpaces;
            lengthOfPassword = options.LengthOfPassword;
        }

        public PasswordGenerator(PasswordOptions options)
        {
            includeLowercase = options.RequireLowercase;
            includeUppercase = options.RequireUppercase;
            includeNumeric = options.RequireDigit;
            includeSpecialCharacters = options.RequireNonAlphanumeric;
            includeSpaces = options.RequireNonAlphanumeric;
            lengthOfPassword = options.RequiredLength;
        }
        /// <summary>
        /// Generates a random password based on the rules passed in the parameters
        /// </summary>
        /// <returns>GeneratedPassword</returns>
        public string Generate(int? length)
        {
            if (length == null)
                length = lengthOfPassword;
            if (length < minimumPasswordLength || length > maximumPasswordLength)
                throw new PasswordLengthNotCompatibleException();

            string characterSet = "";

            if (includeLowercase)
                characterSet += LOWERCASE_CHARACTERS;

            if (includeUppercase)
                characterSet += UPPERCASE_CHARACTERS;

            if (includeNumeric)
                characterSet += NUMERIC_CHARACTERS;

            if (includeSpecialCharacters)
                characterSet += SPECIAL_CHARACTERS;

            if (includeSpaces)
                characterSet += SPACE_CHARACTER;

            char[] password = new char[lengthOfPassword];
            int characterSetLength = characterSet.Length;

            System.Random random = new System.Random();
            for (int characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                    characterPosition--;
            }

            return string.Join(null, password);
        }

        /// <summary>
        /// Checks if the password is valid or not
        /// </summary>
        /// <param name="password">Password string to check</param>
        /// <returns>True or False to say if the password is valid or not</returns>
        public bool IsValid(string password)
        {
            const string REGEX_LOWERCASE = @"[a-z]";
            const string REGEX_UPPERCASE = @"[A-Z]";
            const string REGEX_NUMERIC = @"[\d]";
            const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
            const string REGEX_SPACE = @"([ ])+";

            bool lowerCaseIsValid = !includeLowercase || (includeLowercase && Regex.IsMatch(password, REGEX_LOWERCASE));
            bool upperCaseIsValid = !includeUppercase || (includeUppercase && Regex.IsMatch(password, REGEX_UPPERCASE));
            bool numericIsValid = !includeNumeric || (includeNumeric && Regex.IsMatch(password, REGEX_NUMERIC));
            bool symbolsAreValid = !includeSpecialCharacters || (includeSpecialCharacters && Regex.IsMatch(password, REGEX_SPECIAL));
            bool spacesAreValid = !includeSpaces || (includeSpaces && Regex.IsMatch(password, REGEX_SPACE));

            return lowerCaseIsValid && upperCaseIsValid && numericIsValid && symbolsAreValid && spacesAreValid;
        }
    }
}
