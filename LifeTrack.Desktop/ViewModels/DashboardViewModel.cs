using System;
using System.Threading.Tasks;

namespace LifeTrack.Desktop.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private decimal _monthlyTotal;
        private int _reminderCount;
        private int _noteCount;

        public DashboardViewModel()
        {
            // Demo verisi
            MonthlyTotal = 1250.00m;
            ReminderCount = 3;
            NoteCount = 5;
        }

        public void Initialize()
        {
            // Verileri yükleyen metodu çağır
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Gerçek verileri yüklemek için burada servis çağrıları yapabilirsiniz
            // Şimdilik demo verileri kullanıyoruz
            MonthlyTotal = 1250.00m;
            ReminderCount = 3;
            NoteCount = 5;
        }

        public decimal MonthlyTotal
        {
            get => _monthlyTotal;
            set => SetProperty(ref _monthlyTotal, value);
        }

        public int ReminderCount
        {
            get => _reminderCount;
            set => SetProperty(ref _reminderCount, value);
        }

        public int NoteCount
        {
            get => _noteCount;
            set => SetProperty(ref _noteCount, value);
        }
    }
}