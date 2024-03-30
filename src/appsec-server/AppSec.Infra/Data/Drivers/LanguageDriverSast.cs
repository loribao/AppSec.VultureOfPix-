using AppSec.Domain.Enums;
using AppSec.Domain.Interfaces.IDrivers;

namespace AppSec.Infra.Data.Drivers;

public class LanguageDriverSast : ILanguageDriverSast
{
    public void RunAnalysis(Languages languages, string path, string token, string urlBase, string projectKey, string user, string password)
    {
        if (languages.Equals(Languages.CSharp))
        {
            DotnetDriver dotnetDriver = new DotnetDriver();
            dotnetDriver.InstallDependencies();
            dotnetDriver.Sonarscanner(projectKey, urlBase, token, path);
            dotnetDriver.Builder(path);
            dotnetDriver.SonarscannerEnd(token, path);
        }
        else if (languages.Equals(Languages.Java))
        {
            throw new NotImplementedException();
        }
        else if (languages.Equals(Languages.Python))
        {
            throw new NotImplementedException();
        }
        else if (languages.Equals(Languages.TypeScript))
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
