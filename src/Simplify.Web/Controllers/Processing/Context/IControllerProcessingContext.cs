using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Processing.Context;

public interface IControllerProcessingContext
{
	public IMatchedController Controller { get; }

	public SecurityStatus SecurityStatus { get; }

	public HttpContext HttpContext { get; }
}