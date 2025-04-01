using LifeTrack.Core;
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
            _expenseService = expenseService;
            NewExpense = new Expense { Date = DateTime.Now };
            LoadExpensesCommand = new RelayCommand(async () => await LoadExpenses());
            AddExpenseCommand = new RelayCommand(async () => await AddExpense());
            UpdateExpenseCommand = new RelayCommand(async () => await UpdateExpense(), () => SelectedExpense != null);
            DeleteExpenseCommand = new RelayCommand(async () => await DeleteExpense(), () => SelectedExpense != null);
        }

        public ExpenseViewModel()
        {
        }

        public ObservableCollection<Expense> Expenses
        {
            get => _expenses;
            set
            {
                _expenses = value;
                OnPropertyChanged();
            }
        }

        public Expense SelectedExpense
        {
            get => _selectedExpense;
            set
            {
                _selectedExpense = value;
                OnPropertyChanged();
            }
        }

        public Expense NewExpense
        {
            get => _newExpense;
            set
            {
                _newExpense = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadExpensesCommand { get; }
        public ICommand AddExpenseCommand { get; }
        public ICommand UpdateExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }

        private async Task LoadExpenses()
        {
            var expenses = await _expenseService.GetAllAsync();
            Expenses = new ObservableCollection<Expense>(expenses);
        }

        private async Task AddExpense()
        {
            if (NewExpense.Amount <= 0 || string.IsNullOrWhiteSpace(NewExpense.Description))
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