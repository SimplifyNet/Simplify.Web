using System;
using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the template response (puts data to DataCollector specified variable).
/// </summary>
public class InlineTpl : ControllerResponse
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InlineTpl"/> class.
	/// </summary>
	/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
	/// <param name="template">The template.</param>
	public InlineTpl(string? dataCollectorVariableName, ITemplate? template)
	{
		if (string.IsNullOrEmpty(dataCollectorVariableName))
			throw new ArgumentNullException(nameof(dataCollectorVariableName));

		DataCollectorVariableName = dataCollectorVariableName!;

		if (template != null)
			Data = template.Get();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="InlineTpl"/> class.
	/// </summary>
	/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
	/// <param name="data">The data.</param>
	public InlineTpl(string? dataCollectorVariableName, string data)
	{
		if (string.IsNullOrEmpty(dataCollectorVariableName))
			throw new ArgumentNullException(nameof(dataCollectorVariableName));

		DataCollectorVariableName = dataCollectorVariableName!;
		Data = data;
	}

	/// <summary>
	/// Gets the name of the data collector variable.
	/// </summary>
	public string DataCollectorVariableName { get; }

	/// <summary>
	/// Gets the data.
	/// </summary>
	public string? Data { get; }

	/// <summary>
	/// Executes this response
	/// </summary>
	public override Task<ControllerResponseResult> ExecuteAsync()
	{
		DataCollector.Add(DataCollectorVariableName, Data);

		return Task.FromResult(ControllerResponseResult.Default);
	}
}