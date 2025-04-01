using LifeTrack.Core.Models;
using LifeTrack.Desktop.Commands;
using LifeTrack.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private readonly ExpenseService _expenseService;
        private ObservableCollection<Expense> _expenses;
        private Expense _selectedExpense;
        private Expense _newExpense;

        public ExpenseViewModel(ExpenseService expenseService)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
            NewExpense = new Expense { Date = DateTime.Now };

            LoadExpensesCommand = new RelayCommand(async () => await LoadExpenses());
            AddExpenseCommand = new RelayCommand(async () => await AddExpense());
            UpdateExpenseCommand = new RelayCommand(async () => await UpdateExpense(), () => SelectedExpense != null);
            DeleteExpenseCommand = new RelayCommand(async () => await DeleteExpense(), () => SelectedExpense != null);
        }

        public void Initialize()
        {
            // Başlangıçta verileri yükle
            LoadExpenses();
        }

        public ObservableCollection<Expense> Expenses
        {
            get => _expenses;
            set => SetProperty(ref _expenses, value);
        }

        public Expense SelectedExpense
        {
            get => _selectedExpense;
            set => SetProperty(ref _selectedExpense, value);
        }

        public Expense NewExpense
        {
            get => _newExpense;
            set => SetProperty(ref _newExpense, value);
        }

        public ICommand LoadExpensesCommand { get; }
        public ICommand AddExpenseCommand { get; }
        public ICommand UpdateExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }

        private async Task LoadExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetAllAsync();
                Expenses = new ObservableCollection<Expense>(expenses);
            }
            catch (Exception ex)
            {
                // Hata işleme burada yapılabilir
                Console.WriteLine($"Giderleri yüklerken hata: {ex.Message}");
            }
        }

        private async Task AddExpense()
        {
            if (NewExpense.Amount <= 0 || string.IsNullOrWhiteSpace(NewExpense.Title))
                return;

            await _expenseService.AddAsync(NewExpense);
            NewExpense = new Expense { Date = DateTime.Now };
            await LoadExpenses();
        }

        private async Task UpdateExpense()
        {
            if (SelectedExpense == null || SelectedExpense.Amount <= 0)
                return;

            await _expenseService.UpdateAsync(SelectedExpense);
            await LoadExpenses();
        }

        private async Task DeleteExpense()
        {
            if (SelectedExpense == null)
                return;

            await _expenseService.DeleteAsync(SelectedExpense.Id);
            await LoadExpenses();
        }
    }
}