using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace PropertyManager.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; private set; }

        public ValidationException(
            IEnumerable<ValidationFailure> failures,
            string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
            var failureGroups = failures
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                .ToList();

            foreach (var fg in failureGroups)
            {
                Errors.Add(fg.Key, fg.ToArray());
            }
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this(failures, "One or more validation errors occured.")
        {

        }
    }
}
