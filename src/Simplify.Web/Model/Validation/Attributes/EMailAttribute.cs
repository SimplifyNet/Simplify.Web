using System;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should be a valid email address
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EMailAttribute : Attribute
	{
	}
}