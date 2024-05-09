﻿using System;
using System.Collections.Generic;

namespace Simplify.Web.Views.Meta;

/// <summary>
/// Represent a views meta store.
/// </summary>
public interface IViewsMetaStore
{
	/// <summary>
	/// Gets the current domain views types
	/// </summary>
	IList<Type> ViewsTypes { get; }
}