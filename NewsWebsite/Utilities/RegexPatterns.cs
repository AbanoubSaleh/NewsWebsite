namespace NewsWebsite.Utilities
{
    /// <summary>
    /// Contains regex patterns used throughout the application
    /// </summary>
    public static class RegexPatterns
    {
        /// <summary>
        /// Pattern for Egyptian mobile phone numbers (starts with 01 and has 11 digits total)
        /// </summary>
        public const string EgyptianMobilePhone = @"^01[0-9]{9}$";
        
        /// <summary>
        /// Pattern for standard email addresses
        /// </summary>
        public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
        /// <summary>
        /// Pattern for strong passwords (at least 8 characters, including uppercase, lowercase, number, and special character)
        /// </summary>
        public const string StrongPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
    }
}