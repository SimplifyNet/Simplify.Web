﻿using System;
using System.Collections.Generic;
using Simplify.Web.Attributes;
using Simplify.Web.Http;

namespace Simplify.Web.Utils;

/// <summary>
/// Provides the types relations container.
/// </summary>
public class Relations
{
	/// <summary>
	/// Provides the HTTP method to HTTP method attribute relations
	/// </summary>
	public static readonly Dictionary<HttpMethod, Type> HttpMethodToHttpMethodAttributeRelations = new()
	{
		{ HttpMethod.Get, typeof(GetAttribute) },
		{ HttpMethod.Post, typeof(PostAttribute) },
		{ HttpMethod.Put, typeof(PutAttribute) },
		{ HttpMethod.Patch, typeof(PatchAttribute) },
		{ HttpMethod.Delete, typeof(DeleteAttribute) },
		{ HttpMethod.Options, typeof(OptionsAttribute) }
	};
}