#nullable disable

using System;
using Simplify.Web.Old.Modules;

namespace Simplify.Web.Old.Model.Binding;

/// <summary>
/// Provides model binder event arguments.
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
	/// <value>
	/// The model.
	/// </value>
	public T Model { get; private set; }

	/// <summary>
	/// Gets the context.
	/// </summary>
	/// <value>
	/// The context.
	/// </value>
	public IWebContext Context { get; } = context;

	/// <summary>
	/// Gets a value indicating whether model was bound.
	/// </summary>
	/// <value>
	/// <c>true</c> if current model was bound; otherwise, <c>false</c>.
	/// </value>
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