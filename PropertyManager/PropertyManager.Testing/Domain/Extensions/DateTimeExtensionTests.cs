using System;
using NUnit.Framework;
using PropertyManager.Domain.Extensions;

namespace PropertyManager.Testing.Domain.Extensions
{
    public class DateTimeExtensionTests
    {
        [Test]
        public void StandardDateString_IsValidFormat()
        {
            var regex = @"^[0-9]{4}\-[0-9]{2}\-[0-9]{2}$";
            Assert.That(DateTime.Now.ToStandardDateString(), Does.Match(regex));
        }

        [Test]
        public void StandardDateTimeString_IsValidFormat()
        {
            var regex = @"^[0-9]{4}\-[0-9]{2}\-[0-9]{2}\s[0-9]{2}\:[0-9]{2}$";
            Assert.That(DateTime.Now.ToStandardDateTimeString(), Does.Match(regex));
        }
    }
}
