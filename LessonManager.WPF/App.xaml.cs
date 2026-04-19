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
            // Repositories - Singleton to share storage context
            services.AddSingleton<ISubjectRepository, SubjectRepository>();
            services.AddSingleton<ILessonRepository, LessonRepository>();

            // Services
            services.AddSingleton<IDataService, DataService>();

            // ViewModels
            services.AddTransient<SubjectsListViewModel>();
            services.AddTransient<SubjectDetailsViewModel>();
            services.AddTransient<LessonDetailsViewModel>();

            // UI
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}