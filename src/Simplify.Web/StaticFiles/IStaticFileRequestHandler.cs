using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Represents a static file request handler.
/// </summary>
public interface IStaticFileRequestHandler
{
	/// <summary>
	/// Determines whether this handler can handle the file requested.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns>
	///   <c>true</c> if this handler can handle the file requested; otherwise, <c>false</c>.
	/// </returns>
	bool CanHandle(IStaticFileProcessingContext context);

	/// <summary>
	/// Executes the handler asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="response">The response.</param>
	public Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response);
}