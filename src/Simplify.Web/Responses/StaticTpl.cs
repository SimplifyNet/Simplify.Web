using System;
using System.Threading.Tasks;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides template response (loads template and puts it to DataCollector)
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Tpl" /> class.
/// </remarks>
/// <param name="templateFileName">Name of the template file.</param>
/// <param name="title">The title.</param>
/// <param name="statusCode">The HTTP response status code.</param>
/// <exception cref="ArgumentNullException"></exception>
public class StaticTpl(string templateFileName, string? title = null, int statusCode = 200) : ControllerResponse
{

	/// <summary>
	/// Gets the name of the template file.
	/// </summary>
	/// <value>
	/// The name of the template file.
	/// </value>
	public string TemplateFileName { get; } = templateFileName ?? throw new ArgumentNullException(nameof(templateFileName));

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; } = statusCode;

	/// <summary>
	/// Gets the name of the string table title item.
	/// </summary>
	/// <value>
	/// The name of the string table title item.
	/// </value>
	private string? Title { get; } = title;

	/// <summary>
	/// Processes this response
	/// </summary>
	public override Task<ControllerResponseResult> Process()
	{
		Context.Response.StatusCode = StatusCode;

		DataCollector.Add(TemplateFactory.Load(TemplateFileName));

		if (!string.IsNullOrEmpty(Title))
			DataCollector.AddTitle(Title);

		return Task.FromResult(ControllerResponseResult.Default);
	}
}