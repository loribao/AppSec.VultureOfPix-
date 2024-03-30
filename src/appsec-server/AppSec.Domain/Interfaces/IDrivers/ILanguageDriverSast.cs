using AppSec.Domain.Enums;

namespace AppSec.Domain.Interfaces.IDrivers;

public interface ILanguageDriverSast
{
    public void RunAnalysis(Languages languages, string path, string token, string urlBase, string projectKey, string user, string password);
}
