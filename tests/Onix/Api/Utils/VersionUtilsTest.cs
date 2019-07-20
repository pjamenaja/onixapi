using System;
using NUnit.Framework;

using Onix.Api.Utils;

namespace Onix.Test.Api.Utils
{
    public class VersionUtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetVersionNumberTest()
        {
            string version = VersionUtils.GetVersion();
            Assert.AreEqual("1.0.0.0", version, "Version incorrect !!!");         
        }
    }
}