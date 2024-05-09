﻿using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.V1.Matcher;

/// <summary>
/// Provides HTTP route parser and matcher.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RouteMatcher"/> class.
/// </remarks>
/// <param name="controllerPathParser">The controller path parser.</param>
public class Controller1RouteMatcher(IController1PathParser controllerPathParser) : IRouteMatcher
{
	public bool CanHandle(IControllerMetadata controller) => controller is IController1Metadata;

	/// <summary>
	/// Matches the current path with controller path.
	/// Only "/", "/action", "/action/{userName}/{id}", "/action/{id:int}", "/{id}" etc. route types allowed
	/// </summary>
	/// <param name="currentPath">The current path.</param>
	/// <param name="controllerPath">The controller path.</param>
	/// <returns></returns>
	public IRouteMatchResult Match(string? currentPath, string? controllerPath)
	{
		if (string.IsNullOrEmpty(currentPath))
			return new RouteMatchResult();

		// Run on all pages route
		if (string.IsNullOrEmpty(controllerPath))
			return new RouteMatchResult(true);

		var controllerPathParsed = controllerPathParser.Parse(controllerPath!);
		var currentPathItems = currentPath!.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

		if (currentPathItems.Length != controllerPathParsed.Items.Count)
			return new RouteMatchResult();

		IDictionary<string, object> routeParameters = new Dictionary<string, object>();

		for (var i = 0; i < controllerPathParsed.Items.Count; i++)
		{
			var currentItem = controllerPathParsed.Items[i];

			if (currentItem is PathSegment)
			{
				if (currentItem.Name != currentPathItems[i])
					return new RouteMatchResult();
			}
			else if (currentItem is PathParameter item)
			{
				var value = GetParameterValue(item, currentPathItems[i]);

				if (value == null)
					return new RouteMatchResult();

				routeParameters.Add(item.Name, value);
			}
		}

		return new RouteMatchResult(true, (IReadOnlyDictionary<string, object>)routeParameters);
	}

	private static object? GetParameterValue(PathParameter pathParameter, string source)
	{
		if (pathParameter.Type == typeof(string))
			return source;

		if (pathParameter.Type == typeof(int))
			return GetIntParameterValue(source);

		if (pathParameter.Type == typeof(decimal))
			return GetDecimalParameterValue(source);

		if (pathParameter.Type == typeof(bool))
			return GetBoolParameterValue(source);

		if (pathParameter.Type == typeof(string[]))
			return GetStringArrayParameterValue(source);

		if (pathParameter.Type == typeof(int[]))
			return GetIntArrayParameterValue(source);

		if (pathParameter.Type == typeof(decimal[]))
			return GetDecimalArrayParameterValue(source);

		if (pathParameter.Type == typeof(bool[]))
			return GetBoolArrayParameterValue(source);

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