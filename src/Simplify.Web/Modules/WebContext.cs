﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules
{
	/// <summary>
	/// Provides web context
	/// </summary>
	public class WebContext : IWebContext
	{
		private readonly SemaphoreSlim _formReadLock = new SemaphoreSlim(1, 1);
		private readonly SemaphoreSlim _requestBodyReadLock = new SemaphoreSlim(1, 1);

		private IFormCollection _form;
		private string _requestBody;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebContext"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public WebContext(HttpContext context)
		{
			Context = context;
			Request = context.Request;
			Response = context.Response;
			Query = context.Request.Query;

			VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value;

			SiteUrl = Request.Scheme + "://" + Request.Host.Value + VirtualPath + "/";

			IsAjax = Request.Headers.ContainsKey("X-Requested-With");

			Route = Request.Path.Value;
		}

		/// <summary>
		/// Current web-site route, for example: "/" or "/user/delete/15"
		/// </summary>
		public string Route { get; }

		/// <summary>
		/// Site root url, for example: http://mysite.com or http://localhost/mysite/
		/// </summary>
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
		public HttpContext Context { get; }

		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		public HttpRequest Request { get; }

		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		public HttpResponse Response { get; }

		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		public IQueryCollection Query { get; }

		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
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
		/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
		/// </value>
		public bool IsAjax { get; }

		/// <summary>
		/// Gets or sets the request body.
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

				if (_requestBody == null)
					throw new InvalidOperationException("Request body is null");

				return _requestBody;
			}
		}

		/// <summary>
		/// Reads the form asynchronously.
		/// </summary>
		public async Task ReadFormAsync()
		{
			await _formReadLock.WaitAsync();

			if (_form != null)
				return;

			try
			{
				_form = await Context.Request.ReadFormAsync();
			}
			finally
			{
				_formReadLock.Release();
			}
		}

		/// <summary>
		/// Reads the form asynchronously.
		/// </summary>
		public async Task ReadRequestBodyAsync()
		{
			await _requestBodyReadLock.WaitAsync();

			if (_requestBody != null)
				return;

			try
			{
				using var reader = new StreamReader(Context.Request.Body);

				_requestBody = await reader.ReadToEndAsync() ?? "";
			}
			finally
			{
				_requestBodyReadLock.Release();
			}
		}
	}
}