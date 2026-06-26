using System;
using System.IO;
using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the HTTP file response (sends file to HTTP response).
/// </summary>
/// <seealso cref="ControllerResponse" />
public class File : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="File" /> class which sends the specified bytes as a downloadable attachment.
	/// </summary>
	/// <param name="outputFileName">The name of the file.</param>
	/// <param name="contentType">Type of the content.</param>
	/// <param name="data">The data of the file.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public File(string outputFileName, string contentType, byte[] data, int statusCode = 200)
	{
		OutputFileName = outputFileName ?? throw new ArgumentNullException(nameof(outputFileName));
		ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
		Data = data ?? throw new ArgumentNullException(nameof(data));
		StatusCode = statusCode;
		Disposition = ContentDispositionType.Attachment;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="File" /> class which sends the specified bytes with full control over disposition and caching.
	/// </summary>
	/// <param name="data">The data of the file.</param>
	/// <param name="contentType">Type of the content.</param>
	/// <param name="outputFileName">The name of the file (appended to the <c>Content-Disposition</c> header when specified).</param>
	/// <param name="disposition">The <c>Content-Disposition</c> type.</param>
	/// <param name="cacheControl">The <c>Cache-Control</c> header value (not sent when <see langword="null" />).</param>
	/// <param name="eTag">The <c>ETag</c> header value (not sent when <see langword="null" />).</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public File(byte[] data, string contentType, string? outputFileName = null,
		ContentDispositionType disposition = ContentDispositionType.Inline,
		string? cacheControl = null, string? eTag = null, int statusCode = 200)
	{
		Data = data ?? throw new ArgumentNullException(nameof(data));
		ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
		OutputFileName = outputFileName;
		Disposition = disposition;
		CacheControl = cacheControl;
		ETag = eTag;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="File" /> class which streams the specified stream with full control over disposition and caching.
	/// The <paramref name="dataStream" /> is read from its current position and disposed once it has been written to the response.
	/// </summary>
	/// <param name="dataStream">The stream of the file.</param>
	/// <param name="contentType">Type of the content.</param>
	/// <param name="outputFileName">The name of the file (appended to the <c>Content-Disposition</c> header when specified).</param>
	/// <param name="disposition">The <c>Content-Disposition</c> type.</param>
	/// <param name="cacheControl">The <c>Cache-Control</c> header value (not sent when <see langword="null" />).</param>
	/// <param name="eTag">The <c>ETag</c> header value (not sent when <see langword="null" />).</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public File(Stream dataStream, string contentType, string? outputFileName = null,
		ContentDispositionType disposition = ContentDispositionType.Inline,
		string? cacheControl = null, string? eTag = null, int statusCode = 200)
	{
		DataStream = dataStream ?? throw new ArgumentNullException(nameof(dataStream));
		ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
		OutputFileName = outputFileName;
		Disposition = disposition;
		CacheControl = cacheControl;
		ETag = eTag;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the name of the output file.
	/// </summary>
	public string? OutputFileName { get; }

	/// <summary>
	/// Gets the type of the content.
	/// </summary>
	public string ContentType { get; }

	/// <summary>
	/// Gets the data.
	/// </summary>
	public byte[]? Data { get; }

	/// <summary>
	/// Gets the data stream.
	/// </summary>
	public Stream? DataStream { get; }

	/// <summary>
	/// Gets the <c>Content-Disposition</c> type.
	/// </summary>
	public ContentDispositionType Disposition { get; }

	/// <summary>
	/// Gets the <c>Cache-Control</c> header value.
	/// </summary>
	public string? CacheControl { get; }

	/// <summary>
	/// Gets the <c>ETag</c> header value.
	/// </summary>
	public string? ETag { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	public int StatusCode { get; set; }

	/// <summary>
	/// Executes this response asynchronously.
	/// </summary>
	public override async Task<ResponseBehavior> ExecuteAsync()
	{
		Context.Response.StatusCode = StatusCode;
		Context.Response.ContentType = ContentType;
		Context.Response.Headers["Content-Disposition"] = BuildContentDisposition();

		if (CacheControl != null)
			Context.Response.Headers["Cache-Control"] = CacheControl;

		if (ETag != null)
			Context.Response.Headers["ETag"] = ETag;

		if (DataStream != null)
		{
			try
			{
				await ResponseWriter.WriteAsync(Context.Response, DataStream);
			}
			finally
			{
				DataStream.Dispose();
			}
		}
		else
		{
			await ResponseWriter.WriteAsync(Context.Response, Data!);
		}

		return ResponseBehavior.RawOutput;
	}

	private string BuildContentDisposition()
	{
		var type = Disposition == ContentDispositionType.Attachment ? "attachment" : "inline";

		return OutputFileName != null
			? $"{type}; filename=\"{OutputFileName}\""
			: type;
	}
}
