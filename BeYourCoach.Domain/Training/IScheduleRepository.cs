using System;
using System.Linq;

namespace BeYourCoach.Domain.Training
{
    public interface IScheduleRepository
    {
        void Add(Schedule schedule);

        Schedule Get(Guid id);

        IQueryable<Schedule> Schedules { get; }

        void Update(Schedule schedule);
    }
}
