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

		private void OrderDeleteMenu_Click(object sender, RoutedEventArgs e)
		{


			if (CustomerOrderListView.SelectedItem is not null)
			{
				OrdersEntity selectedOrder = CustomerOrderListView.SelectedItem as OrdersEntity;


				var messageBoxResponse = MessageBox.Show($"You are deleting Order - [{selectedOrder.OrderName}] Are you sure about this?", $"Order with name - {selectedOrder.OrderName} deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (messageBoxResponse == MessageBoxResult.Yes)
				{
					_ordersService.DeleteOrder(selectedOrder);
					CustomerOrders.Remove(selectedOrder);
				}


			}

		}

		private void OrderItemDeleteMenu_Click(object sender, RoutedEventArgs e)
		{
			if (CustomerOrderMaterialListView.SelectedItem is not null)
			{
				OrderItemEntity selectedOrderItem = CustomerOrderMaterialListView.SelectedItem as OrderItemEntity;


				var messageBoxResponse = MessageBox.Show($"You are deleting Material - [{selectedOrderItem.Material.Name}] Are you sure about this?", $"Material with name - {selectedOrderItem.Material.Name} deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (messageBoxResponse == MessageBoxResult.Yes)
				{
					_ordersService.DeleteOrderItem(selectedOrderItem);
					OrderItems.Remove(selectedOrderItem);
				}



			}
		}
	}
}
