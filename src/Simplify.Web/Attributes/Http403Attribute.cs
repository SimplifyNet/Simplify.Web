using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates that the controller handles HTTP 403 errors.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Class)]
public class Http403Attribute : Attribute;