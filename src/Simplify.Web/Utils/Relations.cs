using System;
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
	public static readonly Dictionary<HttpMethod, Type> HttpMethodToHttpMethodAttributeRelation = new()
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
	public static readonly Dictionary<HttpMethod, string> HttpMethodToToHttpMethodStringRelation = new()
	{
		{ HttpMethod.Get, "GET" },
		{ HttpMethod.Post, "POST" },
		{ HttpMethod.Put, "PUT" },
		{ HttpMethod.Patch, "PATCH" },
		{ HttpMethod.Delete, "DELETE" },
		{ HttpMethod.Options, "OPTIONS" }
	};
}
