using System;

namespace Simplify.Web.ModelBinding.Attributes;

/// <summary>
/// Sets maximum required property length
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MaxLengthAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MaxLengthAttribute"/> class.
	/// </summary>
	/// <param name="maximumPropertyLength">Maximum length of the property.</param>
	public MaxLengthAttribute(int maximumPropertyLength)
	{
		MaximumPropertyLength = maximumPropertyLength;
	}

	/// <summary>
	/// Gets or sets the maximum length of the property.
	/// </summary>
	/// <value>
	/// The maximum length of the property.
	/// </value>
	public int MaximumPropertyLength { get; set; }
}