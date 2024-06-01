using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Represents a web context
/// </summary>
public interface IWebContext
{
	/// <summary>
	/// Gets the current web-site route, for example: "/" or "/user/delete/15"/
	/// </summary>
	/// <value>
	/// The route.
	/// </value>
	string Route { get; }

	/// <summary>
	/// Gets the site root url, for example: http://mysite.com or http://localhost/mysite//
	/// </summary>
	/// <value>
	/// The site URL.
	/// </value>
	string SiteUrl { get; }

	/// <summary>
	/// Gets the virtual path.
	/// </summary>
	/// <value>
	/// The virtual path.
	/// </value>
	string VirtualPath { get; }

	/// <summary>
	/// Gets the context for the current HTTP request.
	/// </summary>
	/// <value>
	/// The context.
	/// </value>
	HttpContext Context { get; }

	/// <summary>
	/// Gets the request for the current HTTP request.
	/// </summary>
	/// <value>
	/// The request.
	/// </value>
	HttpRequest Request { get; }

	/// <summary>
	/// Gets the response for the current HTTP request.
	/// </summary>
	/// <value>
	/// The response.
	/// </value>
	HttpResponse Response { get; }

	/// <summary>
	/// Gets the query string for current HTTP request.
	/// </summary>
	/// <value>
	/// The query.
	/// </value>
	IQueryCollection Query { get; }

	/// <summary>
	/// Gets the form data of post HTTP request.
	/// </summary>
	/// <value>
	/// The form.
	/// </value>
	IFormCollection Form { get; }

	/// <summary>
	/// Gets a value indicating whether this request is ajax request.
	/// </summary>
	/// <value>
	/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
	/// </value>
	bool IsAjax { get; }

	/// <summary>
	/// Gets a value indicating whether current request context user is not null and is authenticated.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
	/// </value>
	bool IsAuthenticated { get; }

	/// <summary>
	/// Gets the request body.
	/// </summary>
	/// <value>
	/// The request body.
	/// </value>
	string RequestBody { get; }

	/// <summary>
	/// Reads the form asynchronously.
	/// </summary>
	Task ReadFormAsync();

	/// <summary>
	/// Reads the request body asynchronously.
	/// </summary>
	Task ReadRequestBodyAsync();
}