#nullable disable

using System;
using Simplify.Web.Modules.Context;

namespace Simplify.Web.Model.Binding;

/// <summary>
/// Provides the model binder event arguments.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ModelBinderEventArgs{T}"/> class.
/// </remarks>
/// <param name="context">The context.</param>
public class ModelBinderEventArgs<T>(IWebContext context) : EventArgs
{

	/// <summary>
	/// Gets the model.
	/// </summary>
	public T Model { get; private set; }

	/// <summary>
	/// Gets the context.
	/// </summary>
	public IWebContext Context { get; } = context;

	/// <summary>
	/// Gets a value indicating whether model was bound.
	/// </summary>
	public bool IsBound { get; private set; }

	/// <summary>
	/// Sets current model.
	/// </summary>
	/// <param name="model">The model.</param>
	public void SetModel(T model)
	{
		Model = model;
		IsBound = true;
	}
}