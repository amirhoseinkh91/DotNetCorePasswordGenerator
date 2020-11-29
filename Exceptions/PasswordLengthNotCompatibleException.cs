using System;

namespace Password.Generator.Exceptions
{
    public class PasswordLengthNotCompatibleException : Exception
    {
        private const string message = "Requested password length is not compatible with configurations.";
        public PasswordLengthNotCompatibleException() : base(message)
        {

        }
    }
}
