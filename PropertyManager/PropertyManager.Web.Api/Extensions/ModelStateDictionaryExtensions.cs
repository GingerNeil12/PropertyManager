using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PropertyManager.Web.Api.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static IDictionary<string, string[]> ConvertToDictionary(
            this ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string[]>();
            foreach (var entry in modelState)
            {
                var propertyName = entry.Key;
                var propertyErrors = entry.Value
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                result.Add(propertyName, propertyErrors);
            }
            return result;
        }
    }
}
