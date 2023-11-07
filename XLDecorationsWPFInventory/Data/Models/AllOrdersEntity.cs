using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models
{
	public class AllOrdersEntity
	{



		public string? OrderName { get; set; }
		public int OrderQuantity { get; set; }
		public int CustomerId { get; set; }

		public double TotalOrderValue { get; set; }
		public int OrderId { get; set; }
		public OrdersEntity Orders { get; set; }

		public CustomerEntity Customer { get; set; }

		public List<MaterialsEntity> Materials { get; set; }

		public OrderItemEntity OrderItems { get; set; }


	}
}
