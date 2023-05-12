using ItalianToolkit.People;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ItalianToolkit.Tests.People
{
    internal class FiscalCodeHelperTests

    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FiscalCodeShouldBeValidated()
        {
            {
                var result = FiscalCodeHelper.IsFormallyValid("MRNLSS90H27A271J");

                Assert.IsTrue(result);
            }

            {
                var result = FiscalCodeHelper.IsFormallyValid("    XGNIUX23A07Z110V   ");

                Assert.IsTrue(result);
            }

            {
                var result = FiscalCodeHelper.IsFormallyValid("12345678901"); // temporary fiscal code -> 11 numeric chars

                Assert.IsTrue(result);
            }
        }

        [Test]
        public void FiscalCodeShouldFailValidation()
        {
            {
                var result = FiscalCodeHelper.IsFormallyValid("WrongStringAsFiscalCode");

                Assert.IsFalse(result);
            }

            {
                var result = FiscalCodeHelper.IsFormallyValid("XGN I U X2 3A07 Z110V");

                Assert.IsFalse(result);
            }

            {
                var result = FiscalCodeHelper.IsFormallyValid("123456789012345");

                Assert.IsFalse(result);
            }
        }

        [Test]
        public void FiscalCodeValidationShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.IsFormallyValid(string.Empty);
            });
        }

        [Test]
        public void FiscalCodeShouldBeCalculated()
        {
            // NOTE: The names and all the data used in this tests are fictitious.
            //       No identification with actual persons (living or deceased),
            //       places, buildings, and products is intended or should be inferred.

            {
                var assert = "MRNLSS90H27A271J";
                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", "Alessio", new DateTime(1990, 6, 27), "M", "A271");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "VRDGNN80A10E783B";
                var result = FiscalCodeHelper.CalculateFiscalCode("Verdi", "Giovanni", new DateTime(1980, 1, 10), "M", "E783");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "RSSMRA69H45Z602F";
                var result = FiscalCodeHelper.CalculateFiscalCode("Rossi", "Maria", new DateTime(1969, 6, 5), "F", "z602");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "BLUHUX64T01Z210I";
                var result = FiscalCodeHelper.CalculateFiscalCode("BLU", "HU", new DateTime(1964, 12, 1), "M", "z210");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "MAXJIX17L51Z213S";
                var result = FiscalCodeHelper.CalculateFiscalCode("MA", "JI", new DateTime(2017, 7, 11), "f", "Z213");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "XGNIUX23A07Z110V";
                var result = FiscalCodeHelper.CalculateFiscalCode("XI GONG", "IU", new DateTime(2023, 1, 7), "m", "Z110");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }

            {
                var assert = "NRETMS80A01A271D";
                var result = FiscalCodeHelper.CalculateFiscalCode("neri", "tommaso", new DateTime(1980, 1, 1), "m", "a271");

                TestContext.WriteLine($"TEST: {assert} - {result}");

                Assert.AreEqual(assert, result);
            }
        }

        [Test]
        public void FiscalCodeCalculationShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode(string.Empty, "Alessio", new DateTime(1990, 6, 27), "M", "A271");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode(" ", "Alessio", new DateTime(1990, 6, 27), "M", "A271");
            });

            Assert.Throws<ArgumentException>(() =>
            {

                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", " ", new DateTime(1990, 6, 27), "M", "A271");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", "Alessio", new DateTime(1990, 6, 27), "", "A271");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", "Alessio", new DateTime(1990, 6, 27), "AnyOtherValue", "A271");
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", "Alessio", new DateTime(1990, 6, 27), "M", string.Empty);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.CalculateFiscalCode("Marinelli", "Alessio", new DateTime(1990, 6, 27), "M", "WrongBelfioreFormat");
            });
        }

        [Test]
        public void HomocodiesShouldBeCalculated()
        {
            {
                var result = FiscalCodeHelper.GenerateHomocodies("MRNLSS90H27A271J");

                Assert.IsNotNull(result);

                var expected = new List<string>() {
                    "MRNLSS90H27A27MB",
                    "MRNLSS90H27A2TMN",
                    "MRNLSS90H27ANTMC",
                    "MRNLSS90H2TANTMZ",
                    "MRNLSS90HNTANTMK",
                    "MRNLSS9LHNTANTMV",
                    "MRNLSSVLHNTANTMK",
                    "MRNLSS90H27A27MB",
                    "MRNLSS90H27A2T1V",
                    "MRNLSS90H27AN71Y",
                    "MRNLSS90H2TA271G",
                    "MRNLSS90HN7A271U",
                    "MRNLSS9LH27A271U",
                    "MRNLSSV0H27A271Y"
                };

                Assert.AreEqual(expected.Count, result.Count);

                expected.ForEach(x => { Assert.IsTrue(result.Contains(x)); });
            }
        }

        [Test]
        public void HomocodiesCalculationShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = FiscalCodeHelper.GenerateHomocodies(string.Empty);
            });
        }

        [Test]
        public void HomocodiesShouldFailCalculation()
        {
            {
                var result = FiscalCodeHelper.GenerateHomocodies("WrongString");

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);
            }

            {
                var result = FiscalCodeHelper.GenerateHomocodies("12345678901");  // temporary fiscal code

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);
            }
        }

        [Test]
        public void HomocodyShouldBeValidated()
        {
            {
                var expected = new List<string>() {
                    "MRNLSS90H27A27MB",
                    "MRNLSS90H27A2TMN",
                    "MRNLSS90H27ANTMC",
                    "MRNLSS90H2TANTMZ",
                    "MRNLSS90HNTANTMK",
                    "MRNLSS9LHNTANTMV",
                    "MRNLSSVLHNTANTMK",
                    "MRNLSS90H27A27MB",
                    "MRNLSS90H27A2T1V",
                    "MRNLSS90H27AN71Y",
                    "MRNLSS90H2TA271G",
                    "MRNLSS90HN7A271U",
                    "MRNLSS9LH27A271U",
                    "MRNLSSV0H27A271Y"
                };

                expected.ForEach(x => Assert.IsTrue(FiscalCodeHelper.IsHomocody("MRNLSS90H27A271J", x)));
            }
        }

        [Test]
        public void HomocodyShouldFailValidation()
        {
            {
                var expected = new List<string>() {
                    "WrongString",
                    "12345678901"
                };

                expected.ForEach(x => Assert.IsFalse(FiscalCodeHelper.IsHomocody("MRNLSS90H27A271J", x)));
            }
        }
    }
}