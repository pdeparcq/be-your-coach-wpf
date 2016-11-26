﻿using System;
using System.Linq;
using BeYourCoach.Common.Json;
using BeYourCoach.Domain.Training;
using Common.Domain;

namespace BeYourCoach.Infrastructure.Training
{
    public class ScheduleRepository : JsonRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork, "schedules.json")
        {}

        public IQueryable<Schedule> Schedules => Query;
    }
}
