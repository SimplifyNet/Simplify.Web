using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Routing;

public static class Controller1PathParser
{
	private const string RegexPattern = @"^{[a-zA-Z0-9:_\-\[\]]+}$";
	private static readonly char[] RequiredSymbols = ['{', '}', ':'];

	/// <summary>
	/// Parses the specified controller path.
	/// </summary>
	/// <param name="controllerPath">The controller path.</param>
	/// <exception cref="ControllerRouteException">
	/// Bad controller path:  + controllerPath
	/// or
	/// </exception>
	public static IList<PathItem> Parse(string controllerPath) =>
		controllerPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(item =>
				Array.TrueForAll(RequiredSymbols, symbol => !item.Contains(symbol))
					? new PathSegment(item)
					: ParsePathItem(item, controllerPath))
			.ToList();

	private static PathItem ParsePathItem(string item, string controllerPath)
	{
		var matches = Regex.Matches(item, RegexPattern);

		if (matches.Count == 0)
			throw new ControllerRouteException("Bad controller path: " + controllerPath);

#if NETSTANDARD2_0
		var subItem = item.Substring(1, item.Length - 2);
#else
		var subItem = item[1..^1];
#endif

		if (!subItem.Contains(':'))
			return new PathParameter(subItem, typeof(string));

		var parameterData = subItem.Split(':');

		var type = ParseParameterType(parameterData[1])
				   ?? throw new ControllerRouteException(
					   $"Undefined controller parameter type '{parameterData[1]}', path: {controllerPath}");

		return new PathParameter(parameterData[0], type);
	}

	private static Type? ParseParameterType(string typeData) =>
		typeData switch
		{
			"int" => typeof(int),
			"decimal" => typeof(decimal),
			"bool" => typeof(bool),
			"[]" => typeof(string[]),
			"string[]" => typeof(string[]),
			"int[]" => typeof(int[]),
			"decimal[]" => typeof(decimal[]),
			"bool[]" => typeof(bool[]),
			_ => null
		};
}