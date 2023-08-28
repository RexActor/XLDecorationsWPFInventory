using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;

namespace XLDecorationsWPFInventory.UserControls;

/// <summary>
/// Interaction logic for MaterialsUC.xaml
/// </summary>
public partial class MaterialsUC : UserControl
{




	public static ObservableCollection<MaterialsEntity> materialsEntities = new ObservableCollection<MaterialsEntity>();
	public static ObservableCollection<MaterialTypeEntity> materialTypeEntities = new ObservableCollection<MaterialTypeEntity>();

	private readonly IMaterialService _service = MainWindow._materialService;
	AddMaterial addMaterial;

	public MaterialsUC()
	{
		InitializeComponent();
		materialTypeEntities = _service.GetMaterialType();
		MaterialTypeListView.ItemsSource = materialTypeEntities;
		materialsEntities = _service.GetMaterial();
		MaterialListView.ItemsSource = materialsEntities;
	}

	private void CreateMaterialTypeMenu_Click(object sender, RoutedEventArgs e)
	{
		AddMaterialType addMaterialType = new AddMaterialType();
		addMaterialType.Show();

	}

	private void CreateMaterialMenu_Click(object sender, RoutedEventArgs e)
	{
		if (AddMaterial.material is not null)
		{
			AddMaterial.material = null;
		}

		addMaterial = new AddMaterial();
		addMaterial.Show();

	}

	private void DeleteMaterialTypeMenu_Click(object sender, RoutedEventArgs e)
	{
		MaterialTypeEntity materialTypeEntity = MaterialTypeListView.SelectedItem as MaterialTypeEntity;

		MessageBoxResult msgBoxResult = MessageBox.Show($"You are deleting material type {materialTypeEntity.Type}! Are you sure?", "Material Type Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

		if (msgBoxResult == MessageBoxResult.Yes)
		{

			var itemRemoved = _service.DeleteMaterialType(materialTypeEntity);

			if (itemRemoved)
			{
				materialTypeEntities.Remove(materialTypeEntity);
			}
		}

	}

	private async void UpdateMaterialMenu_Click(object sender, RoutedEventArgs e)
	{

		if (MaterialListView.SelectedItem is not null)
		{

			MaterialsEntity material = MaterialListView.SelectedItem as MaterialsEntity;



			AddMaterial.material = await _service.GetMaterialById(material.Id);



			addMaterial = new AddMaterial();
			addMaterial.Show();
		}
	}



	private void MaterialListView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (MaterialListView.SelectedItem is null)
		{
			UpdateMaterialMenu.IsEnabled = false;
		}
		else
		{
			UpdateMaterialMenu.IsEnabled = true;
		}

	}
}
