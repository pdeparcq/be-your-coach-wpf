using System;
using System.Linq;
using BeYourCoach.Domain.Training;
using Deparcq.Common.Application;
using Deparcq.Common.Json;

namespace BeYourCoach.Infrastructure.Training
{
    public class ScheduleRepository : JsonRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork, "schedules.json")
        {}

        public IQueryable<Schedule> Schedules => Query;
    }
}
