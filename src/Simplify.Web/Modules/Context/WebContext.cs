using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Provides the web context.
/// </summary>
/// <seealso cref="IWebContext" />
public sealed class WebContext : IWebContext, IDisposable
{
	private readonly SemaphoreSlim _formReadSemaphore = new(1, 1);
	private readonly SemaphoreSlim _requestBodyReadSemaphore = new(1, 1);

	private IFormCollection? _form;
	private string? _requestBody;
	private bool _disposed;

	/// <summary>
	/// Initializes a new instance of the <see cref="WebContext" /> class.
	/// </summary>
	/// <param name="context">The HTTP context.</param>
	public WebContext(HttpContext context)
	{
		Context = context;
		Request = context.Request;
		Response = context.Response;
		Query = context.Request.Query;

		VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value;

		SiteUrl = $"{Request.Scheme}://{Request.Host.Value}{VirtualPath}/";

		IsAjax = Request.Headers.ContainsKey("X-Requested-With");

		Route = Request.Path.Value ?? "/";
	}

	/// <summary>
	/// Gets the current web-site route, for example: "/" or "/user/delete/15"/
	/// </summary>
	/// <value>
	/// The route.
	/// </value>
	public string Route { get; }

	/// <summary>
	/// Gets the site root url, for example: http://mysite.com or http://localhost/mysite//
	/// </summary>
	/// <value>
	/// The site URL.
	/// </value>
	public string SiteUrl { get; }

	/// <summary>
	/// Gets the virtual path.
	/// </summary>
	/// <value>
	/// The virtual path.
	/// </value>
	public string VirtualPath { get; }

	/// <summary>
	/// Gets the context for the current HTTP request.
	/// </summary>
	/// <value>
	/// The context.
	/// </value>
	public HttpContext Context { get; }

	/// <summary>
	/// Gets the request for the current HTTP request.
	/// </summary>
	/// <value>
	/// The request.
	/// </value>
	public HttpRequest Request { get; }

	/// <summary>
	/// Gets the response for the current HTTP request.
	/// </summary>
	/// <value>
	/// The response.
	/// </value>
	public HttpResponse Response { get; }

	/// <summary>
	/// Gets the query string for current HTTP request.
	/// </summary>
	/// <value>
	/// The query.
	/// </value>
	public IQueryCollection Query { get; }

	/// <summary>
	/// Gets the form data of post HTTP request.
	/// </summary>
	/// <value>
	/// The form.
	/// </value>
	/// <exception cref="InvalidOperationException">Form is null</exception>
	public IFormCollection Form
	{
		get
		{
			if (_form != null)
				return _form;

			ReadFormAsync().Wait();

			if (_form == null)
				throw new InvalidOperationException("Form is null");

			return _form;
		}
	}

	/// <summary>
	/// Gets a value indicating whether this request is ajax request.
	/// </summary>
	/// <value>
	///   <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
	/// </value>
	public bool IsAjax { get; }

	/// <summary>
	/// Gets a value indicating whether current request context user is not null and is authenticated.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
	/// </value>
	public bool IsAuthenticated => Context.User is { Identity.IsAuthenticated: true };

	/// <summary>
	/// Gets the request body.
	/// </summary>
	/// <value>
	/// The request body.
	/// </value>
	public string RequestBody
	{
		get
		{
			if (_requestBody != null)
				return _requestBody;

			ReadRequestBodyAsync().Wait();

			return _requestBody!;
		}
	}

	/// <summary>
	/// Reads the form asynchronously.
	/// </summary>
	public async Task ReadFormAsync()
	{
		if (_form != null)
			return;

		await _formReadSemaphore.WaitAsync();

		try
		{
			if (_form != null)
				return;

			_form = await Context.Request.ReadFormAsync();
		}
		finally
		{
			_formReadSemaphore.Release();
		}
	}

	/// <summary>
	/// Reads the request body asynchronously.
	/// </summary>
	public async Task ReadRequestBodyAsync()
	{
		if (_requestBody != null)
			return;

		await _requestBodyReadSemaphore.WaitAsync();

		try
		{
			if (_requestBody != null)
				return;

			// Keep the underlying request stream open so that other middleware /
			// model binders downstream can still consume the body if it is buffered.
#if NETSTANDARD2_0 || NETSTANDARD2_1
			using var reader = new StreamReader(Context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);
#else
			using var reader = new StreamReader(Context.Request.Body, leaveOpen: true);
#endif

			_requestBody = await reader.ReadToEndAsync();
		}
		finally
		{
			_requestBodyReadSemaphore.Release();
		}
	}

	/// <summary>
	/// Releases unmanaged resources held by the web context (the semaphores used to
	/// serialize body / form reads).
	/// </summary>
	public void Dispose()
	{
		if (_disposed)
			return;

		_disposed = true;

		_formReadSemaphore.Dispose();
		_requestBodyReadSemaphore.Dispose();
	}
}