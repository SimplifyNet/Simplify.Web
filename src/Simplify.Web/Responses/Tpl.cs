﻿using System;
using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the template response (puts the data to DataCollector).
/// </summary>
/// <seealso cref="ControllerResponse" />
public class Tpl : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Tpl" /> class.
	/// </summary>
	/// <param name="data">The data for main content variable.</param>
	/// <param name="title">The site title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	public Tpl(string data, string? title = null, int statusCode = 200)
	{
		Data = data;
		Title = title;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Tpl" /> class.
	/// </summary>
	/// <param name="template">The template.</param>
	/// <param name="title">The site title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException">template</exception>
	public Tpl(ITemplate template, string? title = null, int statusCode = 200)
	{
		if (template == null)
			throw new ArgumentNullException(nameof(template));

		Data = template.Get();

		Title = title;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the data for main content variable.
	/// </summary>
	public string Data { get; }

	/// <summary>
	/// Gets the site title.
	/// </summary>
	public string? Title { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	public int StatusCode { get; }

	/// <summary>
	/// Executes this response asynchronously.
	/// </summary>
	public override Task<ResponseBehavior> ExecuteAsync()
	{
		Context.Response.StatusCode = StatusCode;

		DataCollector.Add(Data);

		if (!string.IsNullOrEmpty(Title))
			DataCollector.AddTitle(Title);

		return Task.FromResult(ResponseBehavior.Default);
	}
}