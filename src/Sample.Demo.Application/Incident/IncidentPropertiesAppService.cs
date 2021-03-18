using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Demo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Reflection;
using System.Reflection;
using Volo.Abp.Users;

namespace Sample.Demo.Incident
{
    public class IncidentPropertiesAppService : DemoAppService
    {
        private readonly ICurrentUser _currentUser;

        public IncidentPropertiesAppService(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public Dictionary<string, string> Get()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(CreateIncidentDetailDto).GetProperties();
            foreach (var property in propertyInfos)
            {
                config.Add(property.Name, "");
            }
            return config;
        }
    }

    public class Props 
    {
        
    }
}
