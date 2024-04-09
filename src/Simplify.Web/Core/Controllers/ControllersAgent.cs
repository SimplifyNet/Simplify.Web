using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;
using Simplify.Web.Routing;
using Simplify.Web.Util;

namespace Simplify.Web.Core.Controllers;

/// <summary>
/// Provides controllers agent.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllersAgent" /> class.
/// </remarks>
/// <param name="controllersMetaStore">The controllers meta store.</param>
/// <param name="routeMatcher">The route matcher.</param>
public class ControllersAgent(IControllersMetaStore controllersMetaStore, IRouteMatcher routeMatcher) : IControllersAgent
{
	private readonly IControllersMetaStore _controllersMetaStore = controllersMetaStore;
	private readonly IRouteMatcher _routeMatcher = routeMatcher;

	/// <summary>
	/// Gets the standard controllers meta data.
	/// </summary>
	/// <returns></returns>
	public IList<IControllerMetaData> GetStandardControllersMetaData() =>
		SortControllersMetaContainers(_controllersMetaStore.ControllersMetaData.Where(x =>
			x.Role == null || (
			x.Role.Is400Handler == false &&
			x.Role.Is403Handler == false &&
			x.Role.Is404Handler == false)));

	/// <summary>
	/// Matches the controller route.
	/// </summary>
	/// <param name="controllerMetaData">The controller meta data.</param>
	/// <param name="sourceRoute">The source route.</param>
	/// <param name="httpMethod">The HTTP method.</param>
	/// <returns></returns>
	public IRouteMatchResult? MatchControllerRoute(IControllerMetaData controllerMetaData, string? sourceRoute, string httpMethod)
	{
		if (controllerMetaData.ExecParameters == null || controllerMetaData.ExecParameters.Routes.Count == 0)
			return _routeMatcher.Match(sourceRoute, null);

		var item = controllerMetaData.ExecParameters.Routes.FirstOrDefault(x => x.Key == HttpRequestUtil.HttpMethodStringToHttpMethod(httpMethod));

		return default(KeyValuePair<HttpMethod, string>).Equals(item)
			? null
			: _routeMatcher.Match(sourceRoute, item.Value);
	}

	/// <summary>
	/// Gets the handler controller.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <returns></returns>
	public IControllerMetaData? GetHandlerController(HandlerControllerType controllerType) =>
		controllerType switch
		{
			HandlerControllerType.Http403Handler => _controllersMetaStore.ControllersMetaData.FirstOrDefault(x =>
				x.Role is { Is403Handler: true }),
			HandlerControllerType.Http404Handler => _controllersMetaStore.ControllersMetaData.FirstOrDefault(x =>
				x.Role is { Is404Handler: true }),
			_ => null
		};

	/// <summary>
	/// Determines whether controller can be executed on any page.
	/// </summary>
	/// <param name="metaData">The controller meta data.</param>
	/// <returns></returns>
	public bool IsAnyPageController(IControllerMetaData metaData)
	{
		if (metaData.Role != null)
			if (metaData.Role.Is400Handler
				|| metaData.Role.Is403Handler
				|| metaData.Role.Is404Handler)
				return false;

		if (metaData.ExecParameters == null)
			return true;

		return metaData.ExecParameters.Routes.Count == 0 || metaData.ExecParameters.Routes.All(x => string.IsNullOrEmpty(x.Value));
	}

	/// <summary>
	/// Determines whether controller security rules violated.
	/// </summary>
	/// <param name="metaData">The controller meta data.</param>
	/// <param name="user">The current request user.</param>
	/// <returns></returns>
	public SecurityRuleCheckResult IsSecurityRulesViolated(IControllerMetaData metaData, ClaimsPrincipal user)
	{
		if (metaData.Security is not { IsAuthorizationRequired: true })
			return SecurityRuleCheckResult.Ok;

		if (metaData.Security.RequiredUserRoles == null || !metaData.Security.RequiredUserRoles.Any())
			return user?.Identity == null ||
				!user.Identity.IsAuthenticated
					? SecurityRuleCheckResult.NotAuthenticated
					: SecurityRuleCheckResult.Ok;

		if (user?.Identity == null || !user.Identity.IsAuthenticated)
			return SecurityRuleCheckResult.NotAuthenticated;

		return metaData.Security.RequiredUserRoles.Any(user.IsInRole)
			? SecurityRuleCheckResult.Ok
			: SecurityRuleCheckResult.Forbidden;
	}

	private static IList<IControllerMetaData> SortControllersMetaContainers(IEnumerable<IControllerMetaData> controllersMetaContainers) =>
		controllersMetaContainers.OrderBy(x => x.ExecParameters?.RunPriority ?? 0)
			.ToList();
}