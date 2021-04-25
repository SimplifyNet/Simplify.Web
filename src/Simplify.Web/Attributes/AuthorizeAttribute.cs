using System;
using System.Collections.Generic;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Indicates whether controller requires user authorization
	/// </summary>
	public class AuthorizeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
		/// </summary>
		/// <param name="requiredUserRoles">Required user roles.</param>
		public AuthorizeAttribute(string? requiredUserRoles = null)
		{
			if (requiredUserRoles == null)
				return;

			RequiredUserRoles = requiredUserRoles.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
		/// </summary>
		/// <param name="requiredUserRoles">The required user roles.</param>
		public AuthorizeAttribute(params string[] requiredUserRoles) => RequiredUserRoles = requiredUserRoles;

		/// <summary>
		/// Gets the required user roles.
		/// </summary>
		/// <value>
		/// The required user roles.
		/// </value>
		public IEnumerable<string> RequiredUserRoles { get; } = new List<string>();
	}
}