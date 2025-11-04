using Assignment11._3._1_Client.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment11._3._1_Client
{
    public partial class MainWindow : Window
    {
        private readonly ProductService _productService = new();
        private List<Product> _products = new();

        public MainWindow()
        {
            InitializeComponent();
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            _products = await _productService.GetProductsAsync();
            ProductsGrid.ItemsSource = _products;
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product { Name = "New Product", Price = 10, Description = "New Product Description" };
            await _productService.CreateProductAsync(product);
            await LoadProductsAsync();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                selected.Price += 5;
                await _productService.UpdateProductAsync(selected);
                await LoadProductsAsync();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                await _productService.DeleteProductAsync(selected.Id);
                await LoadProductsAsync();
            }
        }
    }
}