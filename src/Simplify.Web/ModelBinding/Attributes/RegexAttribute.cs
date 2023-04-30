using System;

namespace Simplify.Web.ModelBinding.Attributes;

/// <summary>
/// Indicates what this property should match regular expression
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RegexAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RegexAttribute"/> class.
	/// </summary>
	/// <param name="regexString">The regex string.</param>
	public RegexAttribute(string regexString)
	{
		RegexString = regexString;
	}

	/// <summary>
	/// Gets or sets the regex string.
	/// </summary>
	/// <value>
	/// The regex string.
	/// </value>
	public string RegexString { get; set; }
}