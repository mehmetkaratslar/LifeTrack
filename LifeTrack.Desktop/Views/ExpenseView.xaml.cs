using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace LifeTrack.Desktop.Views
{
    /// <summary>
    /// ExpenseView.xaml için etkileşim mantığı
    /// </summary>
    public partial class ExpenseView : UserControl
    {
        public ExpenseView()
        {
            InitializeComponent();

            // Bu kısmı, eğer ViewModel'i dependency injection ile almıyorsanız veya
            // App.xaml'da resources olarak tanımlamadıysanız kullanabilirsiniz
            /*
            var expenseRepository = ((App)Application.Current).ServiceProvider.GetService<IRepository<Expense>>();
            var categoryService = ((App)Application.Current).ServiceProvider.GetService<ICategoryService>();
            DataContext = new ExpenseViewModel(expenseRepository, categoryService);
            */

            // Alternatif olarak, XAML'de DataContext'i belirtebilirsiniz:
            // <UserControl.DataContext>
            //     <vm:ExpenseViewModel/>
            // </UserControl.DataContext>

            // BooleanToVisibilityConverter değerini eklememiz gerekiyor
            this.Resources["BooleanToVisibilityConverter"] = new System.Windows.Controls.BooleanToVisibilityConverter();
        }

        // Gerekirse, view-specific event handler'lar burada tanımlanabilir
        // Örneğin:
        /*
        private void ExpenseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // View-specific kodlar
        }
        */
    }
}