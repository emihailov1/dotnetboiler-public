using BusinessLogic.Helpers;
using BusinessLogic.Services.Helpers;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    public interface IPropertyMappingService
    {
        bool ValidMappingExistsFor<TSource>(string fields);

        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource>();
    }

}
