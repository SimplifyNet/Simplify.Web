using System;
using System.Collections.Generic;

namespace Simplify.Web.Meta2;

/// <summary>
/// Represent views meta store.
/// </summary>
public interface IViewsMetaStore
{
	/// <summary>
	/// Current domain views types
	/// </summary>
	/// <returns></returns>
	IList<Type> ViewsTypes { get; }
}