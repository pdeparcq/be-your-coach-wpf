using System;

namespace BeYourCoach.Domain.Registration
{
    public interface IAthleteRepository
    {
        Athlete Get(Guid id);
    }
}
