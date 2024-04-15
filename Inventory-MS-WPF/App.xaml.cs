using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Inventory_MS_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly AuthenticationStore _authenticationStore;
        public App()
        {
            SplashScreen splashScreen = new SplashScreen(@"./Assets/SplashScreen.jpg");
            splashScreen.Show(true);
            _navigationStore = new NavigationStore();
            _authenticationStore = new AuthenticationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _authenticationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();



            return services.BuildServiceProvider();
        }







    }
}
