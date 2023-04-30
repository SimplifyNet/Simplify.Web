using System;

namespace Simplify.Web.ModelBinding.Attributes;

/// <summary>
/// Sets minimum required property length
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MinLengthAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MinLengthAttribute"/> class.
	/// </summary>
	/// <param name="minimumPropertyLength">Minimum length of the property.</param>
	public MinLengthAttribute(int minimumPropertyLength)
	{
		MinimumPropertyLength = minimumPropertyLength;
	}

	/// <summary>
	/// Gets or sets the minimum length of the property.
	/// </summary>
	/// <value>
	/// The minimum length of the property.
	/// </value>
	public int MinimumPropertyLength { get; set; }
}