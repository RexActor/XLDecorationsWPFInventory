using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data.Services
{
	public interface IMaterialService
	{

		public Task<MaterialsEntity> CreateMaterial(MaterialsEntity entity);
		public Task<MaterialTypeEntity> CreateMaterialType(MaterialTypeEntity entity);
		public ObservableCollection<MaterialsEntity> GetMaterial();
		public ObservableCollection<MaterialTypeEntity> GetMaterialType();
		public bool MaterialExists(string materialName);
		public bool MaterialTypeExists(string materialTypeName);
		public bool MaterialMeasureExists(string measureType);
		public bool DeleteMaterial(MaterialsEntity entity);
		public bool DeleteMaterialType(MaterialTypeEntity entity);

		public Task<MeasureTypeEntity> CreateMeasureType(MeasureTypeEntity entity);
		public ObservableCollection<MeasureTypeEntity> GetMeasureTypes();


	}
}
