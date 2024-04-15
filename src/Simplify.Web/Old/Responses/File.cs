﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Responses;

/// <summary>
/// Provides Http file response (send file to response).
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="File" /> class.
/// </remarks>
/// <param name="outputFileName">The name of the file.</param>
/// <param name="contentType">Type of the content.</param>
/// <param name="data">The data of the file.</param>
/// <param name="statusCode">The HTTP response status code.</param>
/// <exception cref="ArgumentNullException">
/// </exception>
public class File(string outputFileName, string contentType, byte[] data, int statusCode = 200) : ControllerResponse
{

	/// <summary>
	/// Gets the name of the output file.
	/// </summary>
	/// <value>
	/// The name of the output file.
	/// </value>
	public string OutputFileName { get; } = outputFileName ?? throw new ArgumentNullException(nameof(outputFileName));

	/// <summary>
	/// Gets the type of the content.
	/// </summary>
	/// <value>
	/// The type of the content.
	/// </value>
	public string ContentType { get; } = contentType ?? throw new ArgumentNullException(nameof(contentType));

	/// <summary>
	/// Gets the data.
	/// </summary>
	/// <value>
	/// The data.
	/// </value>
	public byte[] Data { get; } = data ?? throw new ArgumentNullException(nameof(data));

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; set; } = statusCode;

	/// <summary>
	/// Processes this response.
	/// </summary>
	public override async Task<ControllerResponseResult> ExecuteAsync()
	{
		Context.Response.StatusCode = StatusCode;

		Context.Response.Headers.Append("Content-Disposition", "attachment; filename=\"" + OutputFileName + "\"");
		Context.Response.ContentType = ContentType;

		await Context.Response.Body.WriteAsync(Data, 0, Data.Length);

		return ControllerResponseResult.RawOutput;
	}
}