using ItalianToolkit.People;
using NUnit.Framework;
using System;

namespace ItalianToolkit.Tests.People
{
    internal class AnprCodeHelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AnprCodeShouldBeValidated()
        {
            var validCodes = new []
            {
                "FL46721JJ",
                "fl46721jj"
            };

            foreach(var code in validCodes)
            {
                var result = AnprCodeHelper.IsFormallyValid(code);

                Assert.IsTrue(result);
            }
        }
        
        [Test]
        public void AnprCodeShouldFailValidation()
        {
            var notValidCodes = new[]
            {
                "WrongCode",
                "Shorter",
                "LongerThanExpected",
                "3edr7y89",
                "POL098231"
            };
            
            foreach (var code in notValidCodes)
            {
                var result = AnprCodeHelper.IsFormallyValid(code);

                Assert.IsFalse(result);
            }
        }
        
        [Test]
        public void AnprCodeValidationShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = AnprCodeHelper.IsFormallyValid(string.Empty);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = AnprCodeHelper.IsFormallyValid(null);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = AnprCodeHelper.IsFormallyValid(" ");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = AnprCodeHelper.IsFormallyValid("POL?98231");
            });
        }
    }
}