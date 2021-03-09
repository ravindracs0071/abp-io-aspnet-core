using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Sample.Demo.Localization
{
    public class LanguageTextAppService : ApplicationService, ILanguageTextAppService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<ApplicationLanguageText, long> _languageRepository;
        public LanguageTextAppService(IRepository<ApplicationLanguageText, long> languageRepository, ICurrentUser currentUser)
        {
            _currentUser = currentUser;
            _languageRepository = languageRepository;
        }

        [UnitOfWork(IsDisabled = false)]
        public Dictionary<string, string> GetAllValuesFromDatabase(string sourceName, string languageName)
        {
            var languageTexts = _languageRepository.GetListAsync().Result;
            if (_currentUser.TenantId != null && _currentUser.TenantId.HasValue.Equals(true)) 
            {
                return languageTexts.Where(l => l.Source == sourceName && l.LanguageName == languageName && l.TenantId == _currentUser.TenantId)?.ToDictionary(l => l.Key, l => l.Value);
            }
            else 
            {
                return languageTexts.Where(l => l.Source == sourceName && l.LanguageName == languageName)?.ToDictionary(l => l.Key, l => l.Value);
            }
        }

        [UnitOfWork(IsDisabled = false)]
        public Dictionary<string, string> GetAllValuesFromDatabaseForCulture(string cultureName)
        {
            var languageTexts = _languageRepository.GetListAsync().Result;
            if (_currentUser.TenantId != null && _currentUser.TenantId.HasValue.Equals(true))
            {
                return languageTexts.Where(l => l.LanguageName == cultureName && l.TenantId == _currentUser.TenantId)?.ToDictionary(l => l.Key, l => l.Value);
            }
            else
            {
                return languageTexts.Where(l => l.LanguageName == cultureName)?.ToDictionary(l => l.Key, l => l.Value);
            }
        }

        [UnitOfWork(IsDisabled = false)]
        public KeyValuePair<string, string> Get(string cultureName, string name)
        {
            if (_currentUser.TenantId != null && _currentUser.TenantId.HasValue.Equals(true))
            {
                var languageTexts = _languageRepository.FirstOrDefault(l => l.LanguageName == cultureName && l.Key == name && l.TenantId == _currentUser.TenantId);
                return new KeyValuePair<string, string>(languageTexts?.Key, languageTexts?.Value);
            }
            else
            {
                var languageTexts = _languageRepository.FirstOrDefault(l => l.LanguageName == cultureName && l.Key == name);
                return new KeyValuePair<string, string>(languageTexts?.Key, languageTexts?.Value);
            }
        }
    }
}
