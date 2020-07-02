using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PropertyManager.Infrastructure.Security.Exceptions
{
    public class IdentityValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; private set; }

        public IdentityValidationException(
            IEnumerable<IdentityError> errors,
            string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();

            var errorGroups = errors
                .GroupBy(x => x.Code, x => x.Description)
                .ToList();

            foreach (var errorGroup in errorGroups)
            {
                Errors.Add(errorGroup.Key, errorGroup.ToArray());
            }
        }

        public IdentityValidationException(IEnumerable<IdentityError> errors)
            : this(errors, "One or more validation errors occured.")
        {

        }
    }
}
