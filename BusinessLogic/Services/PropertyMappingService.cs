using BusinessLogic.Models;
using BusinessLogic.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

        private Dictionary<string, PropertyMappingValue> userPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                      {"Id", new PropertyMappingValue(new List<string> {"Id"}) },
                      {"Name", new PropertyMappingValue(new List<string> {"FirstName","LastName"}) },
                      {"Email", new PropertyMappingValue(new List<string> {"Email"}) }
            };
        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<User>(userPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource>()
        {
            var matchingMapping = propertyMappings.OfType<PropertyMapping<TSource>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for  <{typeof(TSource)}>");
        }

        public bool ValidMappingExistsFor<TSource>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();

                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                //find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;

        }

    }
}
