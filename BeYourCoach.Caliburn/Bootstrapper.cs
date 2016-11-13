using System.Windows;
using Autofac;
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
            
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
