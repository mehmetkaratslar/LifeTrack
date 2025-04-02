using Microsoft.Maui.Controls;

namespace LifeTrack.Mobile.Views
{
    public partial class ExpenseListPage : ContentPage
    {
        public ExpenseListPage()
        {
            InitializeComponent();
        }

        // Yeni gider ekleme sayfasýna geçiþ
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddExpensePage");
        }
    }
}
