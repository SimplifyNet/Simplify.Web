using System.ComponentModel;
using Simplify.WindowsServices;

namespace SampleApp.WindowsServiceHosted;

[RunInstaller(true)]
public class ServiceInstaller : ServiceInstallerBase
{
}