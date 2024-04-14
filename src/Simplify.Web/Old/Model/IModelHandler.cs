using System.Threading.Tasks;
using Simplify.DI;

namespace Simplify.Web.Model;

/// <summary>
/// Represent model handler.
/// </summary>
public interface IModelHandler
{
	/// <summary>
	/// Gets a value indicating whether model has been processed (parsed/validated).
	/// </summary>
	bool Processed { get; }

	/// <summary>
	/// Parses model and validates it asynchronously
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	/// <param name="resolver">The resolver.</param>
	/// <returns></returns>
	Task ProcessAsync<T>(IDIResolver resolver);

	/// <summary>
	/// Gets the model.
	/// </summary>
	/// <typeparam name="T">The model</typeparam>
	T GetModel<T>();
}