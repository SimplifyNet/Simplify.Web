using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Represents a Simplify.Web request handling pipeline handler (stage)
/// </summary>
public interface IRequestHandler
{
	/// <summary>
	/// Handle the request.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	Task HandleAsync(HttpContext context, RequestHandlerAsync next);
}