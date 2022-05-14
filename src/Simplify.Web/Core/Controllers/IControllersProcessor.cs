using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers;

/// <summary>
/// Represents controllers processor
/// </summary>
public interface IControllersProcessor
{
	/// <summary>
	/// Process controllers for current HTTP request
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	Task<ControllersProcessorResult> ProcessControllers(IDIResolver resolver, HttpContext context);
}