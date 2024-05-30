using System;
using System.Text.Json;
using System.Threading.Tasks;
using Simplify.Web.Model.Validation;

namespace Simplify.Web.Model.Binding.Binders;

/// <summary>
/// Provides HTTP request JSON data to object binding using System.Text.Json
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JsonModelBinder"/> class.
/// </remarks>
public class JsonModelBinder : IModelBinder
{
	public static JsonSerializerOptions? Options { get; set; }

	/// <summary>
	/// Binds the model asynchronously.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	/// <param name="args">The <see cref="ModelBinderEventArgs{T}" /> instance containing the event data.</param>
	/// <exception cref="ModelValidationException">
	/// JSON request body is null or empty.
	/// or
	/// Error deserializing JSON model. " + e.Message
	/// </exception>
	/// <exception cref="InvalidOperationException">Deserialized model is null.</exception>
	public async Task BindAsync<T>(ModelBinderEventArgs<T> args)
	{
		if (args.Context.Request.ContentType == null || !args.Context.Request.ContentType.Contains("application/json"))
			return;

		await args.Context.ReadRequestBodyAsync();

		if (string.IsNullOrEmpty(args.Context.RequestBody))
			throw new ModelValidationException("JSON request body is null or empty.");

		try
		{
			args.SetModel(JsonSerializer.Deserialize<T>(args.Context.RequestBody, Options)
						  ?? throw new InvalidOperationException("Deserialized model is null."));
		}
		catch (JsonException e)
		{
			throw new ModelValidationException("Error deserializing JSON model. " + e.Message);
		}
		catch (NotSupportedException e)
		{
			throw new ModelValidationException("Error deserializing JSON model. " + e.Message);
		}
	}
}