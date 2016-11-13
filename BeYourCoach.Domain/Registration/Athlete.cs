using System;
using Conditions.Guards;

namespace BeYourCoach.Domain.Registration
{
    public class Athlete
    {
        public Guid Id { get; private set; }
        public FullName FullName { get; private set; }

        public Athlete(FullName fullName)
        {
            Check.If(fullName).IsNotNull();

            Id = Guid.NewGuid();
            FullName = fullName;
        }
    }
}
