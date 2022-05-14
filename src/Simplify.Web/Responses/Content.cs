using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides controller string response
/// </summary>
public class Content : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Content" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <param name="contentType">Type of the content.</param>
	public Content(string content, int statusCode = 200, string contentType = "text/plain")
	{
		StringContent = content;
		StatusCode = statusCode;
		ContentType = contentType;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Content" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="contentType">>The HTTP response status code.</param>
	/// <param name="statusCode">The status code.</param>
	public Content(string content, string contentType, int statusCode = 200)
	{
		StringContent = content;
		ContentType = contentType;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the ajax data.
	/// </summary>
	/// <value>
	/// The ajax data.
	/// </value>
	public string StringContent { get; }

	/// <summary>
	/// Gets the type of the content.
	/// </summary>
	/// <value>
	/// The type of the content.
	/// </value>
	public string? ContentType { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; }

	/// <summary>
	/// Processes this response
	/// </summary>
	public override async Task<ControllerResponseResult> Process()
	{
		Context.Response.StatusCode = StatusCode;

		if (ContentType != null)
			Context.Response.ContentType = ContentType;

		if (StringContent != null)
			await ResponseWriter.WriteAsync(StringContent, Context.Response);

		return ControllerResponseResult.RawOutput;
	}
}