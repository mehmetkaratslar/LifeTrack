namespace LifeTrack.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Uygulama giriş noktası Shell üzerinden
            MainPage = new AppShell();
        }
    }


}
