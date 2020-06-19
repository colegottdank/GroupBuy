using Newtonsoft.Json;
using ShopifySharp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBuy.Filters
{
	public class GroupBuyFilter : ProductListFilter
	{
		[JsonProperty("ProductType")]
		public string ProductType { get; set; }
	}
}
