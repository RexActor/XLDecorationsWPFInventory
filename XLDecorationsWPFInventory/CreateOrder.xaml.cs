using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory
{

	/// <summary>
	/// Interaction logic for CreateOrder.xaml
	/// </summary>
	public partial class CreateOrder : Window
	{

		public static CustomerEntity customer = new CustomerEntity();
		private readonly IMaterialService _materialService = MainWindow._materialService;
		private readonly IOrdersService _orderService = MainWindow._orderService;

		ComboBox MaterialComboBox;
		TextBox MaterialQtyTextBox;
		Label MaterialMeasureLabel;
		Label MaterialAddStatusLabel;

		bool MaterialCanBeAdded = true;
		bool MaterialCanBeRemoved = false;
		int definiedRows;
		List<RowDefinition> rowDefinitions = new List<RowDefinition>();
		List<OrderItemEntity> orderItems = new List<OrderItemEntity>();

		MaterialsEntity materialsEntity;

		public CreateOrder()
		{
			InitializeComponent();

			if (customer is not null)
			{
				UpdateCustomer();
			}
			OrderMaterialRemoveButton.IsEnabled = false;
			OrderMaterialRemoveButton.Visibility = Visibility.Hidden;
		}

		public void UpdateCustomer()
		{
			CustomerAddressLabel.Content = customer.CustomerAddress.ToString();
			customerNameLabel.Content = customer.CustomerName.ToString();
			CustomerEmailLabel.Content = customer.CustomerEmail.ToString();
			CustomerPhoneLabel.Content = customer.CustomerPhone.ToString();
			CustomerOrdersLabel.Content = _orderService.GetOrders(customer).Count;
		}

		private void OrderMaterialsButton_Click(object sender, RoutedEventArgs e)
		{
			if (!MaterialCanBeAdded) { return; }
			GenerateMaterialUI();
		}

		private void GenerateMaterialUI()
		{
			definiedRows = MaterialGrid.RowDefinitions.Count;
			MaterialBorder.Visibility = Visibility.Visible;

			var rowDefinition = new RowDefinition()
			{
				Height = new GridLength(30),
				Name = $"RowDefinition_{definiedRows}"
			};

			MaterialGrid.RowDefinitions.Add(rowDefinition);
			rowDefinitions.Add(rowDefinition);

			MaterialComboBox = new ComboBox();
			var materials = _materialService.GetMaterial().ToList();

			materials.ForEach(material =>
			{
				MaterialComboBox.Items.Add(material);
			});

			MaterialComboBox.IsEditable = true;
			MaterialComboBox.IsReadOnly = true;
			MaterialComboBox.SelectionChanged += MaterialComboBox_SelectionChanged;
			MaterialComboBox.DisplayMemberPath = "Name";
			MaterialComboBox.SelectedValuePath = "Id";
			MaterialComboBox.Text = "Please Select";
			MaterialComboBox.Name = $"MaterialComboBox_{definiedRows}";
			MaterialComboBox.Margin = new Thickness(0, 0, 5, 5);//margin on bottom and right

			MaterialQtyTextBox = new TextBox();
			MaterialQtyTextBox.Name = $"MaterialQuantity_{definiedRows}";
			MaterialQtyTextBox.KeyUp += MaterialQtyTextBox_KeyUp;
			MaterialQtyTextBox.Width = 50;
			MaterialQtyTextBox.Margin = new Thickness(0, 0, 5, 5);//margin on bottom and right

			MaterialMeasureLabel = new Label();
			MaterialMeasureLabel.Content = "";
			MaterialMeasureLabel.Name = $"MaterialMeasureLabel_{definiedRows}";
			MaterialMeasureLabel.Margin = new Thickness(0, 0, 5, 5); //margin on bottom and right

			MaterialAddStatusLabel = new Label();
			MaterialAddStatusLabel.Content = "";
			MaterialAddStatusLabel.Name = $"MaterialAddStatusLabel_{definiedRows}";
			MaterialAddStatusLabel.FontWeight = FontWeights.Bold;
			MaterialAddStatusLabel.Foreground = new SolidColorBrush(Colors.Red);
			MaterialAddStatusLabel.Margin = new Thickness(0, 0, 5, 5); //margin on bottom and right

			Grid.SetRow(MaterialComboBox, definiedRows);
			Grid.SetRow(MaterialQtyTextBox, definiedRows);
			Grid.SetRow(MaterialMeasureLabel, definiedRows);
			Grid.SetRow(MaterialAddStatusLabel, definiedRows);

			Grid.SetColumn(MaterialQtyTextBox, 1);
			Grid.SetColumn(MaterialMeasureLabel, 2);
			Grid.SetColumn(MaterialAddStatusLabel, 3);

			MaterialGrid.Children.Add(MaterialComboBox);
			MaterialGrid.Children.Add(MaterialQtyTextBox);
			MaterialGrid.Children.Add(MaterialMeasureLabel);
			MaterialGrid.Children.Add(MaterialAddStatusLabel);

			MaterialCanBeAdded = false;
			OrderMaterialRemoveButton.IsEnabled = true;
			OrderMaterialRemoveButton.Visibility = Visibility.Visible;
			MaterialCanBeRemoved = true;
			MaterialScrollViewer.ScrollToBottom();
		}

		private void MaterialQtyTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (MaterialQtyTextBox.IsFocused)
			{
				ToolTip tp = new ToolTip();
				tp.Content = "Press Enter";
				tp.ShowsToolTipOnKeyboardFocus = true;
				tp.IsOpen = true;

				MaterialQtyTextBox.ToolTip = tp;
			}

			if (e.Key == Key.Enter)
			{
				var thisTextbox = sender as TextBox;
				if (thisTextbox.Text == string.Empty) { return; }

				OrderItemEntity newOrderItem = new OrderItemEntity
				{
					Material = materialsEntity,
					MaterialId = materialsEntity.Id,
					MaterialQuantity = int.TryParse(thisTextbox.Text, out int quanttiy) ? quanttiy : 0,
				};

				orderItems.Add(newOrderItem);
				MaterialCanBeAdded = true;

				var comboBoxName = thisTextbox.Name;
				var affectedRow = comboBoxName.Split("_");

				foreach (var item in MaterialGrid.Children)
				{
					if (item is Label)
					{
						var label = (Label)item;
						if (label.Name == $"MaterialAddStatusLabel_{affectedRow[1]}")
						{
							label.Content = "Added";
						}
					}
				}
				Keyboard.ClearFocus();
			}

		}

		private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			materialsEntity = MaterialComboBox.SelectedItem as MaterialsEntity;

			if (materialsEntity is null) { return; }

			var comboBoxName = MaterialComboBox.Name;
			var affectedRow = comboBoxName.Split("_");


			foreach (var item in MaterialGrid.Children)
			{
				if (item is Label)
				{
					var label = (Label)item;
					if (label.Name == $"MaterialMeasureLabel_{affectedRow[1]}")
					{
						label.Content = materialsEntity.MaterialMeasureType.Type.ToString();
					}
				}
			}
		}

		private void OrderMaterialRemoveButton_Click(object sender, RoutedEventArgs e)
		{
			if (!MaterialCanBeRemoved) return;

			var usedRows = MaterialGrid.RowDefinitions.Count; //gets amount of rows are defined in grid. as each control is in seperate grid row

			List<UIElement> childrenTORemove = new List<UIElement>();

			foreach (var child in MaterialGrid.Children)
			{
				if (child is Label)
				{
					var labelChild = (Label)child;
					if (labelChild.Name == $"MaterialMeasureLabel_{usedRows - 1}")
					{
						childrenTORemove.Add(labelChild);
					};
					if (labelChild.Name == $"MaterialAddStatusLabel_{usedRows - 1}")
					{
						childrenTORemove.Add(labelChild);
					};

				}
				if (child is ComboBox)
				{
					var comboBoxChild = (ComboBox)child;
					if (comboBoxChild.Name == $"MaterialComboBox_{usedRows - 1}")
					{
						childrenTORemove.Add(comboBoxChild);
					};
				}
				if (child is TextBox)
				{
					var textBoxChild = (TextBox)child;
					if (textBoxChild.Name == $"MaterialQuantity_{usedRows - 1}")
					{
						childrenTORemove.Add(textBoxChild);
					};
				}
			}


			MaterialGrid.RowDefinitions.RemoveAt(MaterialGrid.RowDefinitions.Count - 1);
			rowDefinitions.RemoveAt(rowDefinitions.Count - 1);

			if (rowDefinitions.Count == 0)
			{
				OrderMaterialRemoveButton.IsEnabled = false;
				OrderMaterialRemoveButton.Visibility = Visibility.Hidden;
			}

			foreach (var item in childrenTORemove)
			{
				MaterialGrid.Children.Remove(item);
			}
			orderItems.RemoveAt(orderItems.Count - 1);
			MaterialCanBeAdded = true;
		}


		private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
		{

			if (orderItems.Count == 0) { MessageBox.Show("Please add Materials to order"); return; }
			if (OrderNameTextBox.Text == string.Empty) { return; }
			if (OrderQuantityTextBox.Text == string.Empty) { return; }

			OrdersEntity order = new OrdersEntity
			{
				Customer = customer,
				OrderName = OrderNameTextBox.Text,
				OrderQuantity = int.TryParse(OrderQuantityTextBox.Text, out int quantity) ? quantity : 0,
				CustomerId = customer.Id,
			};

			var createdOrder = _orderService.CreateOrder(order);

			orderItems.ForEach(orderMaterial =>
			{
				orderMaterial.OrderId = order.Id;
				orderMaterial.Orders = order;
				_orderService.CreateOrderItem(orderMaterial);

			});
			if (ViewOrders.CustomerOrders is not null)
			{
				ViewOrders.CustomerOrders.Add(order);
			}
			this.Close();
		}
	}
}
