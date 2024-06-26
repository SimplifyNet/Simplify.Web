﻿using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the controller response with exact status code and optional string data (send only specified string to response or empty body).
/// </summary>
/// <seealso cref="ControllerResponse" />
/// <remarks>
/// Initializes a new instance of the <see cref="StatusCode" /> class.
/// </remarks>
/// <param name="statusCode">The HTTP response status code.</param>
/// <param name="responseData">The response data.</param>
/// <param name="contentType">Type of the content.</param>
public class StatusCode(int statusCode, string? responseData = null, string? contentType = "text/plain") : ControllerResponse
{
	/// <summary>
	/// Gets the response data.
	/// </summary>
	/// <value>
	/// The response data.
	/// </value>
	public string? ResponseData { get; } = responseData;

	/// <summary>
	/// Gets the type of the content.
	/// </summary>
	public string? ContentType { get; } = contentType;

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	public int Code { get; } = statusCode;

	/// <summary>
	/// Executes this response asynchronously.
	/// </summary>
	public override async Task<ResponseBehavior> ExecuteAsync()
	{
		Context.Response.StatusCode = Code;

		if (ContentType != null)
			Context.Response.ContentType = ContentType;

		if (ResponseData != null)
			await ResponseWriter.WriteAsync(Context.Response, ResponseData);

		return ResponseBehavior.RawOutput;
	}
}