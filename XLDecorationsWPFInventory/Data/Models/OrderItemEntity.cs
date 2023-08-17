using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models
{
	public class OrderItemEntity
	{
		public int Id { get; set; }


		public int OrderId { get; set; }

		[ForeignKey(nameof(OrderId))]
		public OrdersEntity Orders { get; set; }

		

	}
}
