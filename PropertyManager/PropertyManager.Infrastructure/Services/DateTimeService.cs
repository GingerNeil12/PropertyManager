using System;
using PropertyManager.Application.Common.Interfaces;

namespace PropertyManager.Infrastructure.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
