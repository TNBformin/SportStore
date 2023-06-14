using SportStore.Entities;
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

namespace SportStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
        }
        public int OrderID = App.CurrentOrder;
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CartInit();
        }

        public void CartInit()
        {
            var OrderProducts = App.Context.OrderProducts.Where(x => x.OrderID == OrderID).ToList();
            var Order = App.Context.Orders.FirstOrDefault(x => x.OrderID == OrderID);
            if(OrderProducts.Count > 0)
            {
                List<Entities.Product> Products = new List<Entities.Product>();
                double FullPrice = 0;
                double LastPrice = 0;
                bool SixDays = false;
                foreach (var OrderProduct in OrderProducts)
                {
                    var Product = App.Context.Products.FirstOrDefault(x => x.ProductID == OrderProduct.ProductID);
                    Product.ProductQuantityInOrder = OrderProduct.ProductQuantityInOrder.ToString();
                    if((Product.ProductQuantityInStock > 3) && SixDays == false)
                    {
                        Order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                        MainWindow.DateBlock.Text = "День доставки: " + Order.OrderDeliveryDate.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        Order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                        SixDays = true;
                        MainWindow.DateBlock.Text = "День доставки: " + Order.OrderDeliveryDate.ToString("dd.MM.yyyy");
                    }
                    Products.Add(Product);
                    FullPrice += (double)Product.ProductPrice * OrderProduct.ProductQuantityInOrder;
                    LastPrice += (double)Product.ProductPrice * (1 - (double)Product.ProductDiscount / 100) * OrderProduct.ProductQuantityInOrder;
                }
                MainWindow.FullPrice.Text = "Без скидки: " + FullPrice.ToString() + " руб.";
                MainWindow.DiscountAmount.Text = "Размер скидки: " + (FullPrice - LastPrice).ToString() + " руб.";
                MainWindow.LastPrice.Text = "Конечная сумма: " + LastPrice.ToString() + " руб.";
                CartProductListView.ItemsSource = Products;

                List<string> PickUpPointNames = new List<string>();
                var PickUpPoints = App.Context.PickUpPoints.ToList();
                foreach( var PickUpPoint in PickUpPoints)
                {
                    PickUpPointNames.Add(PickUpPoint.PickUpPointName);
                }
                MainWindow.PickUpPointBox.ItemsSource = PickUpPointNames;
            }
            else
            {
                MainWindow.MainFrame.Navigate(new Pages.ProductList());
                MainWindow.CartButton.Visibility  = Visibility.Hidden;
                MainWindow.Prices.Visibility = Visibility.Hidden;
            }
            
        }

        private void PlusButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int ProductID = int.Parse(button.Tag.ToString());
            var Product = App.Context.Products.FirstOrDefault(x => x.ProductID == ProductID);
            Product.ProductQuantityInStock--;
            var OrderProduct = App.Context.OrderProducts.Where(x => x.OrderID == OrderID).ToList().FirstOrDefault(x => x.ProductID == ProductID);
            OrderProduct.ProductQuantityInOrder++;
            App.Context.SaveChanges();
            CartInit();
        }

        private void MinusButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int ProductID = int.Parse(button.Tag.ToString());
            var Product = App.Context.Products.FirstOrDefault(x => x.ProductID == ProductID);
            Product.ProductQuantityInStock++;
            var OrderProduct = App.Context.OrderProducts.Where(x => x.OrderID == OrderID).ToList().FirstOrDefault(x => x.ProductID == ProductID);
            OrderProduct.ProductQuantityInOrder--;
            if(OrderProduct.ProductQuantityInOrder == 0)
            {
                MessageBoxResult Result = MessageBox.Show("Вы уверены что хотите удалить товар из корзины?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult. Yes)
                {
                    App.Context.OrderProducts.Remove(OrderProduct);
                    App.Context.SaveChanges();
                }
                else
                {
                    Product.ProductQuantityInStock--;
                    OrderProduct.ProductQuantityInOrder++;
                    App.Context.SaveChanges();
                }
            }
            CartInit();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы уверены что хотите удалить товар из корзины?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (Result == MessageBoxResult.Yes)
            {
                var Product = CartProductListView.SelectedItem as Entities.Product;
                var OrderProduct = App.Context.OrderProducts.Where(x => x.OrderID == OrderID).ToList().FirstOrDefault(x => x.ProductID == Product.ProductID);
                Product.ProductQuantityInStock += OrderProduct.ProductQuantityInOrder;
                App.Context.OrderProducts.Remove(OrderProduct);
                App.Context.SaveChanges();
                CartInit();
            }
        }
    }
}
