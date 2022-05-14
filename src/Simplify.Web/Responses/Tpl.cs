using System;
using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides template response (puts data to DataCollector)
/// </summary>
public class Tpl : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Tpl"/> class.
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
	public Tpl(ITemplate template, string? title = null, int statusCode = 200)
	{
		if (template == null)
			throw new ArgumentNullException(nameof(template));

		Data = template.Get();

		Title = title;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the The data for main content variable.
	/// </summary>
	public string Data { get; }

	/// <summary>
	/// Gets the site title.
	/// </summary>
	public string? Title { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; }

	/// <summary>
	/// Processes this response
	/// </summary>
	public override Task<ControllerResponseResult> Process()
	{
		Context.Response.StatusCode = StatusCode;

		DataCollector.Add(Data);

		if (!string.IsNullOrEmpty(Title))
			DataCollector.AddTitle(Title);

		return Task.FromResult(ControllerResponseResult.Default);
	}
}