using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers
{
	/// <summary>
	/// Represent controllers HTTP request handler
	/// </summary>
	public interface IControllersRequestHandler
	{
		/// <summary>
		/// Processes the HTTP request for controllers.
		/// </summary>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		Task<RequestHandlingStatus> ProcessRequest(IDIResolver resolver, HttpContext context);
	}
}