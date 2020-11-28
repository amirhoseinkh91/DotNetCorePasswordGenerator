namespace Generator
{
    public sealed class PasswordGeneratorOptions
    {
        internal const int MINIMUM_PASSWORD_LENGTH = 8;
        internal const int MAXIMUM_PASSWORD_LENGTH = 128;
        public int MinimumPasswordLength { get; set; } = MINIMUM_PASSWORD_LENGTH;
        public int MaximumPasswordLength { get; set; } = MAXIMUM_PASSWORD_LENGTH;
        public bool IncludeLowercase { get; set; } = true;
        public bool IncludeUppercase { get; set; } = true;
        public bool IncludeNumeric { get; set; } = true;
        public bool IncludeSpecialCharacters { get; set; } = true;
        public bool IncludeSpaces { get; set; } = false;
        public int LengthOfPassword { get; set; } = MINIMUM_PASSWORD_LENGTH;
    }
}
