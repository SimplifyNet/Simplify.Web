using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates that the controller handles HTTP 404 errors.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Class)]
public class Http404Attribute : Attribute;