using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set the controller execution priority.
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="PriorityAttribute" /> class.
/// </remarks>
/// <param name="priority">The execution priority.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PriorityAttribute(int priority) : Attribute
{
	/// <summary>
	/// Gets the priority.
	/// </summary>
	public int Priority { get; } = priority;
}