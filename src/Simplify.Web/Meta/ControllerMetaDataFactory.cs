using System;
using System.Collections.Generic;
using System.Reflection;
using Simplify.Web.Attributes;
using Simplify.Web.Util;

namespace Simplify.Web.Meta
{
	/// <summary>
	/// Creates controller meta-data
	/// </summary>
	public class ControllerMetaDataFactory : IControllerMetaDataFactory
	{
		/// <summary>
		/// Creates the controller meta data.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public ControllerMetaData CreateControllerMetaData(Type controllerType) =>
			new(controllerType, GetControllerExecParameters(controllerType), GetControllerRole(controllerType), GetControllerSecurity(controllerType));

		private static ControllerExecParameters? GetControllerExecParameters(ICustomAttributeProvider controllerType)
		{
			var priority = 0;

			var attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			var routeInfo = GetControllerRouteInfo(controllerType);

			return routeInfo.Count > 0 || priority != 0
				? new ControllerExecParameters(routeInfo, priority)
				: null;
		}

		private static IDictionary<HttpMethod, string> GetControllerRouteInfo(ICustomAttributeProvider controllerType)
		{
			var routeInfo = new Dictionary<HttpMethod, string>();

			foreach (var item in Relations.HttpMethodToHttpMethodAttributeRelations)
			{
				var attributes = controllerType.GetCustomAttributes(item.Value, false);

				if (attributes.Length > 0)
					routeInfo.Add(item.Key, ((ControllerRouteAttribute)attributes[0]).Route);
			}

			return routeInfo;
		}

		private static ControllerRole? GetControllerRole(ICustomAttributeProvider controllerType)
		{
			var http400 = false;
			var http403 = false;
			var http404 = false;

			var attributes = controllerType.GetCustomAttributes(typeof(Http400Attribute), false);

			if (attributes.Length > 0)
				http400 = true;

			attributes = controllerType.GetCustomAttributes(typeof(Http403Attribute), false);

			if (attributes.Length > 0)
				http403 = true;

			attributes = controllerType.GetCustomAttributes(typeof(Http404Attribute), false);

			if (attributes.Length > 0)
				http404 = true;

			return http403 || http404 ? new ControllerRole(http400, http403, http404) : null;
		}

		private static ControllerSecurity? GetControllerSecurity(ICustomAttributeProvider controllerType)
		{
			var isAuthorizationRequired = false;
			IEnumerable<string>? requiredUserRoles = null;

			var attributes = controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), false);

			if (attributes.Length > 0)
			{
				isAuthorizationRequired = true;
				requiredUserRoles = ((AuthorizeAttribute)attributes[0]).RequiredUserRoles;
			}

			return isAuthorizationRequired ? new ControllerSecurity(true, requiredUserRoles) : null;
		}
	}
}