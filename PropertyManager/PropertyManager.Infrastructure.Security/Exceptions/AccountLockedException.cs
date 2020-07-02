using System;

namespace PropertyManager.Infrastructure.Security.Exceptions
{
    public class AccountLockedException : Exception
    {
        public AccountLockedException(string message)
            : base(message)
        {

        }

        public AccountLockedException()
            : this("Account locked.")
        {

        }
    }
}
