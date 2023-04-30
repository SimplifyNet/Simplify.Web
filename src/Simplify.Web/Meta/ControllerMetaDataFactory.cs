using System;
using System.Collections.Generic;
using Simplify.Web.Attributes;

#nullable disable

namespace Simplify.Web.Meta;

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
	public ControllerMetaData CreateControllerMetaData(Type controllerType)
	{
		return new ControllerMetaData(controllerType, GetControllerExecParameters(controllerType), GetControllerRole(controllerType), GetControllerSecurity(controllerType));
	}

	private static ControllerExecParameters GetControllerExecParameters(Type controllerType)
	{
		var priority = 0;

		var attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

		if (attributes.Length > 0)
			priority = ((PriorityAttribute)attributes[0]).Priority;

		var routeInfo = GetControllerRouteInfo(controllerType);

		return routeInfo != null || priority != 0
			? new ControllerExecParameters(routeInfo, priority)
			: null;
	}

	private static ControllerRouteInfo GetControllerRouteInfo(Type controllerType)
	{
		string getRoute = null;
		string postRoute = null;
		string putRoute = null;
		string patchRoute = null;
		string deleteRoute = null;

		var attributes = controllerType.GetCustomAttributes(typeof(GetAttribute), false);

		if (attributes.Length > 0)
			getRoute = ((GetAttribute)attributes[0]).Route;

		attributes = controllerType.GetCustomAttributes(typeof(PostAttribute), false);

		if (attributes.Length > 0)
			postRoute = ((PostAttribute)attributes[0]).Route;

		attributes = controllerType.GetCustomAttributes(typeof(PutAttribute), false);

		if (attributes.Length > 0)
			putRoute = ((PutAttribute)attributes[0]).Route;

		attributes = controllerType.GetCustomAttributes(typeof(PatchAttribute), false);

		if (attributes.Length > 0)
			patchRoute = ((PatchAttribute)attributes[0]).Route;

		attributes = controllerType.GetCustomAttributes(typeof(DeleteAttribute), false);

		if (attributes.Length > 0)
			deleteRoute = ((DeleteAttribute)attributes[0]).Route;

		return !string.IsNullOrEmpty(getRoute)
		       || !string.IsNullOrEmpty(postRoute)
		       || !string.IsNullOrEmpty(putRoute)
		       || !string.IsNullOrEmpty(patchRoute)
		       || !string.IsNullOrEmpty(deleteRoute)
			? new ControllerRouteInfo(getRoute, postRoute, putRoute, patchRoute, deleteRoute)
			: null;
	}

	private static ControllerRole GetControllerRole(Type controllerType)
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

	private static ControllerSecurity GetControllerSecurity(Type controllerType)
	{
		var isAuthorizationRequired = false;
		IEnumerable<string> requiredUserRoles = null;

		var attributes = controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), false);

		if (attributes.Length > 0)
		{
			isAuthorizationRequired = true;
			requiredUserRoles = ((AuthorizeAttribute)attributes[0]).RequiredUserRoles;
		}

		return isAuthorizationRequired ? new ControllerSecurity(true, requiredUserRoles) : null;
	}
}