using System;

namespace PropertyManager.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {

        }

        public NotFoundException()
            : this("Resource not found.")
        {

        }
    }
}
