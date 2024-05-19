using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Controllers.V1.Routing;

public class Controller1RouteMatcher : IRouteMatcher
{
	private static readonly Dictionary<Type, Func<string, object?>> ParameterValueConverters =
		new()
		{
			{ typeof(string), sourceValue => sourceValue },
			{ typeof(int), GetIntParameterValue },
			{ typeof(decimal), GetDecimalParameterValue },
			{ typeof(bool), GetBoolParameterValue },
			{ typeof(string[]), GetStringArrayParameterValue },
			{ typeof(int[]), GetIntArrayParameterValue },
			{ typeof(decimal[]), GetDecimalArrayParameterValue },
			{ typeof(bool[]), GetBoolArrayParameterValue },
		};

	public bool CanHandle(IControllerMetadata controller) => controller is IController1Metadata;

	public IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute)
	{
		// Run on all pages route
		if (controllerRoute.Items.Count == 0)
			return new RouteMatchResult(true);

		if (currentPath.Count != controllerRoute.Items.Count)
			return new RouteMatchResult();

		var routeParameters = new Dictionary<string, object>();

		return TryMatchPathItems(currentPath, controllerRoute.Items, routeParameters)
			? new RouteMatchResult(true, routeParameters)
			: new RouteMatchResult();
	}

	private static bool TryMatchPathItems(IList<string> currentPath, IList<PathItem> controllerRouteItems, Dictionary<string, object> routeParameters) =>
		!controllerRouteItems.Where((currentItem, i) => !MatchPathItem(currentItem, currentPath[i], routeParameters)).Any();

	private static bool MatchPathItem(PathItem item, string currentPathSegment, Dictionary<string, object> routeParameters)
	{
		switch (item)
		{
			case PathSegment segment:
				return segment.Name == currentPathSegment;

			case PathParameter parameter:
				var value = GetParameterValue(parameter, currentPathSegment);

				if (value == null)
					return false;

				routeParameters.Add(parameter.Name, value);
				return true;

			default:
				return false;
		}
	}

	private static object? GetParameterValue(PathParameter pathParameter, string sourceValue) =>
		ParameterValueConverters.TryGetValue(pathParameter.Type, out var converter)
			? converter(sourceValue)
			: null;

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