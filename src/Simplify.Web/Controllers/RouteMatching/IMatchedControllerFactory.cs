using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllerFactory
{
	bool CanCreate(IControllerMetadata metaData);

	IMatchedController Create(IControllerMetadata metaData, HttpContext context);
}