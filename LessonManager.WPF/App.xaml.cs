using LessonManager.Repositories;
using LessonManager.Repositories.Interfaces;
using LessonManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LessonManager.ViewModels;

namespace LessonManager.WPF
{

    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISubjectRepository, MockSubjectRepository>();
            services.AddSingleton<ILessonRepository, MockLessonRepository>();

            services.AddSingleton<IDataService, DataService>();

            services.AddTransient<SubjectsListViewModel>();
            services.AddTransient<SubjectDetailsViewModel>();
            services.AddTransient<LessonDetailsViewModel>();

            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}