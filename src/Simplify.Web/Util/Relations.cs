using System;
using System.Collections.Generic;
using Simplify.Web.Attributes;
using Simplify.Web.Meta;

namespace Simplify.Web.Util
{
	/// <summary>
	/// Provides types relations container
	/// </summary>
	public class Relations
	{
		/// <summary>
		/// The HTTP method to HTTP method attribute relations
		/// </summary>
		public static readonly Dictionary<HttpMethod, Type> HttpMethodToHttpMethodAttributeRelations = new Dictionary<HttpMethod, Type>
		{
			{ HttpMethod.Get, typeof(GetAttribute) },
			{ HttpMethod.Post, typeof(PostAttribute) },
			{ HttpMethod.Put, typeof(PutAttribute) },
			{ HttpMethod.Patch, typeof(PatchAttribute) },
			{ HttpMethod.Delete, typeof(DeleteAttribute) },
			{ HttpMethod.Options, typeof(OptionsAttribute) }
		};
	}
}