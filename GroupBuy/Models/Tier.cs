using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBuy.Models
{
	public class Tier
	{
		[Key]
		public int TierId { get; set; }
		public decimal Price { get; set; }
		public int MinQuantity { get; set; }
		public int MaxQuantity { get; set; }

		[ForeignKey("ProductId")]
		public Product Product { get; set; }
	}
}
