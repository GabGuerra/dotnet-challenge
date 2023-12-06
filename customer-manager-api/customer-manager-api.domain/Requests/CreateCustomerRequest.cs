using customer_manager_api.domain;
using FluentValidation;

namespace customer_management.Requests
{
    public class CreateCustomerRequest
    {
        public CreateCustomerRequest()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public int? Id { get; set; }


        public class CreateMultipleCustomersRequestValidator : AbstractValidator<IEnumerable<CreateCustomerRequest>>
        {
            public CreateMultipleCustomersRequestValidator()
            {
                RuleForEach(x => x).SetValidator(new CreateCustomerRequestValidator());
            }
        }
        public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
        {
            public CreateCustomerRequestValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage(ErrorMessages.FirstNameRequired);
                RuleFor(x => x.LastName).NotEmpty().WithMessage(ErrorMessages.LastNameRequired);
                RuleFor(x => x.Age).NotEmpty().WithMessage(ErrorMessages.AgeRequired);
                RuleFor(x => x.Age).GreaterThanOrEqualTo(18).WithMessage(ErrorMessages.MustBeOver18);
                RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorMessages.IdRequired);
            }
        }
    }
}
