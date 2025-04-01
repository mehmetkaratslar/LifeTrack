using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LifeTrack.Core.Models;
using LifeTrack.Desktop.Commands;
using LifeTrack.Services.Repositories;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.ComponentModel;
using System.Collections.Generic;
using LifeTrack.Services;

namespace LifeTrack.Desktop.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private readonly IRepository<Expense> _expenseRepository;
        private readonly ICategoryService _categoryService;
        private Expense _newExpense;
        private Expense _selectedExpense;
        private Category _selectedCategory;
        private Category _filterCategory;
        private DateTime _startDate;
        private DateTime _endDate;
        private ObservableCollection<Expense> _filteredExpenses;
        private decimal _totalExpenses;
        private decimal _monthlyAverage;
        private string _topCategory;
        private bool _isMonthlyChartSelected = true;
        private bool _isCategoryChartSelected;

        // Grafik verileri
        public SeriesCollection MonthlyExpenseSeries { get; set; }
        public SeriesCollection CategoryExpenseSeries { get; set; }
        public List<string> MonthLabels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        // Komutlar
        public ICommand AddExpenseCommand { get; private set; }
        public ICommand DeleteExpenseCommand { get; private set; }
        public ICommand ClearFormCommand { get; private set; }
        public ICommand ApplyFilterCommand { get; private set; }
        public ICommand ExportToPdfCommand { get; private set; }
        public ICommand ExportToExcelCommand { get; private set; }

        public ExpenseViewModel(IRepository<Expense> expenseRepository, ICategoryService categoryService)
        {
            _expenseRepository = expenseRepository;
            _categoryService = categoryService;

            // Command tanımlamaları
            AddExpenseCommand = new RelayCommand(AddExpense, CanAddExpense);
            DeleteExpenseCommand = new RelayCommand(DeleteExpense);
            ClearFormCommand = new RelayCommand(ClearForm);
            ApplyFilterCommand = new RelayCommand(ApplyFilter);
            ExportToPdfCommand = new RelayCommand(ExportToPdf);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);

            // Tarih filtrelerini ayarla
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;

            // Verileri yükle
            NewExpense = new Expense
            {
                Date = DateTime.Now
            };

            LoadCategories();
            ApplyFilter();
            InitializeCharts();
        }

        public ObservableCollection<Category> Categories { get; private set; }

        public Expense NewExpense
        {
            get => _newExpense;
            set
            {
                _newExpense = value;
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

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                if (_newExpense != null && _selectedCategory != null)
                {
                    _newExpense.CategoryId = _selectedCategory.Id;
                    _newExpense.Category = _selectedCategory;
                }
            }
        }

        public Category FilterCategory
        {
            get => _filterCategory;
            set
            {
                _filterCategory = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Expense> FilteredExpenses
        {
            get => _filteredExpenses;
            set
            {
                _filteredExpenses = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalExpenses
        {
            get => _totalExpenses;
            set
            {
                _totalExpenses = value;
                OnPropertyChanged();
            }
        }

        public decimal MonthlyAverage
        {
            get => _monthlyAverage;
            set
            {
                _monthlyAverage = value;
                OnPropertyChanged();
            }
        }

        public string TopCategory
        {
            get => _topCategory;
            set
            {
                _topCategory = value;
                OnPropertyChanged();
            }
        }

        public bool IsMonthlyChartSelected
        {
            get => _isMonthlyChartSelected;
            set
            {
                _isMonthlyChartSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsCategoryChartSelected
        {
            get => _isCategoryChartSelected;
            set
            {
                _isCategoryChartSelected = value;
                OnPropertyChanged();
            }
        }

        private void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(_categoryService.GetAll());
            if (Categories.Count > 0)
            {
                SelectedCategory = Categories.First();
            }
        }

        private void ApplyFilter()
        {
            var expenses = _expenseRepository.GetAll()
                .Where(e => e.Date >= StartDate && e.Date <= EndDate);

            if (FilterCategory != null)
            {
                expenses = expenses.Where(e => e.CategoryId == FilterCategory.Id);
            }

            var expenseList = expenses.ToList();
            FilteredExpenses = new ObservableCollection<Expense>(expenseList);

            // İstatistikleri hesapla
            CalculateStatistics(expenseList);

            // Grafikleri güncelle
            UpdateCharts(expenseList);
        }

        private void CalculateStatistics(List<Expense> expenses)
        {
            TotalExpenses = expenses.Sum(e => e.Amount);

            // Aylık ortalama hesaplama
            var firstDate = expenses.Count > 0 ? expenses.Min(e => e.Date) : DateTime.Now;
            var lastDate = expenses.Count > 0 ? expenses.Max(e => e.Date) : DateTime.Now;
            var monthDiff = ((lastDate.Year - firstDate.Year) * 12) + lastDate.Month - firstDate.Month;
            monthDiff = monthDiff == 0 ? 1 : monthDiff;
            MonthlyAverage = TotalExpenses / monthDiff;

            // En çok harcama yapılan kategori
            if (expenses.Count > 0)
            {
                var topCategoryId = expenses
                    .GroupBy(e => e.CategoryId)
                    .OrderByDescending(g => g.Sum(e => e.Amount))
                    .FirstOrDefault()?.Key;

                if (topCategoryId.HasValue)
                {
                    var category = Categories.FirstOrDefault(c => c.Id == topCategoryId.Value);
                    TopCategory = category != null ? $"{category.Name} ({expenses.Where(e => e.CategoryId == topCategoryId.Value).Sum(e => e.Amount):N2} TL)" : "Bilinmiyor";
                }
                else
                {
                    TopCategory = "Bilinmiyor";
                }
            }
            else
            {
                TopCategory = "Veri yok";
            }
        }

        private void InitializeCharts()
        {
            MonthlyExpenseSeries = new SeriesCollection();
            CategoryExpenseSeries = new SeriesCollection();
            MonthLabels = new List<string>();
            YFormatter = value => $"{value:N0} TL";
        }

        private void UpdateCharts(List<Expense> expenses)
        {
            UpdateMonthlyChart(expenses);
            UpdateCategoryChart(expenses);
        }

        private void UpdateMonthlyChart(List<Expense> expenses)
        {
            MonthlyExpenseSeries.Clear();
            MonthLabels.Clear();

            var monthlyData = expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Total = g.Sum(e => e.Amount)
                })
                .ToList();

            var values = new ChartValues<decimal>();

            foreach (var data in monthlyData)
            {
                MonthLabels.Add(data.Date.ToString("MMM yy"));
                values.Add(data.Total);
            }

            MonthlyExpenseSeries.Add(new ColumnSeries
            {
                Title = "Aylık Giderler",
                Values = new ChartValues<decimal>(values),
                Fill = System.Windows.Media.Brushes.DodgerBlue
            });

            OnPropertyChanged(nameof(MonthlyExpenseSeries));
            OnPropertyChanged(nameof(MonthLabels));
        }

        private void UpdateCategoryChart(List<Expense> expenses)
        {
            CategoryExpenseSeries.Clear();

            var categoryData = expenses
                .GroupBy(e => e.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .ToList();

            foreach (var data in categoryData)
            {
                var category = Categories.FirstOrDefault(c => c.Id == data.CategoryId);
                if (category != null)
                {
                    CategoryExpenseSeries.Add(new PieSeries
                    {
                        Title = category.Name,
                        Values = new ChartValues<decimal> { data.Total },
                        DataLabels = true,
                        LabelPoint = point => $"{category.Name}: {point.Y:N0} TL"
                    });
                }
            }

            OnPropertyChanged(nameof(CategoryExpenseSeries));
        }

        private void AddExpense()
        {
            if (SelectedCategory != null)
            {
                NewExpense.CategoryId = SelectedCategory.Id;
                _expenseRepository.Add(NewExpense);

                // Filtreyi yeniden uygula
                ApplyFilter();

                // Formu temizle
                ClearForm();
            }
        }

        private bool CanAddExpense()
        {
            return !string.IsNullOrWhiteSpace(NewExpense?.Title) &&
                   NewExpense?.Amount > 0 &&
                   SelectedCategory != null;
        }

        private void DeleteExpense(object parameter)
        {
            if (parameter is Expense expense)
            {
                _expenseRepository.Delete(expense.Id);

                // Filtreyi yeniden uygula
                ApplyFilter();
            }
        }

        private void ClearForm()
        {
            NewExpense = new Expense
            {
                Date = DateTime.Now
            };

            if (Categories.Count > 0)
            {
                SelectedCategory = Categories.First();
            }
        }

        private void ExportToPdf()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF dosyaları (*.pdf)|*.pdf",
                Title = "Gider raporunu PDF olarak kaydet"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // PDF dışa aktarma kodunu burada uygulayın
                    // 3. parti bir kütüphane kullanmanız gerekebilir (iTextSharp, PDFsharp, vb.)
                    MessageBox.Show("PDF dışa aktarma özelliği henüz uygulanmadı. İlgili kütüphane entegrasyonu gerekiyor.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PDF dışa aktarma sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExportToExcel()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel dosyaları (*.xlsx)|*.xlsx",
                Title = "Gider raporunu Excel olarak kaydet"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Excel dışa aktarma kodunu burada uygulayın
                    // 3. parti bir kütüphane kullanmanız gerekebilir (EPPlus, ClosedXML, vb.)
                    MessageBox.Show("Excel dışa aktarma özelliği henüz uygulanmadı. İlgili kütüphane entegrasyonu gerekiyor.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Excel dışa aktarma sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}