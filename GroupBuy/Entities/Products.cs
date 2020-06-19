using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBuy.Entities
{
	public class Products
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("product_type")]
		public string ProductType { get; set; }
	}
}
