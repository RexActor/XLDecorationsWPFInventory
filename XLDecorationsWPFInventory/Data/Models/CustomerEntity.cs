using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models;

public class CustomerEntity
{

	[Required]
	public int Id { get; set; }
	[Required]
	public string CustomerName { get; set; }
	[Required]
	public string CustomerAddress { get; set; }

	[Required]
	public string CustomerPhone { get; set; }


	[Required]
	[DataType(DataType.EmailAddress)]
	[EmailAddress]
	public string CustomerEmail { get; set; }
}
