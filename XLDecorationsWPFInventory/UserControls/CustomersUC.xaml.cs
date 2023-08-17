using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory.UserControls
{
	/// <summary>
	/// Interaction logic for CustomersUC.xaml
	/// </summary>
	public partial class CustomersUC : UserControl
	{


		private readonly ICustomersService _service = MainWindow._service;
		ObservableCollection<CustomerEntity> customerEntities = new ObservableCollection<CustomerEntity>();

		public CustomersUC()
		{
			InitializeComponent();

			customerEntities = GetCustomers();
			CustomerListBox.ItemsSource = customerEntities;
		}

		private void CreateCustomerBtn_Click(object sender, RoutedEventArgs e)
		{
			if (_service.CustomerCheck(CustomerNameTextBox.Text)) { MessageBox.Show("Customer Already exist"); return; }


			if (CustomerNameTextBox.Text == string.Empty || CustomerAddressTextBox.Text == string.Empty || CustomerPhoneTextBox.Text == String.Empty || CustomerEmailTextBox.Text == string.Empty)
			{
				MessageBox.Show("You are missing some information");
				return;
			}

			CustomerEntity newCustomer = new CustomerEntity
			{
				CustomerAddress = CustomerAddressTextBox.Text,
				CustomerName = CustomerNameTextBox.Text,
				CustomerPhone = CustomerPhoneTextBox.Text,
				CustomerEmail = CustomerEmailTextBox.Text,


			};



			_service.CreateCustomer(newCustomer);

			customerEntities.Add(newCustomer);

			CustomerAddressTextBox.Text = string.Empty;
			CustomerNameTextBox.Text = string.Empty;
			CustomerPhoneTextBox.Text = string.Empty;
			CustomerEmailTextBox.Text = string.Empty;

		}


		public ObservableCollection<CustomerEntity> GetCustomers()
		{
			customerEntities = _service.GetCustomers();
			return customerEntities;

		}

		private void CustomerOrdersMenu_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RemoveCustomerMenu_Click(object sender, RoutedEventArgs e)
		{

			MessageBoxResult messageBoxResult = MessageBox.Show("You will be permanently deleting customer! Are you sure you want to do it?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Warning);

			CustomerEntity customerEntity = CustomerListBox.SelectedItem as CustomerEntity;


			if (messageBoxResult == MessageBoxResult.Yes)
			{
				_service.DeleteCustomer(customerEntity);
				customerEntities.Remove(customerEntity);
			}


		}

		private void CreateCustomerOrder_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
