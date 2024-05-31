using System;

namespace Simplify.Web.Model.Binding.Attributes;

/// <summary>
/// Sets the format for parsing (for example, date time format).
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="FormatAttribute" /> class.
/// </remarks>
/// <param name="format">The format.</param>
[AttributeUsage(AttributeTargets.Property)]
public class FormatAttribute(string format) : Attribute
{
	/// <summary>
	/// Gets the format.
	/// </summary>
	public string Format { get; } = format;
}