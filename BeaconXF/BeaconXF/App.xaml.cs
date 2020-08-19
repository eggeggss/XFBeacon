using Prism;
using Prism.Ioc;
using BeaconXF.ViewModels;
using BeaconXF.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;

namespace BeaconXF
{
    public partial class App
    {
        public IBeacon ibeacon { set; get; }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

           
        }

        protected override void OnStart()
        {
            base.OnStart();

            ibeacon.Start();


        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();


            ibeacon = DependencyService.Get<IBeacon>();
            ibeacon.Initial();
        }
    }
}
