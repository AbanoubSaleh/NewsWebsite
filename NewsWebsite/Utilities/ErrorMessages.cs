namespace NewsWebsite.Utilities
{
    /// <summary>
    /// Contains error messages used throughout the application
    /// </summary>
    public static class ErrorMessages
    {
        // General validation errors
        public const string RequiredField = "{0} is required";
        public const string InvalidFormat = "Invalid {0} format";
        
        // User registration errors
        public const string NameRequired = "Name is required";
        public const string EmailRequired = "Email is required";
        public const string InvalidEmail = "Invalid email address";
        public const string PhoneRequired = "Mobile phone is required";
        public const string InvalidEgyptianPhone = "Phone number must start with '01' and be 11 digits long";
        public const string PasswordRequired = "Password is required";
        public const string PasswordLength = "The {0} must be at least {2} characters long.";
        public const string PasswordMismatch = "The password and confirmation password do not match.";
        
        // Account errors
        public const string UserAlreadyExists = "A user with this email already exists";
        public const string InvalidLoginAttempt = "Invalid login attempt.";
        public const string AccountLocked = "Account locked. Please try again later.";
        public const string LoginError = "An error occurred during login. Please try again.";
        public const string RegistrationError = "An error occurred during registration. Please try again.";
    }
}