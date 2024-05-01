using System;

namespace Simplify.Web.StaticFiles.Context;

public class StaticFileProcessingContext(string relativeFilePath,
	DateTime fileLastModificationTime,
	bool isCached) : IStaticFileProcessingContext
{
	public string RelativeFilePath { get; } = relativeFilePath;

	public DateTime FileLastModificationTime { get; } = fileLastModificationTime;

	public bool IsCached { get; } = isCached;
}