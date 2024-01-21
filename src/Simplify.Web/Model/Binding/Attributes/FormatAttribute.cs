using System;

namespace Simplify.Web.Model.Binding.Attributes;

/// <summary>
/// Sets format for parsing (for example, date time format).
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FormatAttribute"/> class.
/// </remarks>
/// <param name="format">The format.</param>
[AttributeUsage(AttributeTargets.Property)]
public class FormatAttribute(string format) : Attribute
{

	/// <summary>
	/// Gets or sets the format.
	/// </summary>
	/// <value>
	/// The format.
	/// </value>
	public string Format { get; set; } = format;
}