using System;
using System.Collections.Generic;
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

        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this(failures, "One or more validation errors occured.")
        {

        }
    }
}
