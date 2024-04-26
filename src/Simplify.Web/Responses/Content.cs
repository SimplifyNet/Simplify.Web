using System;
using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the controller string response.
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
		StringContent = content ?? throw new ArgumentNullException(nameof(content));
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
		StringContent = content ?? throw new ArgumentNullException(nameof(content));
		ContentType = contentType;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the ajax data.
	/// </summary>
	public string StringContent { get; }

	/// <summary>
	/// Gets the type of the content.
	/// </summary>
	public string? ContentType { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	public int StatusCode { get; }

	/// <summary>
	/// Executes this response
	/// </summary>
	public override async Task<ResponseBehavior> ExecuteAsync()
	{
		Context.Response.StatusCode = StatusCode;

		if (ContentType != null)
			Context.Response.ContentType = ContentType;

		await ResponseWriter.WriteAsync(StringContent, Context.Response);

		return ResponseBehavior.RawOutput;
	}
}