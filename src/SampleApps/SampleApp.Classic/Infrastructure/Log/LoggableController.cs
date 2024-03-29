﻿using System;
using Simplify.Web;

namespace SampleApp.Classic.Infrastructure.Log;

public abstract class LoggableController<T> : AsyncController<T>
	where T : class
{
	protected void Log(string message) => Console.WriteLine(message);
}