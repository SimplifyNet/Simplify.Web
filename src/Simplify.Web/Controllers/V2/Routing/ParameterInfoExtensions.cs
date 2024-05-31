using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplify.Web.Controllers.V2.Routing;

/// <summary>
/// Provides the parameter info extensions.
/// </summary>
public static class ParameterInfoExtensions
{
	/// <summary>
	/// Converts the parameter info list to parameter name and type dictionary.
	/// </summary>
	/// <param name="items">The items.</param>
	public static IDictionary<string, Type> ToLowercaseNameTypeDictionary(this IEnumerable<ParameterInfo> items) =>
		items.ToDictionary(x => (x.Name ?? throw new InvalidOperationException("Parameters name is null")).ToLowerInvariant(), x => x.ParameterType);
}