using System;
using System.Linq;
using BeYourCoach.Common.Json;
using BeYourCoach.Domain.Training;

namespace BeYourCoach.Infrastructure.Training
{
    public class ScheduleRepository : JsonRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository() : base("schedules.json")
        {}

        public IQueryable<Schedule> Schedules => Query;
    }
}
