using System;
using System.Collections.Generic;

namespace Simplify.Web.System;

/// <summary>
/// Provides String extensions
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// Parses the comma separated list.
	/// </summary>
	/// <param name="source">The source.</param>
	public static IEnumerable<string> ParseCommaSeparatedList(this string source) => source
		.Replace(" ", "")
		.Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
}