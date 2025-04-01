using LifeTrack.Desktop.Commands;
using System;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private bool _isDarkTheme;
        private bool _enableNotifications;
        private string _userName;

        public SettingsViewModel()
        {
            // Varsayılan ayarlar
            _isDarkTheme = false;
            _enableNotifications = true;
            _userName = "Kullanıcı";

            SaveSettingsCommand = new RelayCommand(SaveSettings);
        }

        public void Initialize()
        {
            // Ayarları yükle (örnek uygulama için bir şey yapmıyoruz)
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set => SetProperty(ref _isDarkTheme, value);
        }

        public bool EnableNotifications
        {
            get => _enableNotifications;
            set => SetProperty(ref _enableNotifications, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public ICommand SaveSettingsCommand { get; }

        private void SaveSettings()
        {
            // Burada ayarları kaydedebilirsiniz, örn. Properties.Settings'e
            // Şimdilik sadece bildirim gösterelim
            Console.WriteLine("Ayarlar kaydedildi!");
        }
    }
}