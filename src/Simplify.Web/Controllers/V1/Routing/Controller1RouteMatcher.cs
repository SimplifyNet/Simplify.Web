﻿using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Controllers.V1.Routing;

public class Controller1RouteMatcher : IRouteMatcher
{
	public bool CanHandle(IControllerMetadata controller) => controller is IController1Metadata;

	public IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute)
	{
		if (currentPath.Count != controllerRoute.Items.Count)
			return new RouteMatchResult();

		IDictionary<string, object> routeParameters = new Dictionary<string, object>();

		for (var i = 0; i < controllerRoute.Items.Count; i++)
		{
			var currentItem = controllerRoute.Items[i];

			if (currentItem is PathSegment)
			{
				if (currentItem.Name != currentPath[i])
					return new RouteMatchResult();
			}
			else if (currentItem is PathParameter item)
			{
				var value = GetParameterValue(item, currentPath[i]);

				if (value == null)
					return new RouteMatchResult();

				routeParameters.Add(item.Name, value);
			}
		}

		return new RouteMatchResult(true, (IReadOnlyDictionary<string, object>)routeParameters);
	}

	private static object? GetParameterValue(PathParameter pathParameter, string sourceValue)
	{
		if (pathParameter.Type == typeof(string))
			return sourceValue;

		if (pathParameter.Type == typeof(int))
			return GetIntParameterValue(sourceValue);

		if (pathParameter.Type == typeof(decimal))
			return GetDecimalParameterValue(sourceValue);

		if (pathParameter.Type == typeof(bool))
			return GetBoolParameterValue(sourceValue);

		if (pathParameter.Type == typeof(string[]))
			return GetStringArrayParameterValue(sourceValue);

		if (pathParameter.Type == typeof(int[]))
			return GetIntArrayParameterValue(sourceValue);

		if (pathParameter.Type == typeof(decimal[]))
			return GetDecimalArrayParameterValue(sourceValue);

		if (pathParameter.Type == typeof(bool[]))
			return GetBoolArrayParameterValue(sourceValue);

		return null;
	}

	private static object? GetIntParameterValue(string source)
	{
		if (!int.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetDecimalParameterValue(string source)
	{
		if (!decimal.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetBoolParameterValue(string source)
	{
		if (!bool.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static IList<string> GetStringArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

	private static IList<int> GetIntArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetIntParameterValue)
			.Where(x => x != null)
			.Cast<int>()
			.ToList();

	private static IList<decimal> GetDecimalArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetDecimalParameterValue)
			.Where(x => x != null)
			.Cast<decimal>()
			.ToList();

	private static IList<bool> GetBoolArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetBoolParameterValue)
			.Where(x => x != null)
			.Cast<bool>()
			.ToList();
}