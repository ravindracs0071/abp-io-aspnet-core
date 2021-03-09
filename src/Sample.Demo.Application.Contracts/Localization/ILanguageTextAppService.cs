
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace Sample.Demo.Localization
{
    public interface ILanguageTextAppService : IApplicationService
    {
        Dictionary<string, string> GetAllValuesFromDatabase(string SourceName, string LanguageName);

        Dictionary<string, string> GetAllValuesFromDatabaseForCulture(string cultureName);

        KeyValuePair<string, string> Get(string cultureName, string name);
    }
}
