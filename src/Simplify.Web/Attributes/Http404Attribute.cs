using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates that the controller handles HTTP 404 errors.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class Http404Attribute : Attribute;