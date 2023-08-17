using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models;

public class OrderItemMaterialEntity
{



	public int Id { get; set; }
	public int OrderItemId { get; set; }

	[ForeignKey(nameof(OrderItemId))]
	public OrderItemEntity OrderItem { get; set; }
	public int Qty { get; set; }
}
