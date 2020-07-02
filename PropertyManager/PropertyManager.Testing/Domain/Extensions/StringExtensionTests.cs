﻿using NUnit.Framework;
using PropertyManager.Domain.Extensions;

namespace PropertyManager.Testing.Domain.Extensions
{
    public class StringExtensionTests
    {
        [Test]
        public void CapitalizeFirstLetter_WorksOnSingleString()
        {
            var data = "hello";
            Assert.AreEqual("Hello", data.CapitalizeFirstLetter());
        }

        [Test]
        public void GetFirstLetters_WorksWithSingleString()
        {
            var data = "hello";
            Assert.AreEqual("H", data.GetFirstLetter());
        }

        [Test]
        public void GetFirstLetter_WorksWithMultipleStrings()
        {
            var data = "hello world";
            Assert.AreEqual("H W", data.GetFirstLetter());
        }
    }
}
