using GroupBuy.Entities;
using GroupBuy.Models;
using GroupBuy.Services;
using GroupBuy.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GroupBuy.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public void ProductsCreate()
		{
			var dto = DeserializeUtility.DeserializeStreamToType<Products>(Request.Body);

			if (dto.ProductType == "groupbuy")
			{
				ProductUtility.StoreProduct(dto.Id);
			}
		}

		public IActionResult ProductsUpdate()
		{
			string content;

			using (MemoryStream ms = new MemoryStream())
			{
				Request.Body.CopyToAsync(ms);
				content = Encoding.UTF8.GetString(ms.ToArray());
			}

			dynamic dto = JsonConvert.DeserializeObject<dynamic>(content);

			return View();
		}

		public IActionResult ProductsDelete()
		{
			string content;

			using (MemoryStream ms = new MemoryStream())
			{
				Request.Body.CopyToAsync(ms);
				content = Encoding.UTF8.GetString(ms.ToArray());
			}

			dynamic dto = JsonConvert.DeserializeObject<dynamic>(content);

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
