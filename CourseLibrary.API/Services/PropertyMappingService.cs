using System;
using System.Collections.Generic;
using System.Linq;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;

namespace CourseLibrary.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string>() {"Id"})},
                {"MainCategory", new PropertyMappingValue(new List<string>(){"MainCategory"})},
                {"Age", new PropertyMappingValue(new List<string>(){"DateOfBirth"}, true)},
                {"Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName"})}
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_authorPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            return (from field
                    in fieldsAfterSplit
                select field.Trim()
                into trimmedField
                let indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase) == -1
                    ? 1
                    : trimmedField.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase)
                select indexOfFirstSpace == 1 
                    ? trimmedField 
                    : trimmedField.Remove(indexOfFirstSpace)).All(propertyName 
                // returns true or false 
                => propertyMapping.ContainsKey(propertyName));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                                $"for <{typeof(TSource)}, {typeof(TDestination)}");
        }
    }
}
