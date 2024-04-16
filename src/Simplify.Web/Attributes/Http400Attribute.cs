using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates that the controller handles HTTP 400 errors.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class Http400Attribute : Attribute;