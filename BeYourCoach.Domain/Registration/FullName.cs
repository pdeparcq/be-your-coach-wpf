using Conditions.Guards;

namespace BeYourCoach.Domain.Registration
{
    public class FullName
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public FullName(string firstName, string lastName)
        {
            Check.If(firstName).IsNotNullOrEmpty();
            Check.If(lastName).IsNotNullOrEmpty();

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
