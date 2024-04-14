using System.Collections.Generic;
using System.Security.Claims;
using Simplify.Web.Old.Meta;
using Simplify.Web.Old.Routing;

namespace Simplify.Web.Old.Core.Controllers;

/// <summary>
/// Represent controllers agent.
/// </summary>
public interface IControllersAgent
{
	/// <summary>
	/// Gets the standard controllers meta data.
	/// </summary>
	/// <returns></returns>
	IList<IControllerMetaData> GetStandardControllersMetaData();

	/// <summary>
	/// Matches the controller route.
	/// </summary>
	/// <param name="controllerMetaData">The controller meta data.</param>
	/// <param name="sourceRoute">The source route.</param>
	/// <param name="httpMethod">The HTTP method.</param>
	/// <returns></returns>
	IRouteMatchResult? MatchControllerRoute(IControllerMetaData controllerMetaData, string? sourceRoute, string httpMethod);

	/// <summary>
	/// Gets the handler controller.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <returns></returns>
	IControllerMetaData? GetHandlerController(HandlerControllerType controllerType);

	/// <summary>
	/// Determines whether controller can be executed on any page.
	/// </summary>
	/// <param name="metaData">The controller meta data.</param>
	/// <returns></returns>
	bool IsAnyPageController(IControllerMetaData metaData);

	/// <summary>
	/// Determines whether controller security rules violated.
	/// </summary>
	/// <param name="metaData">The controller meta data.</param>
	/// <param name="user">The current request user.</param>
	/// <returns></returns>
	SecurityRuleCheckResult IsSecurityRulesViolated(IControllerMetaData metaData, ClaimsPrincipal user);
}