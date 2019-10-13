using System;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should be not null or empty
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : Attribute
	{
	}
}