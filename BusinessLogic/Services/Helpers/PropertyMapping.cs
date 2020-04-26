using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Helpers
{
    public class PropertyMapping<TSource> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> mappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            this.mappingDictionary = mappingDictionary;
        }
    }
}
