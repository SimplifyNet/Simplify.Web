using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V2.Routing;

/// <summary>
/// Provides the controller v2 path parser.
/// </summary>
public static class Controller2PathParser
{
	private const string RegexPattern = @"^{[a-zA-Z0-9_\-]+}$";

	private static readonly char[] RequiredSymbols = ['{', '}'];

	/// <summary>
	/// Parses the specified controller path.
	/// </summary>
	/// <param name="controllerPath">The controller path.</param>
	/// <param name="invokeMethodParameters">The method parameters.</param>
	/// <exception cref="ControllerRouteException">Bad controller path:  + controllerPath
	/// or</exception>
	public static IList<PathItem> Parse(string controllerPath, IDictionary<string, Type> invokeMethodParameters)
	{
		var result = controllerPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(item =>
				Array.TrueForAll(RequiredSymbols, symbol => !item.Contains(symbol))
					? new PathSegment(item)
					: ParsePathParameter(item, controllerPath, invokeMethodParameters))
			.ToList();

		CheckMissingRouteParameters(result, invokeMethodParameters);

		return result;
	}

	private static void CheckMissingRouteParameters(List<PathItem> pathItems, IDictionary<string, Type> invokeMethodParameters)
	{
		foreach (var item in invokeMethodParameters.Keys)
			if (!pathItems.Exists(x => x is PathParameter && x.Name == item))
				throw new ControllerRouteException($"No route parameter found for Invoke method parameter '{item}'");
	}

	private static PathItem ParsePathParameter(string item, string controllerPath, IDictionary<string, Type> invokeMethodParameters)
	{
		var matches = Regex.Matches(item, RegexPattern);

		if (matches.Count == 0)
			throw new ControllerRouteException("Bad controller path: " + controllerPath);

#if NETSTANDARD2_0
		var parameterName = item.Substring(1, item.Length - 2);
#else
		var parameterName = item[1..^1];
#endif

		parameterName = parameterName.ToLowerInvariant();

		if (!invokeMethodParameters.TryGetValue(parameterName, out var parameterType))
			return new PathParameter(parameterName, typeof(string));

		if (!StringConverter.ValueConverters.ContainsKey(parameterType))
			throw new ControllerRouteException(
				$"Unsupported parameter type '{parameterType.Name}' of parameter '{parameterName}'. Can be one of: {StringConverter.ValueConverters.Keys.GetTypeNamesAsString()}");

		return new PathParameter(parameterName, parameterType);
	}
}