using LifeTrack.Core.Models;
using LifeTrack.Desktop.Commands;
using LifeTrack.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class ReminderViewModel : ViewModelBase
    {
        private readonly ReminderService _reminderService;
        private ObservableCollection<Reminder> _reminders;
        private Reminder _selectedReminder;
        private Reminder _newReminder;

        public ReminderViewModel(ReminderService reminderService)
        {
            _reminderService = reminderService ?? throw new ArgumentNullException(nameof(reminderService));
            NewReminder = new Reminder { CreatedAt = DateTime.Now, DueDate = DateTime.Now.AddDays(1) };

            LoadRemindersCommand = new RelayCommand(async () => await LoadReminders());
            AddReminderCommand = new RelayCommand(async () => await AddReminder());
            UpdateReminderCommand = new RelayCommand(async () => await UpdateReminder(), () => SelectedReminder != null);
            DeleteReminderCommand = new RelayCommand(async () => await DeleteReminder(), () => SelectedReminder != null);
            CompleteReminderCommand = new RelayCommand(async () => await CompleteReminder(), () => SelectedReminder != null);
        }

        public void Initialize()
        {
            // Başlangıçta verileri yükle
            LoadReminders();
        }

        public ObservableCollection<Reminder> Reminders
        {
            get => _reminders;
            set => SetProperty(ref _reminders, value);
        }

        public Reminder SelectedReminder
        {
            get => _selectedReminder;
            set => SetProperty(ref _selectedReminder, value);
        }

        public Reminder NewReminder
        {
            get => _newReminder;
            set => SetProperty(ref _newReminder, value);
        }

        public ICommand LoadRemindersCommand { get; }
        public ICommand AddReminderCommand { get; }
        public ICommand UpdateReminderCommand { get; }
        public ICommand DeleteReminderCommand { get; }
        public ICommand CompleteReminderCommand { get; }

        private async Task LoadReminders()
        {
            try
            {
                var reminders = await _reminderService.GetAllAsync();
                Reminders = new ObservableCollection<Reminder>(reminders);
            }
            catch (Exception ex)
            {
                // Hata işleme burada yapılabilir
                Console.WriteLine($"Hatırlatıcıları yüklerken hata: {ex.Message}");
            }
        }

        private async Task AddReminder()
        {
            if (string.IsNullOrWhiteSpace(NewReminder.Title))
                return;

            await _reminderService.AddAsync(NewReminder);
            NewReminder = new Reminder { CreatedAt = DateTime.Now, DueDate = DateTime.Now.AddDays(1) };
            await LoadReminders();
        }

        private async Task UpdateReminder()
        {
            if (SelectedReminder == null || string.IsNullOrWhiteSpace(SelectedReminder.Title))
                return;

            await _reminderService.UpdateAsync(SelectedReminder);
            await LoadReminders();
        }

        private async Task DeleteReminder()
        {
            if (SelectedReminder == null)
                return;

            await _reminderService.DeleteAsync(SelectedReminder.Id);
            await LoadReminders();
        }

        private async Task CompleteReminder()
        {
            if (SelectedReminder == null)
                return;

            SelectedReminder.IsCompleted = true;
            SelectedReminder.CompletedAt = DateTime.Now;
            await _reminderService.UpdateAsync(SelectedReminder);
            await LoadReminders();
        }
    }
}