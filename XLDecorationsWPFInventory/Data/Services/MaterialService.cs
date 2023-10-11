using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data.Services;

public class MaterialService : IMaterialService
{
	private readonly AppDbContext _context;

	ObservableCollection<MaterialsEntity> materialsEntities = new ObservableCollection<MaterialsEntity>();
	ObservableCollection<MaterialTypeEntity> materialTypeEntities = new ObservableCollection<MaterialTypeEntity>();
	ObservableCollection<MeasureTypeEntity> measureTypeEntities = new ObservableCollection<MeasureTypeEntity>();


	public MaterialService()
	{
		_context = MainWindow._context;
	}

	public async Task<MaterialsEntity> CreateMaterial(MaterialsEntity entity)
	{

		await _context.Materials.AddAsync(entity);

		await _context.SaveChangesAsync();
		return entity;
	}
	public async Task<MaterialsEntity> UpdateMaterial(MaterialsEntity entity)
	{
		var materialToUpdate = await _context.Materials.Where(item => item.Id == entity.Id).Include(item => item.MaterialType).Include(item => item.MaterialMeasureType).FirstOrDefaultAsync();

		if (materialToUpdate is not null)
		{

			materialToUpdate.Qty = entity.Qty;
			materialToUpdate.Size = entity.Size;
			materialToUpdate.MaterialMeasureTypeId = entity.MaterialMeasureTypeId;
			materialToUpdate.MaterialTypeId = entity.MaterialTypeId;
			materialToUpdate.Comments = entity.Comments;
			materialToUpdate.Name = entity.Name;

			_context.Materials.Update(materialToUpdate);

			await _context.SaveChangesAsync();
		}


		return entity;


	}
	public async Task<MaterialTypeEntity> CreateMaterialType(MaterialTypeEntity entity)
	{
		await _context.MaterialTypes.AddAsync(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<MeasureTypeEntity> CreateMeasureType(MeasureTypeEntity entity)
	{
		await _context.MeasureTypes.AddAsync(entity);
		await _context.SaveChangesAsync();



		//return JsonConvert.DeserializeObject<MeasureTypeEntity>(await response);
		return entity;
	}

	public bool DeleteMaterial(MaterialsEntity entity)
	{
		throw new NotImplementedException();
	}

	public bool DeleteMaterialType(MaterialTypeEntity entity)
	{
		var deleteMaterialType = _context.MaterialTypes.Where(item => item.Id == entity.Id).FirstOrDefault();

		if (deleteMaterialType is not null)
		{
			_context.MaterialTypes.Remove(deleteMaterialType);
			_context.SaveChanges();
			return true;
		}
		return false;



	}

	public ObservableCollection<MaterialsEntity> GetMaterial()
	{
		_context.Materials.Include(item => item.MaterialMeasureType).Include(item => item.MaterialType).ForEachAsync(item =>
		{
			if (!materialsEntities.Contains(item)) { materialsEntities.Add(item); }
		});
		return materialsEntities;
	}

	public Task<MaterialsEntity> GetMaterialById(int id)
	{
		return _context.Materials.Include(item => item.MaterialMeasureType).Include(item => item.MaterialType).FirstOrDefaultAsync(item => item.Id == id);
	}


	public ObservableCollection<MaterialTypeEntity> GetMaterialType()
	{


		_context.MaterialTypes.ForEachAsync(item =>
		{
			if (!materialTypeEntities.Contains(item)) { materialTypeEntities.Add(item); }
		});
		return materialTypeEntities;
	}

	public ObservableCollection<MeasureTypeEntity> GetMeasureTypes()
	{
		_context.MeasureTypes.ForEachAsync(item =>
		{
			if (!measureTypeEntities.Contains(item)) { measureTypeEntities.Add(item); }
		});
		return measureTypeEntities;
	}

	public bool MaterialExists(string materialName)
	{
		return _context.Materials.Where(item => item.Name.ToLower() == materialName.ToLower()).Any();
	}

	public bool MaterialMeasureExists(string measureType)
	{
		return _context.MeasureTypes.Where(item => item.Type.ToLower() == measureType.ToLower()).Any();
	}

	public bool MaterialTypeExists(string materialTypeName)
	{
		return _context.MaterialTypes.Where(item => item.Type.ToLower() == materialTypeName.ToLower()).Any();
	}

	public async Task<MaterialsEntity> UpdateMaterialAfterOrder(int entityId, int quantity)
	{
		var materialToUpdate = await _context.Materials.Where(item => item.Id == entityId).Include(item => item.MaterialType).Include(item => item.MaterialMeasureType).FirstOrDefaultAsync();

		if (materialToUpdate is not null)
		{
			double newQuantity = materialToUpdate.Qty - Convert.ToDouble(quantity);

			materialToUpdate.Qty = newQuantity;


			_context.Materials.Update(materialToUpdate);

			await _context.SaveChangesAsync();
		}


		return materialToUpdate;
	}
}
