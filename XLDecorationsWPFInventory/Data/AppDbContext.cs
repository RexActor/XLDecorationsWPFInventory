using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using XLDecorationsWPFInventory.Data.Models;

namespace XLDecorationsWPFInventory.Data;

public class AppDbContext : DbContext
{


	protected override void OnConfiguring(
	   DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			"Data Source=./Data/AppDb.db");
		//optionsBuilder.UseLazyLoadingProxies();

	}



	public DbSet<OrdersEntity> Orders { get; set; }
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<OrderItemEntity> OrderItems { get; set; }
	public DbSet<OrderItemMaterialEntity> OrderItemsMaterials { get; set; }
	public DbSet<MaterialsEntity> Materials { get; set; }
	public DbSet<MaterialTypeEntity> MaterialTypes { get; set; }
	public DbSet<MeasureTypeEntity> MeasureTypes { get; set; }
}
