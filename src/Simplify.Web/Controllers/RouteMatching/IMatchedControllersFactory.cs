using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllersFactory
{
	IReadOnlyList<IMatchedController> Create(HttpContext context);
}