using AppSec.Domain.Interfaces.IDrivers;
using System.Diagnostics;

namespace AppSec.Infra.Data.Drivers;

public class DotnetDriver : IDriverDeps
{
    public DotnetDriver()
    {

    }

    public void InstallDependencies()
    {
        var startInfo = new ProcessStartInfo("dotnet", $"tool install --global dotnet-sonarscanner");
        startInfo.UseShellExecute = true;
        Process.Start(startInfo)?.WaitForExit();
    }

    public void Sonarscanner(string SONAR_PROJECT_KEY, string SONAR_HOST_URL, string SONAR_PROJECT_TOKEN, string project_path, string pattern_cov = "test/**/coverage.opencover.xml")
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("dotnet", $"sonarscanner begin /k:{SONAR_PROJECT_KEY} /d:sonar.host.url={SONAR_HOST_URL}  /d:sonar.token={SONAR_PROJECT_TOKEN} /d:sonar.cs.opencover.reportsPaths=\"{pattern_cov}\"");
        startInfo.WorkingDirectory = project_path;
        startInfo.UseShellExecute = true;
        Process.Start(startInfo)?.WaitForExit();
    }
    public void Builder(string project_path)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("dotnet", $"build");
        startInfo.WorkingDirectory = project_path;
        startInfo.UseShellExecute = true;
        Process.Start(startInfo)?.WaitForExit();
    }
    public void SonarscannerEnd(string SONAR_PROJECT_TOKEN, string project_path)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("dotnet", $"sonarscanner end /d:sonar.token={SONAR_PROJECT_TOKEN}");
        startInfo.WorkingDirectory = project_path;
        startInfo.UseShellExecute = true;
        Process.Start(startInfo)?.WaitForExit();
    }
}
