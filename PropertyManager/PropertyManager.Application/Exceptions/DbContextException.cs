using System;

namespace PropertyManager.Application.Exceptions
{
    public class DbContextException : Exception
    {
        public DbContextException(
            string message, 
            Exception ex = null)
            : base(message, ex)
        {

        }
    }
}
