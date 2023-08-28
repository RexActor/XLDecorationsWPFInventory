using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Options;

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
	public static MaterialsEntity material;


	public AddMaterial()
	{
		InitializeComponent();



		UpdateMaterialTypes();
		UpdateMeasureTypes();


		if (material is not null)
		{
			UpdateMaterialInfo();

		}

		//MaterialTypeComboBox.ItemsSource= materialTypes;
	}

	private void UpdateMaterialInfo()
	{


		MaterialTypeComboBox.Text = material.MaterialTypeId.ToString();
		MaterialTypeComboBox.SelectedValue = material.MaterialTypeId;
		MaterialMeasureTypeComboBox.Text = material.MaterialMeasureType.Type;
		MaterialMeasureTypeComboBox.SelectedValue = material.MaterialMeasureTypeId;
		MaterialCostTextBox.Text = material.Cost.ToString();
		MaterialCommentTextBlock.Text = material.Comments.ToString();
		MaterialNameTextBox.Text = material.Name.ToString();
		MaterialQuantityTextBox.Text = material.Qty.ToString();
		MaterialSizeTextBox.Text = material.Size.ToString();
		CreateBtn.Content = "Update";
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

		MaterialsEntity material = GenerateMaterialInfo();


		if (CreateBtn.Content == "Update")
		{

			var updateItem = materials.FirstOrDefault(item => item.Id == material.Id);
			var updateItem2 = MaterialsUC.materialsEntities.FirstOrDefault(item => item.Id == material.Id);

			materials.Remove(updateItem);
			MaterialsUC.materialsEntities.Remove(updateItem2);

			material.MaterialMeasureType = MaterialMeasureTypeComboBox.SelectedItem as MeasureTypeEntity;
			material.MaterialType = MaterialTypeComboBox.SelectedItem as MaterialTypeEntity;

			var updatedEntity = _service.UpdateMaterial(material);




			materials.Add(updatedEntity.Result);

			MaterialsUC.materialsEntities.Add(updatedEntity.Result);
			this.Close();

		}
		else
		{
			if (!_service.MaterialExists(MaterialNameTextBox.Text))
			{


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
	}

	private MaterialsEntity GenerateMaterialInfo()
	{
		return new MaterialsEntity
		{

			Id = material is not null ? material.Id : 0,
			Name = MaterialNameTextBox.Text,
			MaterialTypeId = (int)MaterialTypeComboBox.SelectedValue,
			MaterialMeasureTypeId = (int)MaterialMeasureTypeComboBox.SelectedValue,
			MaterialType = material is not null ? material.MaterialType : null,
			MaterialMeasureType = material is not null ? material.MaterialMeasureType : null,
			Cost = Convert.ToDouble(MaterialCostTextBox.Text),
			Qty = Convert.ToDouble(MaterialQuantityTextBox.Text),
			Size = MaterialSizeTextBox.Text,
			Comments = MaterialCommentTextBlock.Text




		};
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
		if (MaterialMeasureTypeComboBox.Text == string.Empty) { return; }
		if (!MaterialMeasureTypeComboBox.Text.Any(item => char.IsLetter(item))) { return; }

		if (!_service.MaterialMeasureExists(MaterialMeasureTypeComboBox.Text))
		{
			MeasureTypeEntity measureType = new MeasureTypeEntity
			{
				Type = MaterialMeasureTypeComboBox.Text.Trim()
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
