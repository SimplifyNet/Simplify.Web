using System;
using System.Collections.Generic;
using Simplify.Web.Attributes;

namespace Simplify.Web.Http;

/// <summary>
/// Provides the types relations container.
/// </summary>
public static class Relations
{
	/// <summary>
	/// Provides the HTTP method to HTTP method attribute relations
	/// </summary>
	public static readonly IReadOnlyDictionary<HttpMethod, Type> HttpMethodToHttpMethodAttributeRelation = new Dictionary<HttpMethod, Type>
	{
		{ HttpMethod.Get, typeof(GetAttribute) },
		{ HttpMethod.Post, typeof(PostAttribute) },
		{ HttpMethod.Put, typeof(PutAttribute) },
		{ HttpMethod.Patch, typeof(PatchAttribute) },
		{ HttpMethod.Delete, typeof(DeleteAttribute) },
		{ HttpMethod.Options, typeof(OptionsAttribute) }
	};

	/// <summary>
	/// Provides the HTTP method to HTTP method string attribute relations
	/// </summary>
	public static readonly IReadOnlyDictionary<HttpMethod, string> HttpMethodToToHttpMethodStringRelation = new Dictionary<HttpMethod, string>
	{
		{ HttpMethod.Get, "GET" },
		{ HttpMethod.Post, "POST" },
		{ HttpMethod.Put, "PUT" },
		{ HttpMethod.Patch, "PATCH" },
		{ HttpMethod.Delete, "DELETE" },
		{ HttpMethod.Options, "OPTIONS" }
	};
}