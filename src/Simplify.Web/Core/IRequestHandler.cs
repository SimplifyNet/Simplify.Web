using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace Simplify.Web.Core;

/// <summary>
/// Represent OWIN HTTP request handler
/// </summary>
public interface IRequestHandler
{
	/// <summary>
	/// Processes the OWIN HTTP request.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	Task ProcessRequest(IDIResolver resolver, IOwinContext context);
}