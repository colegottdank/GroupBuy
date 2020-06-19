using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBuy.Models
{
	public class ShopToken
	{
		[Key]
		public string Token { get; set; }
		public string Shop { get; set; }
	}
}
