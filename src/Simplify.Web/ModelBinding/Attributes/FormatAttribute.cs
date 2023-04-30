using System;

namespace Simplify.Web.ModelBinding.Attributes;

/// <summary>
/// Sets format for parsing (for example, date time format)
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class FormatAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="FormatAttribute"/> class.
	/// </summary>
	/// <param name="format">The format.</param>
	public FormatAttribute(string format)
	{
		Format = format;
	}

	/// <summary>
	/// Gets or sets the format.
	/// </summary>
	/// <value>
	/// The format.
	/// </value>
	public string Format { get; set; }
}