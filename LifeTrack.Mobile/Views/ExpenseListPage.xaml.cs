using LifeTrack.Core.Models;
using LifeTrack.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LifeTrack.Mobile.ViewModels
{
    public partial class ExpenseListViewModel : ObservableObject
    {
        private readonly ExpenseService _expenseService;

        [ObservableProperty]
        private ObservableCollection<Expense> expenses;

        public ExpenseListViewModel()
        {
            // Burada ServiceProvider’dan kendimiz alabiliriz (sonraki adýmda daha iyi yapýlacak)
            _expenseService = App.Current.Handler.MauiContext.Services.GetService<ExpenseService>();

            LoadExpenses();
        }

        private async void LoadExpenses()
        {
            var list = await _expenseService.GetAllAsync();

            Expenses = new ObservableCollection<Expense>(list);
        }
    }
}
