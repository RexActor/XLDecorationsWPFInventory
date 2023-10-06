using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory
{
	/// <summary>
	/// Interaction logic for ViewOrders.xaml
	/// </summary>
	public partial class ViewOrders : Window
	{
		public static CustomerEntity customer = new CustomerEntity();
		private readonly ICustomersService _custoerService = MainWindow._service;
		private readonly IOrdersService _ordersService = MainWindow._orderService;
		public static ObservableCollection<OrdersEntity> CustomerOrders;
		public static ObservableCollection<OrderItemEntity> OrderItems;


		public ViewOrders()
		{
			InitializeComponent();
			if (customer is not null)
			{
				CustomerNameLabel.Content = $"Orders for {customer.CustomerName}";

				CustomerOrders = new ObservableCollection<OrdersEntity>(_ordersService.GetOrders(customer));

				CustomerOrderListView.ItemsSource = CustomerOrders;

			}

		}

		private void CreateOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			if (customer is not null)
			{
				CreateOrder.customer = customer;
			}

			CreateOrder createOrder = new CreateOrder();
			createOrder.Show();
		}

		private void CustomerOrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{


			if (CustomerOrderListView.SelectedItem is not null)
			{
				OrdersEntity selectedOrder = CustomerOrderListView.SelectedItem as OrdersEntity;

				OrderItems = new ObservableCollection<OrderItemEntity>(_ordersService.GetOrderItems(selectedOrder));
				CustomerOrderMaterialListView.ItemsSource = OrderItems;
			}



		}
	}
}
