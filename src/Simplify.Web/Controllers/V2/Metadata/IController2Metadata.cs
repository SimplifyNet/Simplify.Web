using System;
using System.Collections.Generic;
using System.Reflection;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.V2.Metadata;

/// <summary>
/// Represents a controller v2 metadata information.
/// </summary>
public interface IController2Metadata : IControllerMetadata
{
	/// <summary>
	/// Gets the invoke method information.
	/// </summary>
	/// <value>
	/// The invoke method information.
	/// </value>
	public MethodInfo InvokeMethodInfo { get; }

	/// <summary>
	/// Gets the invoke method parameters.
	/// </summary>
	/// <value>
	/// The invoke method parameters.
	/// </value>
	public IDictionary<string, Type> InvokeMethodParameters { get; }
}