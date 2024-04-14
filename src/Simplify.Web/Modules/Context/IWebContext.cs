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
	string Route { get; }

	/// <summary>
	/// Gets the site root url, for example: http://mysite.com or http://localhost/mysite//
	/// </summary>
	string SiteUrl { get; }

	/// <summary>
	/// Gets the virtual path.
	/// </summary>
	string VirtualPath { get; }

	/// <summary>
	/// Gets the context for the current HTTP request.
	/// </summary>
	HttpContext Context { get; }

	/// <summary>
	/// Gets the request for the current HTTP request.
	/// </summary>
	HttpRequest Request { get; }

	/// <summary>
	/// Gets the response for the current HTTP request.
	/// </summary>
	HttpResponse Response { get; }

	/// <summary>
	/// Gets the query string for current HTTP request.
	/// </summary>
	IQueryCollection Query { get; }

	/// <summary>
	/// Gets the form data of post HTTP request.
	/// </summary>
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