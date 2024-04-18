using System;

namespace Simplify.Web.Diagnostics;

/// <summary>
/// Provides the catched exceptions delegate.
/// </summary>
/// <param name="e">The exception.</param>
public delegate void ExceptionEventHandler(Exception e);