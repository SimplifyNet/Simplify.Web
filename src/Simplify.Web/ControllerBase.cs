#nullable disable

using System;
using Simplify.Templates;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web;

/// <summary>
/// Controllers base class
/// </summary>
public abstract class ControllerBase : ActionModulesAccessor
{
	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public virtual dynamic RouteParameters { get; internal set; }

	#region Response Wrappers

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Content" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <param name="contentType">Type of the content.</param>
	protected Content Content(string content, int statusCode = 200, string contentType = null) => new(content, statusCode, contentType);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Content" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="contentType">>The HTTP response status code.</param>
	/// <param name="statusCode">The status code.</param>
	protected Content Content(string content, string contentType, int statusCode = 200) => new(content, contentType, statusCode);

	/// <summary>
	/// Initializes a new instance of the <see cref="Created" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="contentType">Type of the content.</param>
	protected Created Created(string content, string contentType = "text/plain") => new(content, contentType);

	/// <summary>
	/// Initializes a new instance of the <see cref="File" /> class.
	/// </summary>
	/// <param name="outputFileName">The name of the file.</param>
	/// <param name="contentType">Type of the content.</param>
	/// <param name="data">The data of the file.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException">
	/// </exception>
	protected File File(string outputFileName, string contentType, byte[] data, int statusCode = 200) => new(outputFileName, contentType, data, statusCode);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.InlineTpl"/> class.
	/// </summary>
	/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
	/// <param name="template">The template.</param>
	protected InlineTpl InlineTpl(string dataCollectorVariableName, ITemplate template) => new(dataCollectorVariableName, template);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.InlineTpl"/> class.
	/// </summary>
	/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
	/// <param name="data">The data.</param>
	protected InlineTpl InlineTpl(string dataCollectorVariableName, string data) => new(dataCollectorVariableName, data);

	/// <summary>
	/// Initializes a new instance of the <see cref="NoContent"/> class.
	/// </summary>
	protected NoContent NoContent() => new();

	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	protected Redirect Redirect(string url) => new(url);

	/// <summary>
	/// Redirects the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the navigation.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	protected Redirect Redirect(RedirectionType redirectionType = RedirectionType.DefaultPage, string bookmarkName = null) => new(redirectionType, bookmarkName);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Tpl" /> class.
	/// </summary>
	/// <param name="template">The template.</param>
	/// <param name="title">The site title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	protected Tpl Tpl(ITemplate template, string title = null, int statusCode = 200) => new(template, title, statusCode);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Tpl"/> class.
	/// </summary>
	/// <param name="data">The data for main content variable.</param>
	/// <param name="title">The site title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	protected Tpl Tpl(string data, string title = null, int statusCode = 200) => new(data, title, statusCode);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.Tpl" /> class.
	/// </summary>
	/// <param name="templateFileName">Name of the template file.</param>
	/// <param name="title">The title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <exception cref="ArgumentNullException"></exception>
	protected StaticTpl StaticTpl(string templateFileName, string title = null, int statusCode = 200) => new(templateFileName, title, statusCode);

	/// <summary>
	/// Initializes a new instance of the <see cref="Responses.StatusCode" /> class.
	/// </summary>
	/// <param name="statusCode">The HTTP response status code.</param>
	/// <param name="responseData">The response data.</param>
	/// <param name="contentType">Type of the content.</param>
	protected StatusCode StatusCode(int statusCode, string responseData = null, string contentType = null) => new(statusCode, responseData, contentType);

	/// <summary>
	/// Initializes a new instance of the <see cref="ViewModel{T}" /> class.
	/// </summary>
	/// <param name="templateFileName">Name of the template file.</param>
	/// <param name="viewModel">The view model.</param>
	/// <param name="title">The title.</param>
	/// <param name="statusCode">The HTTP response status code.</param>
	protected ViewModel<T> ViewModel<T>(string templateFileName, T viewModel = null, string title = null, int statusCode = 200)
		where T : class =>
		new(templateFileName, viewModel, title, statusCode);

	#endregion Response Wrappers
}