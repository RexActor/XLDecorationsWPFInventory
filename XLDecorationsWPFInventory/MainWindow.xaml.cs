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

using XLDecorationsWPFInventory.Data;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public static readonly AppDbContext _context = new AppDbContext();

	public static MainWindow mainWindow;
	public static readonly CustomersService _service = new CustomersService();
	public static readonly MaterialService _materialService = new MaterialService();
	public static readonly OrdersService _orderService = new OrdersService();


	public MainWindow()
	{
		InitializeComponent();
		mainWindow = this;

	}

	private void CustomersBtn_Click(object sender, RoutedEventArgs e)
	{
		this.Width = 800;
		CustomersUserControl.Visibility = Visibility.Visible;
		MaterialsUserControl.Visibility = Visibility.Hidden;
		OrdersUserControl.Visibility = Visibility.Hidden;
	}

	private void MaterialsBtn_Click(object sender, RoutedEventArgs e)
	{
		this.Width = 1500;
		CustomersUserControl.Visibility = Visibility.Hidden;
		MaterialsUserControl.Visibility = Visibility.Visible;
		OrdersUserControl.Visibility = Visibility.Hidden;
	}

	private void OrdersBtn_Click(object sender, RoutedEventArgs e)
	{
		this.Width = 800;
		CustomersUserControl.Visibility = Visibility.Hidden;
		MaterialsUserControl.Visibility = Visibility.Hidden;
		OrdersUserControl.Visibility = Visibility.Visible;

	}
}
