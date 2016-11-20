using System.Collections.Generic;
using System.Windows;
using Autofac;
using BeYourCoach.Domain.Training;
using BeYourCoach.Infrastructure.Training;
using Caliburn.Micro.Autofac;

namespace BeYourCoach.Caliburn
{
    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
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
    }
}