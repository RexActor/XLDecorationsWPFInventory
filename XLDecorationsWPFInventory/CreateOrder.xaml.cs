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

		ComboBox MaterialComboBox;
		TextBox MaterialQtyTextBox;
		Label MaterialMeasureLabel;
		bool MaterialCanBeAdded = true;
		bool MaterialCanBeRemoved = false;
		int definiedRows;
		List<RowDefinition> rowDefinitions = new List<RowDefinition>();
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
			CustomerOrdersLabel.Content = "{COUNT OF ORDERS}";
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
			MaterialQtyTextBox.Width = 50;
			MaterialQtyTextBox.Margin = new Thickness(0, 0, 5, 5);//margin on bottom and right

			MaterialMeasureLabel = new Label();
			MaterialMeasureLabel.Content = "";
			MaterialMeasureLabel.Name = $"MaterialMeasureLabel_{definiedRows}";
			MaterialMeasureLabel.Margin = new Thickness(0, 0, 5, 5); //margin on bottom and right


			Grid.SetRow(MaterialComboBox, definiedRows);
			Grid.SetRow(MaterialQtyTextBox, definiedRows);
			Grid.SetRow(MaterialMeasureLabel, definiedRows);

			Grid.SetColumn(MaterialQtyTextBox, 1);
			Grid.SetColumn(MaterialMeasureLabel, 2);

			MaterialGrid.Children.Add(MaterialComboBox);
			MaterialGrid.Children.Add(MaterialQtyTextBox);
			MaterialGrid.Children.Add(MaterialMeasureLabel);

			MaterialCanBeAdded = false;
			OrderMaterialRemoveButton.IsEnabled = true;
			OrderMaterialRemoveButton.Visibility = Visibility.Visible;
			MaterialCanBeRemoved = true;
			MaterialScrollViewer.ScrollToBottom();
		}

		private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var entity = MaterialComboBox.SelectedItem as MaterialsEntity;	

			if (entity is null) { return; }

			var comboBoxName = MaterialComboBox.Name;
			var affectedRow = comboBoxName.Split("_");


			foreach (var item in MaterialGrid.Children)
			{
				if (item is Label)
				{
					var label = (Label)item;
					if (label.Name == $"MaterialMeasureLabel_{affectedRow[1]}")
					{
						label.Content = entity.MaterialMeasureType.Type.ToString();
					}
				}
			}

			MaterialCanBeAdded = true;

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
						Debug.WriteLine($"{labelChild.Name} has been removed");
					};

				}
				if (child is ComboBox)
				{
					var comboBoxChild = (ComboBox)child;
					if (comboBoxChild.Name == $"MaterialComboBox_{usedRows - 1}")
					{
						childrenTORemove.Add(comboBoxChild);
						Debug.WriteLine($"{comboBoxChild.Name} has been removed");
					};
				}
				if (child is TextBox)
				{
					var textBoxChild = (TextBox)child;
					if (textBoxChild.Name == $"MaterialQuantity_{usedRows - 1}")
					{
						childrenTORemove.Add(textBoxChild);
						Debug.WriteLine($"{textBoxChild.Name} has been removed");
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

			MaterialCanBeAdded = true;
		}
	}
}
