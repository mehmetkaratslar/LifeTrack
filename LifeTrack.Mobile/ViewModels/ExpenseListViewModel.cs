using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeTrack.Core.Models;
using LifeTrack.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace LifeTrack.Mobile.ViewModels
{
    public partial class ExpenseListViewModel : ObservableObject
    {
        private readonly ExpenseService _expenseService;

        public ExpenseListViewModel()
        {
            _expenseService = App.Current.Handler.MauiContext.Services.GetService<ExpenseService>();
            LoadExpensesCommand = new AsyncRelayCommand(LoadExpenses);
        }

        [ObservableProperty]
        private ObservableCollection<Expense> expenses;

        public IAsyncRelayCommand LoadExpensesCommand { get; }

        private async Task LoadExpenses()
        {
            var list = await _expenseService.GetAllWithCategoryAsync(); // kategori bilgileriyle
            Expenses = new ObservableCollection<Expense>(list);
        }
    }
}
