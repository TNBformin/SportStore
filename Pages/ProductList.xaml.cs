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
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        public ProductList()
        {
            InitializeComponent();
        }
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        Random Random = new Random();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductListView.ItemsSource = App.Context.Products.ToList();

            MainWindow.BackButton.Visibility = Visibility.Hidden;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            var Product = ProductListView.SelectedItem as Entities.Product;
            if(Product.ProductQuantityInStock > 0)
            {
                int OrderID = App.CurrentOrder;
                if(OrderID == -1) 
                { 
                    int OrderNewID = App.Context.Orders.Max(x => x.OrderID) + 1;
                    DateTime Date;
                    if(Product.ProductQuantityInStock > 3)
                    {
                        Date = DateTime.Now.AddDays(3);
                    }
                    else
                    {
                        Date = DateTime.Now.AddDays(6);
                    }
                    Entities.Order NewOrder = new Entities.Order
                    {
                        OrderID = OrderNewID,
                        OrderDeliveryDate = Date,
                        OrderPickUpCode = Random.Next(1000),
                        PickUpPointID = 0,
                        StatusID = 0
                    };
                    App.Context.Orders.Add(NewOrder);
                    App.CurrentOrder = OrderNewID;
                    OrderID = OrderNewID;
                    App.Context.SaveChanges();
                    MainWindow.CartButton.Visibility = Visibility.Visible;
                }

                var OrderProduct = App.Context.OrderProducts.Where(x =>  x.OrderID == OrderID).FirstOrDefault(x => x.ProductID == Product.ProductID);
                if(OrderProduct != null)
                {
                    OrderProduct.ProductQuantityInOrder++;
                    Product.ProductQuantityInStock--;
                    App.Context.SaveChanges();
                }
                else
                {
                    int OrderProductNewID = App.Context.OrderProducts.Max(x => x.OrderProductID) + 1;
                    Entities.OrderProduct NewOrderProduct = new Entities.OrderProduct
                    {
                        OrderProductID = OrderProductNewID,
                        ProductID = Product.ProductID,
                        OrderID = OrderID,
                        ProductQuantityInOrder = 1
                    };
                    App.Context.OrderProducts.Add(NewOrderProduct);
                    Product.ProductQuantityInStock--;
                    App.Context.SaveChanges();
                }

                ProductListView.ItemsSource = App.Context.Products.ToList();
                Product.ProductQuantityInOrder = "1";
            }
            else
            {
                MessageBox.Show("Нет на складе", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
