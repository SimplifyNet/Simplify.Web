using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Represents a Simplify.Web request handling pipeline handler (stage)
/// </summary>
public interface IRequestHandler
{
	/// <summary>
	/// Handles the requests.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="stopProcessing">The action to stop processing.</param>
	Task HandleAsync(HttpContext context, Action stopProcessing);
}