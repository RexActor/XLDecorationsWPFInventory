using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models;

public class OrdersEntity
{
	public int Id { get; set; }
	public string OrderName { get; set; }
	public int OrderQuantity { get; set; }
	public int CustomerId { get; set; }

	public double TotalOrderValue { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public CustomerEntity Customer { get; set; }
}
