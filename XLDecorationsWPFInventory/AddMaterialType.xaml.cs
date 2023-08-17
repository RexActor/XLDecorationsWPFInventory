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
using System.Windows.Shapes;

using XLDecorationsWPFInventory.Data.Models;
using XLDecorationsWPFInventory.Data.Services;
using XLDecorationsWPFInventory.UserControls;

namespace XLDecorationsWPFInventory
{
	/// <summary>
	/// Interaction logic for AddMaterialType.xaml
	/// </summary>
	public partial class AddMaterialType : Window
	{

		private readonly IMaterialService _serviceMaterial = MainWindow._materialService;

		public AddMaterialType()
		{
			InitializeComponent();
		}

		private void CreateBtn_Click(object sender, RoutedEventArgs e)
		{

			if(MaterialTypeNameTextBox.Text==string.Empty || MaterialDescriptionTextBox.Text == string.Empty) { MessageBox.Show("Please fill all inputs"); return; }

			if (_serviceMaterial.MaterialTypeExists(MaterialTypeNameTextBox.Text)) { MessageBox.Show("Material Already exists!"); return; }


			MaterialTypeEntity materialType = new MaterialTypeEntity
			{
				Description = MaterialDescriptionTextBox.Text,
				Type = MaterialTypeNameTextBox.Text
			};

			_serviceMaterial.CreateMaterialType(materialType);

			MaterialTypeNameTextBox.Text = string.Empty;
			MaterialDescriptionTextBox.Text = string.Empty;

			MaterialsUC.materialTypeEntities.Add(materialType);






		}
	}
}
