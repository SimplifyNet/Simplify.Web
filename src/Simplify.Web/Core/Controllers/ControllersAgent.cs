#nullable disable

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Simplify.Web.Meta;
using Simplify.Web.Routing;
using Simplify.Web.Util;

namespace Simplify.Web.Core.Controllers
{
	/// <summary>
	/// Provides controllers agent
	/// </summary>
	public class ControllersAgent : IControllersAgent
	{
		private readonly IControllersMetaStore _controllersMetaStore;
		private readonly IRouteMatcher _routeMatcher;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersAgent" /> class.
		/// </summary>
		/// <param name="controllersMetaStore">The controllers meta store.</param>
		/// <param name="routeMatcher">The route matcher.</param>
		public ControllersAgent(IControllersMetaStore controllersMetaStore, IRouteMatcher routeMatcher)
		{
			_controllersMetaStore = controllersMetaStore;
			_routeMatcher = routeMatcher;
		}

		/// <summary>
		/// Gets the standard controllers meta data.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IControllerMetaData> GetStandardControllersMetaData()
		{
			return _controllersMetaStore.ControllersMetaData.Where(
				x =>
					x.Role == null || (x.Role.Is400Handler == false && x.Role.Is403Handler == false && x.Role.Is404Handler == false));
		}

		/// <summary>
		/// Matches the controller route.
		/// </summary>
		/// <param name="controllerMetaData">The controller meta data.</param>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="httpMethod">The HTTP method.</param>
		/// <returns></returns>
		public IRouteMatchResult MatchControllerRoute(IControllerMetaData controllerMetaData, string sourceRoute, string httpMethod)
		{
			if (controllerMetaData.ExecParameters == null)
				return _routeMatcher.Match(sourceRoute, null);

			var item = controllerMetaData.ExecParameters.Routes.FirstOrDefault(x => x.Key == HttpRequestUtil.HttpMethodStringToHttpMethod(httpMethod));

			return default(KeyValuePair<HttpMethod, string>).Equals(item) ? null : _routeMatcher.Match(sourceRoute, item.Value);
		}

		/// <summary>
		/// Gets the handler controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public IControllerMetaData GetHandlerController(HandlerControllerType controllerType)
		{
			IControllerMetaData metaData = null;

			switch (controllerType)
			{
				case HandlerControllerType.Http403Handler:
					metaData = _controllersMetaStore.ControllersMetaData.FirstOrDefault(x => x.Role != null && x.Role.Is403Handler);
					break;

				case HandlerControllerType.Http404Handler:
					metaData = _controllersMetaStore.ControllersMetaData.FirstOrDefault(x => x.Role != null && x.Role.Is404Handler);
					break;
			}

			return metaData;
		}

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
			if (metaData.Security == null)
				return SecurityRuleCheckResult.Ok;

			if (!metaData.Security.IsAuthorizationRequired)
				return SecurityRuleCheckResult.Ok;

			if (metaData.Security.RequiredUserRoles == null)
				return user?.Identity == null || !user.Identity.IsAuthenticated ? SecurityRuleCheckResult.NotAuthenticated : SecurityRuleCheckResult.Ok;

			if (user?.Identity == null || !user.Identity.IsAuthenticated)
				return SecurityRuleCheckResult.NotAuthenticated;

			return metaData.Security.RequiredUserRoles.Any(user.IsInRole) ? SecurityRuleCheckResult.Ok : SecurityRuleCheckResult.Forbidden;
		}
	}
}