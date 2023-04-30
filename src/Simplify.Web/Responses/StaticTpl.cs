using System;

#nullable disable

namespace Simplify.Web.Responses;

/// <summary>
/// Provides template response (loads template and puts it to DataCollector)
/// </summary>
public class StaticTpl : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Tpl" /> class.
	/// </summary>
	/// <param name="templateFileName">Name of the template file.</param>
	/// <param name="title">The title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public StaticTpl(string templateFileName, string title = null, int statusCode = 200)
	{
		if (templateFileName == null)
			throw new ArgumentNullException(nameof(templateFileName));

		TemplateFileName = templateFileName;
		Title = title;
		StatusCode = statusCode;
	}

	/// <summary>
	/// Gets the name of the template file.
	/// </summary>
	/// <value>
	/// The name of the template file.
	/// </value>
	public string TemplateFileName { get; }

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; }

	/// <summary>
	/// Gets the name of the string table title item.
	/// </summary>
	/// <value>
	/// The name of the string table title item.
	/// </value>
	private string Title { get; }

	/// <summary>
	/// Processes this response
	/// </summary>
	public override ControllerResponseResult Process()
	{
		Context.Response.StatusCode = StatusCode;

		DataCollector.Add(TemplateFactory.Load(TemplateFileName));

		if (!string.IsNullOrEmpty(Title))
			DataCollector.AddTitle(Title);

		return ControllerResponseResult.Default;
	}
}