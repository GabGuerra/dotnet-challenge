using customer_management.Requests;

namespace customer_manager_api_consumer
{
    public static class CreateCustomerGenerator
    {
        public static readonly string[] FirstNames =
        {
            "Leia ",
            "Sadie",
            "Jose",
            "Sara",
            "Frank",
            "Dewey",
            "Tomas",
            "Joel",
            "Lukas",
            "Carlos",
        };

        public static readonly string[] LastNames =
        {
            "Liberty",
            "Ray",
            "Harrison",
            "Ronan",
            "Drew",
            "Powell",
            "Larsen",
            "Chan",
            "Anderson",
            "Lane",
        };

        public static CreateCustomerRequest Generate(int id)
        {
            return new CreateCustomerRequest
            {
                FirstName = FirstNames[new Random().Next(0, FirstNames.Length)],
                LastName = LastNames[new Random().Next(0, LastNames.Length)],
                Age = new Random().Next(10, 90),
                Id = id
            };
        }
    }
}
