using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller execution priority.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PriorityAttribute"/> class.
/// </remarks>
/// <param name="priority">The execution priority.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PriorityAttribute(int priority) : Attribute
{

	/// <summary>
	/// Gets the priority.
	/// </summary>
	/// <value>
	/// The priority.
	/// </value>
	public int Priority { get; } = priority;
}