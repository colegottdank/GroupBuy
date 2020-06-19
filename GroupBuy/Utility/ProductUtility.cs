using GroupBuy.Data;
using GroupBuy.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GroupBuy.Services
{
	public static class ProductUtility
	{
		public static void StoreProduct(string id)
		{
			using (var connection = new GroupBuyContext())
			{
				if (connection.Product.Any(c => c.ProductId == id))
				{
					// Todo: update product
				}
				else
				{
					connection.Product.Add(new Product
					{
						ProductId = id
					});
				}

				connection.SaveChanges();
			}
		}

		public static void StoreProducts(IEnumerable<string> ids)
		{
			foreach(var id in ids)
			{
				StoreProduct(id);
			}
		}
	}
}
