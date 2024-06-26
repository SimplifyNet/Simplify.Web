﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Simplify.Web.Attributes;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller base metadata information.
/// </summary>
/// <seealso cref="IControllerMetadata" />
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerMetadata" /> class.
/// </remarks>
/// <param name="controllerType">Type of the controller.</param>
public abstract class ControllerMetadata(Type controllerType) : IControllerMetadata
{
	/// <summary>
	/// Gets the type of the controller.
	/// </summary>
	/// <value>
	/// The type of the controller.
	/// </value>
	public Type ControllerType { get; } = controllerType;

	/// <summary>
	/// Gets the controller execute parameters.
	/// </summary>
	/// <value>
	/// The execute parameters.
	/// </value>
	public ControllerExecParameters? ExecParameters { get; protected set; }

	/// <summary>
	/// Gets the controller role information.
	/// </summary>
	/// <value>
	/// The role.
	/// </value>
	public ControllerRole? Role { get; } = BuildControllerRole(controllerType);

	/// <summary>
	/// Gets the controller security information.
	/// </summary>
	/// <value>
	/// The security.
	/// </value>
	public ControllerSecurity? Security { get; } = BuildControllerSecurity(controllerType);

	/// <summary>
	/// Builds the controller route.
	/// </summary>
	/// <param name="path">The path.</param>
	protected abstract IControllerRoute BuildControllerRoute(string path);

	/// <summary>
	/// Builds the controller execute parameters.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	protected ControllerExecParameters? BuildControllerExecParameters(ICustomAttributeProvider controllerType)
	{
		var priority = 0;

		var attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

		if (attributes.Length > 0)
			priority = ((PriorityAttribute)attributes[0]).Priority;

		var routeInfo = BuildControllerRouteInfo(controllerType);

		return routeInfo.Count > 0 || priority != 0
			? new ControllerExecParameters(routeInfo, priority)
			: null;
	}

	private static ControllerRole? BuildControllerRole(ICustomAttributeProvider controllerType)
	{
		var http403 = false;
		var http404 = false;

		var attributes = controllerType.GetCustomAttributes(typeof(Http403Attribute), false);

		if (attributes.Length > 0)
			http403 = true;

		attributes = controllerType.GetCustomAttributes(typeof(Http404Attribute), false);

		if (attributes.Length > 0)
			http404 = true;

		return http403 || http404
				? new ControllerRole(http403, http404)
				: null;
	}

	private static ControllerSecurity? BuildControllerSecurity(ICustomAttributeProvider controllerType)
	{
		var isAuthorizationRequired = false;
		IEnumerable<string>? requiredUserRoles = null;

		var attributes = controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), false);

		if (attributes.Length > 0)
		{
			isAuthorizationRequired = true;
			requiredUserRoles = ((AuthorizeAttribute)attributes[0]).RequiredUserRoles;
		}

		return isAuthorizationRequired
			? new ControllerSecurity(true, requiredUserRoles)
			: null;
	}

	private IDictionary<HttpMethod, IControllerRoute> BuildControllerRouteInfo(ICustomAttributeProvider controllerType)
	{
		var routeInfo = new Dictionary<HttpMethod, IControllerRoute>();

		foreach (var item in Relations.HttpMethodToHttpMethodAttributeRelation)
		{
			var attributes = controllerType.GetCustomAttributes(item.Value, false);

			if (attributes.Length > 0)
				routeInfo.Add(item.Key, BuildControllerRoute(((ControllerRouteAttribute)attributes[0]).Route));
		}

		return routeInfo;
	}
}