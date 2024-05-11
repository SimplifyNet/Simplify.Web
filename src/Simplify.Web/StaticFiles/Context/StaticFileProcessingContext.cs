using System;

namespace Simplify.Web.StaticFiles.Context;

public class StaticFileProcessingContext(string relativeFilePath,
	DateTime lastModificationTime,
	bool isCached) : IStaticFileProcessingContext
{
	public string RelativeFilePath { get; } = relativeFilePath;

	public DateTime LastModificationTime { get; } = lastModificationTime;

	public bool CanBeCached { get; } = isCached;
}