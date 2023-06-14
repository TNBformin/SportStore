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

namespace SportStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.ProductList());
        }

        private void GoCart(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.CartPage());
            BackButton.Visibility = Visibility.Visible;
            CartButton.Visibility = Visibility.Hidden;
            Prices.Visibility = Visibility.Visible;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ProductList());
            BackButton.Visibility = Visibility.Hidden;
            CartButton.Visibility = Visibility.Visible;
            Prices.Visibility = Visibility.Hidden;
        }

        private void PickUpPointChoouse(object sender, SelectionChangedEventArgs e)
        {
            var PickUpPointName = (sender as ComboBox).SelectedItem as string;
            int MainOrderID = App.CurrentOrder;
            var Order = App.Context.Orders.FirstOrDefault(x => x.OrderID == MainOrderID);
            int PickUpPointID = App.Context.PickUpPoints.FirstOrDefault(x => x.PickUpPointName == PickUpPointName).PickUpPointID;
            Order.PickUpPointID = PickUpPointID;
            App.Context.SaveChanges();
        }
    }
}
