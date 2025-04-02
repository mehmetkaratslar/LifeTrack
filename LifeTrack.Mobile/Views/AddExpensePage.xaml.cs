using Microsoft.Maui.Controls;

namespace LifeTrack.Mobile.Views
{
    public partial class ExpenseListPage : ContentPage
    {
        public ExpenseListPage()
        {
            InitializeComponent();
        }

        // Yeni gider ekleme sayfas�na ge�i�
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddExpensePage");
        }
    }
}
