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
	public MethodInfo InvokeMethodInfo { get; }
	public IDictionary<string, Type> InvokeMethodParameters { get; }
}