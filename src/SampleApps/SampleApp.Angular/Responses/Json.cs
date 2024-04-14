using System.Text.Json;
using Simplify.Web;

namespace SampleApp.Angular.Responses;

/// <summary>
/// Provides controller JSON response (send only JSON string to response)
/// This is sample Json response, it is recommended to use https://github.com/SimplifyNet/Simplify.Web.Json package instead
/// </summary>
public class Json : ControllerResponse
{
	private readonly object _objectToConvert;

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	private readonly int _statusCode;

	/// <summary>
	/// Initializes a new instance of the <see cref="Json"/> class.
	/// </summary>
	/// <param name="objectToConvert">The object to convert to JSON.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	public Json(object objectToConvert, int statusCode = 200)
	{
		_objectToConvert = objectToConvert;
		_statusCode = statusCode;
	}

	/// <summary>
	/// Processes this response
	/// </summary>
	/// <returns></returns>
	public override async Task<ControllerResponseResult> ExecuteAsync()
	{
		Context.Response.ContentType = "application/json";
		Context.Response.StatusCode = _statusCode;

		await ResponseWriter.WriteAsync(JsonSerializer.Serialize(_objectToConvert,
			new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			}), Context.Response);

		return ControllerResponseResult.RawOutput;
	}
}