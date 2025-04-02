using LifeTrack.Core.Models;
using LifeTrack.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LifeTrack.Mobile.ViewModels
{
    public partial class AddExpenseViewModel : ObservableObject
    {
        private readonly ExpenseService _expenseService;

        [ObservableProperty] private string title;
        [ObservableProperty] private string amount;
        [ObservableProperty] private DateTime date = DateTime.Today;
        [ObservableProperty] private string description;
        [ObservableProperty] private Category selectedCategory;
        [ObservableProperty] private ObservableCollection<Category> categories;

        public AddExpenseViewModel()
        {
            _expenseService = App.Current.Handler.MauiContext.Services.GetService<ExpenseService>();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var list = await _expenseService.GetAllCategoriesAsync();
            Categories = new ObservableCollection<Category>(list);
        }

        [RelayCommand]
        private async Task Save()
        {
            if (!decimal.TryParse(Amount, out decimal parsedAmount)) return;

            var expense = new Expense
            {
                Title = Title,
                Amount = parsedAmount,
                Date = Date,
                Description = Description,
                CategoryId = SelectedCategory?.Id ?? 0
            };

            await _expenseService.AddExpenseAsync(expense);

            // Gerekirse sayfayı geri döndür:
            await Shell.Current.GoToAsync("..");
        }
    }
}
