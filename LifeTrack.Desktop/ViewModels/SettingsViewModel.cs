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

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                _isDarkTheme = value;
                OnPropertyChanged();
            }
        }

        public bool EnableNotifications
        {
            get => _enableNotifications;
            set
            {
                _enableNotifications = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
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