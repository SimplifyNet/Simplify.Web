using System;

namespace Simplify.Web.Old.Attributes;

/// <summary>
/// Indicates what controller handles HTTP 400 errors.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class Http400Attribute : Attribute
{
}