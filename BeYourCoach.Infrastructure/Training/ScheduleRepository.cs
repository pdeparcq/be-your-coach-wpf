using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeYourCoach.Common.Json;
using BeYourCoach.Domain.Training;

namespace BeYourCoach.Infrastructure.Training
{
    public class ScheduleRepository : IScheduleRepository
    {
        private static readonly string FileName = "schedules.json";
        private List<Schedule> _schedules;

        public ScheduleRepository()
        {
            _schedules = new List<Schedule>();
            ReadFromFile();
        }

        private void ReadFromFile()
        {
            if (File.Exists(FileName))
            {
                using (var sr = File.OpenText(FileName))
                {
                    _schedules = sr.Deserialize<List<Schedule>>();
                }
            }
        }

        private void WriteToFile()
        {
            using (var sw = File.CreateText(FileName))
            {
                _schedules.Serialize(sw);
            }
        }

        public void Add(Schedule schedule)
        {
            Update(schedule);
        }

        public Schedule Get(Guid id)
        {
            return _schedules.Single(s => s.Id == id);
        }

        public IQueryable<Schedule> Schedules => _schedules.AsQueryable();

        public void Update(Schedule schedule)
        {
            _schedules.RemoveAll(s => s.Id == schedule.Id);
            _schedules.Add(schedule);
            WriteToFile();
        }
    }
}
