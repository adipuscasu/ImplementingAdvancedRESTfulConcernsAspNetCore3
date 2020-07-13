using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>();

        /// <summary>
        /// Returns true if all the source fields are mapped to destination fields
        /// </summary>
        /// <typeparam name="TSource">source fields</typeparam>
        /// <typeparam name="TDestination">destination fields</typeparam>
        /// <param name="fields">string containing comma separated field names</param>
        /// <returns></returns>
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}