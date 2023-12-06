namespace customer_manager_api.domain
{
    public static class ValidationMessages
    {
        public const string FirstNameRequired = "First name is required";
        public const string LastNameRequired = "Last name is required";
        public const string AgeRequired = "Age is required";
        public const string IdRequired = "Id is required";
        public const string MustBeOver18 = "Must be over 18 years of age";
        public const string IdMustBeUnique = "Id must be unique";
    }
}
