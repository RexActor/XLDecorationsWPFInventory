using Castle.Core.Resource;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory.UserControls;

/// <summary>
/// Interaction logic for OrdersUC.xaml
/// </summary>
public partial class OrdersUC : UserControl
{

	private readonly IMaterialService _service = MainWindow._materialService;

	private readonly ICustomersService _custoerService = MainWindow._service;
	private readonly IOrdersService _ordersService = MainWindow._orderService;
	public static ObservableCollection<AllOrdersEntity> CustomerOrders { get; set; }
	public static ObservableCollection<AllOrdersEntity> Orders;


	public OrdersUC()
	{
		InitializeComponent();



	}

	private void UpdateOrders()
	{
		if (CustomerOrders is not null)
		{
			if (CustomerOrders.Count > 0)
			{
				CustomerOrders.Clear();
			}
		}
	
		CustomerOrders = new ObservableCollection<AllOrdersEntity>(_ordersService.GetAllOrders());

	

		AllOrdersListview.ItemsSource = CustomerOrders;

		CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AllOrdersListview.ItemsSource);
		PropertyGroupDescription groupDescription = new PropertyGroupDescription("Customer.CustomerName");
		PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("OrderId");
		
		view.GroupDescriptions.Add(groupDescription);
		view.GroupDescriptions.Add(groupDescription2);
	}

	private void OrdersUserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (this.Visibility == Visibility.Visible)
		{
			UpdateOrders();
		}
	}
}
