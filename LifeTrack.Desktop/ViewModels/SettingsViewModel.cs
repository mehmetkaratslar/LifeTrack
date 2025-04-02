using LifeTrack.Desktop.Commands;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        // Mevcut özellikler
        private string _userName;
        private bool _isDarkTheme;
        private bool _enableNotifications;

        // Yeni özellikler
        private string _email;
        private int _fontSizeIndex;
        private int _languageIndex;
        private bool _enableReminderNotifications;
        private bool _enableSoundNotifications;

        // Renk teması için bool özellikler
        private bool _isIndigoTheme = true; // varsayılan
        private bool _isPurpleTheme;
        private bool _isBlueTheme;
        private bool _isGreenTheme;

        // Komutlar
        public ICommand SaveSettingsCommand { get; }
        public ICommand BackupDataCommand { get; }
        public ICommand RestoreDataCommand { get; }
        public ICommand VisitWebsiteCommand { get; }
        public ICommand ShowTermsCommand { get; }
        public ICommand ShowPrivacyCommand { get; }

        // Constructor
        public SettingsViewModel()
        {
            // RelayCommand parametresiz Action bekliyor
            SaveSettingsCommand = new RelayCommand(OnSaveSettings);
            BackupDataCommand = new RelayCommand(OnBackupData);
            RestoreDataCommand = new RelayCommand(OnRestoreData);
            VisitWebsiteCommand = new RelayCommand(OnVisitWebsite);
            ShowTermsCommand = new RelayCommand(OnShowTerms);
            ShowPrivacyCommand = new RelayCommand(OnShowPrivacy);
        }

<<<<<<< HEAD
        // (İsteğe bağlı) Initialize metodu
=======
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        public void Initialize()
        {
            // Ayarlar ekranı açıldığında yapılacak işler
            // (ör. config'den veri yükleme vs.)
        }

        // Komutların çalıştıracağı metotlar
        private void OnSaveSettings()
        {
            // Ayarları kaydetme işlemi
        }

        private void OnBackupData()
        {
            // Veri yedekleme işlemi
        }

        private void OnRestoreData()
        {
            // Yedekten geri yükleme işlemi
        }

        private void OnVisitWebsite()
        {
            // Web sitesine yönlendirme, tarayıcı açma vb.
        }

        private void OnShowTerms()
        {
            // Kullanım Koşulları göster
        }

        private void OnShowPrivacy()
        {
            // Gizlilik Politikası göster
        }

        // Özellikler
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
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

        public string Email
        {
<<<<<<< HEAD
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public int FontSizeIndex
        {
            get => _fontSizeIndex;
            set => SetProperty(ref _fontSizeIndex, value);
        }

        public int LanguageIndex
        {
            get => _languageIndex;
            set => SetProperty(ref _languageIndex, value);
        }

        public bool EnableReminderNotifications
        {
            get => _enableReminderNotifications;
            set => SetProperty(ref _enableReminderNotifications, value);
        }

        public bool EnableSoundNotifications
        {
            get => _enableSoundNotifications;
            set => SetProperty(ref _enableSoundNotifications, value);
        }

        public bool IsIndigoTheme
        {
            get => _isIndigoTheme;
            set
            {
                if (SetProperty(ref _isIndigoTheme, value) && value)
                {
                    IsPurpleTheme = false;
                    IsBlueTheme = false;
                    IsGreenTheme = false;
                }
            }
=======
            get => _userName;
            set => SetProperty(ref _userName, value);
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        }

        public bool IsPurpleTheme
        {
            get => _isPurpleTheme;
            set
            {
                if (SetProperty(ref _isPurpleTheme, value) && value)
                {
                    IsIndigoTheme = false;
                    IsBlueTheme = false;
                    IsGreenTheme = false;
                }
            }
        }

        public bool IsBlueTheme
        {
            get => _isBlueTheme;
            set
            {
                if (SetProperty(ref _isBlueTheme, value) && value)
                {
                    IsIndigoTheme = false;
                    IsPurpleTheme = false;
                    IsGreenTheme = false;
                }
            }
        }

        public bool IsGreenTheme
        {
            get => _isGreenTheme;
            set
            {
                if (SetProperty(ref _isGreenTheme, value) && value)
                {
                    IsIndigoTheme = false;
                    IsPurpleTheme = false;
                    IsBlueTheme = false;
                }
            }
        }
    }
}
