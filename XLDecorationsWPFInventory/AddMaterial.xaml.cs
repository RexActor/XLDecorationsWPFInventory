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
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;
using XLDecorationsWPFInventory.UserControls;

namespace XLDecorationsWPFInventory;

/// <summary>
/// Interaction logic for AddMaterial.xaml
/// </summary>
public partial class AddMaterial : Window
{

	private readonly IMaterialService _service = MainWindow._materialService;
	private ObservableCollection<MaterialTypeEntity> materialTypes = new ObservableCollection<MaterialTypeEntity>();
	private ObservableCollection<MaterialsEntity> materials = new ObservableCollection<MaterialsEntity>();
	private ObservableCollection<MeasureTypeEntity> measureTypes = new ObservableCollection<MeasureTypeEntity>();


	public AddMaterial()
	{
		InitializeComponent();



		UpdateMaterialTypes();
		UpdateMeasureTypes();
		//MaterialTypeComboBox.ItemsSource= materialTypes;
	}

	private void UpdateMeasureTypes()
	{
		var measureTypes = _service.GetMeasureTypes().ToList();

		measureTypes.ForEach(measureType =>
		{
			if (!MaterialMeasureTypeComboBox.Items.Contains(measureType))
			{
				MaterialMeasureTypeComboBox.Items.Add(measureType);
			}
		});

	}

	private void UpdateMaterialTypes()
	{
		var materialTypes = _service.GetMaterialType().ToList();

		materialTypes.ForEach(materialType =>
		{
			if (!MaterialTypeComboBox.Items.Contains(materialType))
			{
				MaterialTypeComboBox.Items.Add(materialType);
			}
		});


	}



	private void CreateBtn_Click(object sender, RoutedEventArgs e)
	{

		if (MaterialNameTextBox.Text == string.Empty ||
			MaterialCostTextBox.Text == string.Empty ||
			MaterialSizeTextBox.Text == string.Empty ||
			MaterialCommentTextBlock.Text == string.Empty ||
			MaterialQuantityTextBox.Text == string.Empty ||
			MaterialTypeComboBox.SelectedItem is not MaterialTypeEntity ||
			MaterialMeasureTypeComboBox.SelectedItem is not MeasureTypeEntity)
		{
			MessageBox.Show("Please fill all fields"); return;
		}

		if (!double.TryParse(MaterialCostTextBox.Text, out double cost))
		{
			MessageBox.Show("Please use float number format {0.00} for cost");
			return;
		}

		if (!double.TryParse(MaterialQuantityTextBox.Text, out double quantity))
		{
			MessageBox.Show("Please use float number format {0.00} for Quantity ");
			return;
		}



		if (!_service.MaterialExists(MaterialNameTextBox.Text))
		{

			MaterialsEntity material = new MaterialsEntity
			{
				Name = MaterialNameTextBox.Text,
				MaterialTypeId = (int)MaterialTypeComboBox.SelectedValue,
				MaterialMeasureTypeId = (int)MaterialMeasureTypeComboBox.SelectedValue,
				Cost = Convert.ToDouble(MaterialCostTextBox.Text),
				Qty = Convert.ToDouble(MaterialQuantityTextBox.Text),
				Size = MaterialSizeTextBox.Text,
				Comments = MaterialCommentTextBlock.Text




			};



			_service.CreateMaterial(material);
			materials.Add(material);

			MaterialsUC.materialsEntities.Add(material);

			MaterialNameTextBox.Text = string.Empty;
			MaterialCostTextBox.Text = string.Empty;
			MaterialSizeTextBox.Text = string.Empty;
			MaterialCommentTextBlock.Text = string.Empty;
			MaterialQuantityTextBox.Text = string.Empty;
			MaterialTypeComboBox.Text = "Please Select";
			MaterialMeasureTypeComboBox.Text = "Please Select";

		}
		else
		{
			MessageBox.Show("Material already Exists");
		}

	}



	private void MaterialMeasureType_KeyDown(object sender, KeyEventArgs e)
	{
		AddMeasureTypeBtn.Visibility = Visibility.Visible;

		if (e.Key == Key.Return)
		{
			CreateMaterialMeasureType();

		}

	}

	private void CreateMaterialMeasureType()
	{

		if (!_service.MaterialMeasureExists(MaterialMeasureTypeComboBox.Text))
		{
			MeasureTypeEntity measureType = new MeasureTypeEntity
			{
				Type = MaterialMeasureTypeComboBox.Text
			};

			var createdMeasureType = _service.CreateMeasureType(measureType);

			measureTypes.Add(createdMeasureType.Result);
			UpdateMeasureTypes();

			MaterialMeasureTypeComboBox.SelectedValue = createdMeasureType.Result.Id;
			MaterialMeasureTypeComboBox.SelectedItem = createdMeasureType.Result;
			AddMeasureTypeBtn.Visibility = Visibility.Hidden;
		}
		else
		{
			MessageBox.Show($"Measure Type {MaterialMeasureTypeComboBox.Text} already exists!");
		}
	}

	private void AddMeasureTypeBtn_Click(object sender, RoutedEventArgs e)
	{
		CreateMaterialMeasureType();
	}
}
