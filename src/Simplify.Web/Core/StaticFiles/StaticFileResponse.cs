using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.System;
using Simplify.Web.Util;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Provides static file response
	/// </summary>
	/// <seealso cref="IStaticFileResponse" />
	public class StaticFileResponse : IStaticFileResponse
	{
		private readonly HttpResponse _response;
		private readonly IResponseWriter _responseWriter;

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticFileResponse"/> class.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="responseWriter">The response writer.</param>
		public StaticFileResponse(HttpResponse response, IResponseWriter responseWriter)
		{
			_response = response;
			_responseWriter = responseWriter;
		}

		/// <summary>
		/// Sends the not modified static file response.
		/// </summary>
		/// <param name="lastModifiedTime">The last modified time.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public Task SendNotModified(DateTime lastModifiedTime, string fileName)
		{
			SetModificationHeaders(lastModifiedTime);
			SetMimeType(fileName);

			_response.StatusCode = 304;
			return Task.CompletedTask;
		}

		/// <summary>
		/// Sends the fresh static file.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="lastModifiedTime">The last modified time.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public Task SendNew(byte[] data, DateTime lastModifiedTime, string fileName)
		{
			SetModificationHeaders(lastModifiedTime);
			SetMimeType(fileName);

			_response.Headers["Expires"] = new DateTimeOffset(TimeProvider.Current.Now.AddYears(1)).ToString("R");

			return _responseWriter.WriteAsync(data, _response);
		}

		private void SetModificationHeaders(DateTime lastModifiedTime) => _response.Headers.Append("Last-Modified", lastModifiedTime.ToString("r"));

		/// <summary>
		/// Sets the MIME type of response.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		private void SetMimeType(string fileName) => _response.ContentType = MimeTypeAssistant.GetMimeTypeByFilePath(fileName);
	}
}