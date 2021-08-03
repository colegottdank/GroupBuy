using GroupBuy.Data;
using GroupBuy.Filters;
using GroupBuy.Models;
using GroupBuy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ShopifySharp;
using ShopifySharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpProduct = ShopifySharp.Product;

namespace GroupBuy.Controllers
{
	public class AuthController : Controller
	{
		private Guid Nonce;
		private const string apiKey = "";
		private const string ClientSecret = "";
		private const string Url = "https://423f85de3420.ngrok.io";

		public IActionResult Install(string shop)
		{
			string shopifyUri = $"https://{shop}";
			string redirectUri = $"{Url}/auth/callback";

			var scopes = new List<AuthorizationScope>()
			{
				AuthorizationScope.ReadProducts,
				AuthorizationScope.WriteProducts
			};

			string authUri = AuthorizationService.BuildAuthorizationUrl(scopes, shopifyUri, apiKey, redirectUri).ToString();

			return Redirect(authUri);
		}

		public async Task<IActionResult> CallBack(string shop, string code)
		{
			string accessToken = await AuthorizationService.Authorize(code, shop, apiKey, ClientSecret);

			SaveShopToken(shop, accessToken);

			Initialization(shop, accessToken);

			CreateWebHooks(shop, accessToken);

			return RedirectToAction("Index", "Home");
		}

		private async void Initialization(string shop, string accessToken)
		{
			var service = new ProductService(shop, accessToken);
			var products = await service.ListAsync();

			//Optionally filter the results
			var filter = new GroupBuyFilter()
			{
				ProductType = "groupbuy"
			};

			var groupBuyProducts = await service.ListAsync(filter);
			ProductUtility.StoreProducts(groupBuyProducts.Items.Select(item => item.Id.ToString()));
		}

		private void SaveShopToken(string shop, string token)
		{
			using (var connection = new GroupBuyContext())
			{
				if (connection.ShopToken.Any(c => c.Shop == shop))
				{

				}
				else
				{
					connection.ShopToken.Add(new ShopToken
					{
						Shop = shop,
						Token = token
					});
				}

				connection.SaveChanges();
			}
		}

		private async void CreateWebHooks(string shop, string accessToken)
		{
			var service = new WebhookService(shop, accessToken);

			var webhooks = await service.ListAsync();

			var webhooksList = webhooks.Items.ToList();

			for (int i = 0; i < webhooksList.Count(); i++)
			{
				await service.DeleteAsync((long)webhooksList[i].Id);
			}

			Webhook productsCreate = new Webhook()
			{
				Address = $"{Url}/Home/ProductsCreate",
				CreatedAt = DateTime.Now,
				Format = "json",
				Fields = new List<string>() { "id", "product_type" },
				Topic = "products/create",
			};

			Webhook productsUpdate = new Webhook()
			{
				Address = $"{Url}/Home/ProductsUpdate",
				CreatedAt = DateTime.Now,
				Format = "json",
				Fields = new List<string>() { "id", "product_type" },
				Topic = "products/update",
			};

			Webhook productsDelete = new Webhook()
			{
				Address = $"{Url}/Home/ProductsDelete",
				CreatedAt = DateTime.Now,
				Format = "json",
				Fields = new List<string>() { "id", "product_type" },
				Topic = "products/delete",
			};


			await service.CreateAsync(productsCreate);
			await service.CreateAsync(productsUpdate);
			await service.CreateAsync(productsDelete);
		}
	}
}
