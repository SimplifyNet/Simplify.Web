using System;
using System.Collections.Generic;
using Simplify.Web.Http;
using Simplify.Web.Old.Attributes;

namespace Simplify.Web.Old.Util;

/// <summary>
/// Provides types relations container.
/// </summary>
public class Relations
{
	/// <summary>
	/// The HTTP method to HTTP method attribute relations
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