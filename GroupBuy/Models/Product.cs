using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroupBuy.Models
{
	public class Product
	{
		[Key]
		public string ProductId { get; set; }
		public bool Completed { get; set; }
		public int NumberSold { get; set; }
		public DateTime? CampaignStart { get; set; }
		public DateTime? CampaignEnd { get; set; }
		public IEnumerable<Tier> Tiers { get; set; }
	}
}
