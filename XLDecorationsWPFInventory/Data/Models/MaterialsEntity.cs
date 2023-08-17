using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLDecorationsWPFInventory.Data.Models
{
	public class MaterialsEntity
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public double Qty { get; set; }
		public double Cost { get; set; }


		public int MaterialTypeId { get; set; }


		[ForeignKey(nameof(MaterialTypeId))]
		public MaterialTypeEntity MaterialType { get; set; }

		public int MaterialMeasureTypeId { get; set; }

		[ForeignKey(nameof(MaterialMeasureTypeId))]
		public MeasureTypeEntity MaterialMeasureType { get; set; }


		public string Size { get; set; }
		public string Comments { get; set; }
	}
}
