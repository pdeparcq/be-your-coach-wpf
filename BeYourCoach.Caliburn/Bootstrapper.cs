using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using BeYourCoach.Application.Training;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using BeYourCoach.Infrastructure.Training;
using Caliburn.Micro.Autofac;
using Deparcq.Common.Application;
using Deparcq.Common.Domain;

namespace BeYourCoach.Caliburn
{
    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>, IAthleteRepository
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<DomainEventPublisher>().As<IDomainEventPublisher>().SingleInstance();
            builder.RegisterType<SchedulingService>().As<ISchedulingService>().SingleInstance();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>().SingleInstance();
            builder.RegisterInstance(this).As<IAthleteRepository>();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>(
                new Dictionary<string, object>
                {
                    {"SizeToContent", SizeToContent.Manual},
                    {"Height", 600},
                    {"Width", 1024},
                }
            );
        }

        public Athlete Get(Guid id)
        {
            return new Athlete(new FullName("Pieter","Deparcq"));
        }
    }
}