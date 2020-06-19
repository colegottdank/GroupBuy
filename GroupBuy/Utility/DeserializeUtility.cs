using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace GroupBuy.Utility
{
	public static class DeserializeUtility
	{
		public static T DeserializeStreamToType<T>(Stream stream)
		{
			string content;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyToAsync(memoryStream);
				content = Encoding.UTF8.GetString(memoryStream.ToArray());
			}

			return JsonConvert.DeserializeObject<T>(content);
		}
	}
}
