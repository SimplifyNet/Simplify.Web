using System.Threading.Tasks;

namespace Simplify.Web.Model;

/// <summary>
/// Represents a model handler.
/// </summary>
public interface IModelHandler
{
	/// <summary>
	/// Gets the value indicating whether model has been processed (parsed/validated).
	/// </summary>
	bool Processed { get; }

	/// <summary>
	/// Parses the model and validates it asynchronously
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	Task ProcessAsync<T>();

	/// <summary>
	/// Gets the model.
	/// </summary>
	/// <typeparam name="T">The model</typeparam>
	T GetModel<T>();
}