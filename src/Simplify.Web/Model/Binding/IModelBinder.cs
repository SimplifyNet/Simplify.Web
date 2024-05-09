using System.Threading.Tasks;

namespace Simplify.Web.Model.Binding;

/// <summary>
/// Represents a model binder.
/// </summary>
public interface IModelBinder
{
	/// <summary>
	/// Binds the model asynchronously.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	/// <param name="args">The <see cref="ModelBinderEventArgs{T}"/> instance containing the event data.</param>
	Task BindAsync<T>(ModelBinderEventArgs<T> args);
}