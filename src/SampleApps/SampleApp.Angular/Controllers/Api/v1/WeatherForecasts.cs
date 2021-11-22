using System;
using System.Linq;
using SampleApp.Angular.Responses;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Angular.Controllers
{
	[Get("api/v1/WeatherForecasts")]
	public class WeatherForecasts : Controller
	{
		private static readonly string[] Summaries =
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public override ControllerResponse Invoke()
		{
			var rng = new Random();
			var items = Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			});

			return new Json(items);
		}

		public class WeatherForecast
		{
			public string DateFormatted { get; set; }
			public int TemperatureC { get; set; }
			public string Summary { get; set; }

			public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
		}
	}
}