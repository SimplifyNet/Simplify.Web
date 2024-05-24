using System.Text.Json;
using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides controller JSON response using System.Text.Json (send only JSON string to response)
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Json"/> class.
/// </remarks>
/// <param name="objectToConvert">The object to convert to JSON.</param>
/// <param name="statusCode">The HTTP response status code.</param>
public class Json(object objectToConvert, int statusCode = 200) : ControllerResponse
{

	/// <summary>
	/// Gets or sets the default JsonSerializerOptions
	/// </summary>
	/// <value>
	/// The default JsonSerializerOptions.
	/// </value>
	public static JsonSerializerOptions? DefaultOptions { get; set; } = null;

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	private readonly int _statusCode = statusCode;

	/// <summary>
	/// Processes this response
	/// </summary>
	public override async Task<ResponseBehavior> ExecuteAsync()
	{
		Context.Response.ContentType = "application/json";
		Context.Response.StatusCode = _statusCode;

		await ResponseWriter.WriteAsync(Context.Response, JsonSerializer.Serialize(objectToConvert, DefaultOptions));

		return ResponseBehavior.RawOutput;
	}
}