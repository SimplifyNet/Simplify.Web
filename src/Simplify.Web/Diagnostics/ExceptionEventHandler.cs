using System;

namespace Simplify.Web.Diagnostics;

/// <summary>
/// Catched exceptions delegate
/// </summary>
/// <param name="e">The exception.</param>
public delegate void ExceptionEventHandler(Exception e);