using SampleApp.Angular.ViewModels;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Angular.Controllers.V1;

[Get("api/v1/weather-forecasts")]
public class WeatherForecastsController : Controller
{
	private static readonly string[] Summaries =
	[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	public override ControllerResponse Invoke() =>
		Json(Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateTime.Now.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}));
}