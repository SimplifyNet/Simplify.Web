using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller execution priority
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PriorityAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PriorityAttribute"/> class.
		/// </summary>
		/// <param name="priority">The execution priority.</param>
		public PriorityAttribute(int priority) => Priority = priority;

		/// <summary>
		/// Gets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; }
	}
}