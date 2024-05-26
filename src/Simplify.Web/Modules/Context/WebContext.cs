using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Provides the web context.
/// </summary>
public sealed class WebContext : IWebContext
{
	private readonly SemaphoreSlim _formReadSemaphore = new(1, 1);
	private readonly SemaphoreSlim _requestBodyReadSemaphore = new(1, 1);

	private IFormCollection? _form;
	private string? _requestBody;

	/// <summary>
	/// Initializes a new instance of the <see cref="WebContext"/> class.
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

	public string Route { get; }

	public string SiteUrl { get; }

	public string VirtualPath { get; }

	public HttpContext Context { get; }

	public HttpRequest Request { get; }

	public HttpResponse Response { get; }

	public IQueryCollection Query { get; }

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

	public bool IsAjax { get; }

	public bool IsAuthenticated => Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;

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

	public async Task ReadFormAsync()
	{
		if (_form != null)
			return;

		await _formReadSemaphore.WaitAsync();

		try
		{
			_form = await Context.Request.ReadFormAsync();
		}
		finally
		{
			_formReadSemaphore.Release();
		}
	}

	public async Task ReadRequestBodyAsync()
	{
		if (_requestBody != null)
			return;

		await _requestBodyReadSemaphore.WaitAsync();

		try
		{
			using var reader = new StreamReader(Context.Request.Body);

			_requestBody = await reader.ReadToEndAsync();
		}
		finally
		{
			_requestBodyReadSemaphore.Release();
		}
	}
}