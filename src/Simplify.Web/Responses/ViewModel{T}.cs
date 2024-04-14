using System.Threading.Tasks;
using Simplify.Templates.Model;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides view model response.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ViewModel{T}" /> class.
/// </remarks>
/// <param name="templateFileName">Name of the template file.</param>
/// <param name="viewModel">The view model.</param>
/// <param name="title">The title.</param>
/// <param name="statusCode">The HTTP response status code.</param>
public class ViewModel<T>(string templateFileName, T viewModel, string? title = null, int statusCode = 200) : ControllerResponse
	where T : class
{

	/// <summary>
	/// Gets the name of the template file.
	/// </summary>
	/// <value>
	/// The name of the template file.
	/// </value>
	public string TemplateFileName { get; } = templateFileName;

	/// <summary>
	/// Gets the model.
	/// </summary>
	/// <value>
	/// The model.
	/// </value>
	public T Model { get; } = viewModel;

	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	/// <value>
	/// The title.
	/// </value>
	public string? Title { get; set; } = title;

	/// <summary>
	/// Gets the HTTP response status code.
	/// </summary>
	/// <value>
	/// The HTTP response status code.
	/// </value>
	public int StatusCode { get; } = statusCode;

	/// <summary>
	/// Processes this response.
	/// </summary>
	public override Task<ControllerResponseResult> ExecuteAsync()
	{
		Context.Response.StatusCode = StatusCode;

		DataCollector.Add(TemplateFactory
			.Load(TemplateFileName)
			.Model(Model)
			.Set());

		if (!string.IsNullOrEmpty(Title))
			DataCollector.AddTitle(Title);

		return Task.FromResult(ControllerResponseResult.Default);
	}
}