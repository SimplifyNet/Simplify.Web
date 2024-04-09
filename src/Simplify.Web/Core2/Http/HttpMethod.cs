namespace Simplify.Web.Core2.Http;

/// <summary>
/// Provides HTTP methods used in Simplify.Web.
/// </summary>
public enum HttpMethod
{
	/// <summary>
	/// Undefined HTTP method.
	/// </summary>
	Undefined,

	/// <summary>
	/// The GET HTTP method.
	/// </summary>
	Get,

	/// <summary>
	/// The POST HTTP method.
	/// </summary>
	Post,

	/// <summary>
	/// The PUT HTTP method.
	/// </summary>
	Put,

	/// <summary>
	/// The PATCH HTTP method.
	/// </summary>
	Patch,

	/// <summary>
	/// The DELETE HTTP method.
	/// </summary>
	Delete,

	/// <summary>
	/// The OPTIONS HTTP method.
	/// </summary>
	Options
}