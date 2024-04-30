using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Represents a Simplify.Web pipeline for handling HTTP requests
/// </summary>
public interface IRequestHandlingPipeline
{
	/// <summary>
	/// Executes the pipeline.
	/// </summary>
	/// <param name="context">The context.</param>
	Task ExecuteAsync(HttpContext context);
}