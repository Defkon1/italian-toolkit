using System;
using System.Linq;
using ItalianToolkit.Transports;
using ItalianToolkit.Transports.Models;
using NUnit.Framework;

namespace ItalianToolkit.Tests.Transports
{
    internal class PlatesHelperTests

    {
        private PlatesIdentifier _platesHelper;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _platesHelper = new PlatesIdentifier();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CarPlateShouldBeValidated()
        {
            var validCarPlates = new[]
            {
                "AA 123BB",
                "AA999 EE",
                "dK 010 DB",
                "LL 333BA",
                "ll333 ba",
                "LL333bA",
                "Ll333bA"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.Car, result.Items.First().Type);
            }
        }

        [Test]
        public void CarPlateShouldFailValidation()
        {
            var notValidCarPlates = new[]
            {
                "A123BB",
                "AA999E",
                "$$123EL",
                "OA555AA",
                "AA333IA",
                "AU333EE",
                "AB 333 IE",
            };

            foreach (var plate in notValidCarPlates)

            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.IsFound);
                Assert.AreEqual(0, result.Items.Count);
            }
        }

        [Test]
        public void MotorcyclesPlateShouldBeValidated()
        {
            // Ciclomotori motocarri e mototrattori di cilindrata inferiore a 50 cm³, e quadricicli leggeri
            var validCarPlates = new[]
            {
                "X 92345",
                "BX9 83C",
                "Bx 98dc",
                "Bx98xc",
                "dd98lc",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.Motorcycle, result.Items.First().Type);
            }
        }

        [Test]
        public void MotorbikesPlateShouldBeValidated()
        {
            // Motocicli, motoveicoli, motocarri, mototrattori e quadricicli di cilindrata superiore a 50 cm³
            var validMotorbikePlates = new[]
            {
                "AX 92345",
                "BX 12345",
                " Bx 4 5 6 7 8",
                "LP 98 87 1"
            };

            foreach (var plate in validMotorbikePlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.Motorbike, result.Items.First().Type);
            }
        }

        [Test]
        public void MotorbikePlateShouldFailValidation()
        {
            // TODO: add filter for old provinces codes
            var notValidMotorbikePlates = new string[]
            {
                //"AN 12345",
                //"MC 87653",
                //"RM 43121",
                //"AP 09841",
                //"PU 123 45",
                //"PS 12678"
            };

            foreach (var plate in notValidMotorbikePlates)

            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.IsFound);
                Assert.AreEqual(0, result.Items.Count);
            }
        }

        [Test]
        public void CivilProtectionAostaAndFriuliPlateShouldBeValidated()
        {
            // Protezione Civile Aosta -> PC A12
            // Protezione Civile Friuli -> PC A12

            var ambiguousCarPlates = new[]
            {
                "PC012",
                "PC A23",
                "pc z  4 5",
                "pc s l9",
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.CivilProtectionAosta));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.CivilProtectionFriuli));
            }
        }

        [Test]
        public void CivilProtectionBolzanoPlateShouldBeValidated()
        {
            // Dipartimento Protezione Civile Bolzano -> PC ZS ABC
            var validCarPlates = new[]
            {
                "PCZS123",
                "PCZS ABC",
                "pczs T45",
                "pc zs l9a",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.CivilProtectionBolzano, result.Items.First().Type);
            }
        }

        [Test]
        public void CivilProtectionDepartmentPlateShouldBeValidated()
        {
            // Dipartimento Protezione Civile -> DPC A0123
            var validCarPlates = new[]
            {
                "DPCA1234",
                "DpC A 4567",
                "dpcA 9087",
                "DPCa0000",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.CivilProtectionDepartment, result.Items.First().Type);
            }
        }

        [Test]
        public void CoastGuardPlateShouldBeValidated()
        {
            // Guardia Costiera - CP 1000 - CP 1999 (Veicoli di rappresentanza)
            // Guardia Costiera - CP 2000 - CP 2999 (Auto, bus, mezzi pesanti)
            // Guardia Costiera - CP 3000 - CP 3999 (Motocicli)
            // Guardia Costiera - CP 4000 - CP 4999 (Auto, bus, mezzi pesanti)
            // Guardia Costiera - CP 0000R - CP 0999R (rimorchi)

            {
                var validCarPlates = new[]
                {
                    "CP 1000",
                    "cp 1999",
                    " c P 12 34"
                };

                foreach (var plate in validCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.AreEqual(1, result.Items.Count);
                    Assert.AreEqual(PlateType.CoastGuardDepartment, result.Items.First().Type);
                }
            }

            {
                var validCarPlates = new[]
                {
                    "CP 2000",
                    "CP 4000"
                };

                foreach (var plate in validCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.AreEqual(1, result.Items.Count);
                    Assert.AreEqual(PlateType.CoastGuard, result.Items.First().Type);
                }
            }

            {
                var ambigouosCarPlates = new[]
                {
                    "cp 2999",
                    " c P 22 34",
                    "cp 4999"
                };

                foreach (var plate in ambigouosCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.CoastGuard));
                }
            }

            var validMotorbikePlates = new[]
            {
                "CP 3000",
                "cp 3120"
            };

            foreach (var plate in validMotorbikePlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.CoastGuardMotorbike, result.Items.First().Type);
            }

            {
                var ambigouosMotorbikePlates = new[]
                {
                    " c P 32 34",
                    "cp 3999"
                };

                foreach (var plate in ambigouosMotorbikePlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.CoastGuardMotorbike));
                }
            }

            var validTrailerPlates = new[]
            {
                "CP 0001R",
                "cp 0999R",
                " c P 0666 r"
            };

            foreach (var plate in validTrailerPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.CoastGuardTrailer, result.Items.First().Type);
            }
        }

        [Test]
        public void ConsularCorpsPlateShouldBeValidated()
        {
            // Consular Corps -> CC 000 AA
            //                     Please note that there is a conflict with normal car plates

            var validCarPlates = new[]
            {
                "CC 000 AA",    // Albania
                "CC123 UA",     // Argentina
                "CC123 VA",     // Argentina (staff)
                "CC 666XG",     // Vatican State City
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ConsularCorps));
            }
        }

        [Test]
        public void DiplomaticCorpsPlateShouldBeValidated()
        {
            // Diplomatic Corps -> CD 000 AA
            //                     Please note that there is a conflict with normal car plates

            var validCarPlates = new[]
            {
                "CD 000 AA",    // Albania
                "CD123 UA",     // Argentina
                "CD 666XG",     // Vatican State City
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.DiplomaticCorps));
            }
        }

        [Test]
        public void FireFightersPlateShouldBeValidated()
        {
            // Vigili del Fuoco -> VF 12345
            // Vigili del Fuoco -> VF R 1234 (trailers)
            // Vigili del Fuoco -> VF P 12345 (test plate - ambiguity with standard test plates)

            var validPlates = new[]
            {
                "VF 12345",
                "V F 1234 5",
                "vf 12 345"
            };

            foreach (var plate in validPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.FireFighters));
            }

            var validTrailersPlates = new[]
            {
                "VF r1235",
                "V F R 1234",
                "vf r12 34"
            };

            foreach (var plate in validTrailersPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FireFightersTrailer, result.Items.First().Type);
            }

            var validTestPlates = new[]
            {
                "VF p12345",
                "V F P 12345",
                "vf p12 345"
            };

            foreach (var plate in validTestPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.FireFightersTestVehicle));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.TestVehicle));
            }
        }

        [Test]
        public void FireFightersBolzanoPlateShouldBeValidated()
        {
            // Vigili del Fuoco della Provincia Autonomia di Bolzano -> VFFW AAA

            var validCarPlates = new[]
            {
                "VFFW123",
                "VffW 1A3",
                "VF FW AB7",
                "vffw999",
                "vFFw00d"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FireFightersBolzano, result.Items.First().Type);
            }
        }

        [Test]
        public void FireFightersTrentoPlateShouldBeValidated()
        {
            // Vigili del Fuoco della Provincia Autonomia di Trento -> VF000TN

            var validCarPlates = new[]
            {
                "VF1B3TN",
                "vFA01TN",
                "vFT42tN",
                "vf07Ctn"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FireFightersTrento, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "VF123TN",
                "vF001TN",
                "vF342tN",
                "vf070tn"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.FireFightersTrento));
            }
        }

        [Test]
        public void FinanceGuardPlateShouldBeValidated()
        {
            // Guardia di Finanza -> GdiF 000 AA (cars)
            // Guardia di Finanza -> GdiF 10000 - GdiF 12000 (motorbikes)
            // Guardia di Finanza -> G.diF. 10000 - G.diF. 12000 (motorbikes - pre-1981 numbering)
            // Guardia di Finanza -> GdiF 000 R (trailers)
            // Guardia di Finanza -> G.diF. 000 R (trailers - pre-1981 numbering)

            var validCarPlates = new[]
            {
                "GdiF 123AA",
                "g di f 453BC",
                "GDIF675rd"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FinanceGuard, result.Items.First().Type);
            }

            var validBikesPlates = new[]
            {
                "GdiF 11345",
                "g dif11999",
                "G DI F 11111",
                "G.diF. 11345",
                "g.dif.11999",
                "G.DIF.11111"
            };

            foreach (var plate in validBikesPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FinanceGuardMotorbike, result.Items.First().Type);
            }

            var notValidBikesPlates = new[]
            {
                "GdiF 12345",
                "gdif 12000",
                "G DI F 09999",
                "G.diF.12345",
                "g.dif.12000",
                "G.DIF.09999"
            };

            foreach (var plate in notValidBikesPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.Items.Any(p => p.Type == PlateType.FinanceGuardMotorbike));
            }

            var validTrailersPlates = new[]
            {
                "GdiF000R",
                "gdif345r",
                "GDIF 999r",
                "G. diF. 000R",
                "g. dif. 345r",
                "G.DIF.999r",
            };

            foreach (var plate in validTrailersPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.FinanceGuardTrailer, result.Items.First().Type);
            }
        }

        [Test]
        public void ForeignExcursionistsPlateShouldBeValidated()
        {
            // Escursionisti Esteri -> EE000AA

            var validCarPlates = new[]
            {
                "EE 123 BB",
                "eE 999EE",
                "ee010 DB",
                "Ee333BA"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForeignExcursionists, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsPlateShouldBeValidated()
        {
            // Corpo Forestale dello Stato (<01/01/2017) -> CFS000AA

            var validCarPlates = new[]
            {
                "CFS 123BB",
                "CfS 999 EE",
                "cf s 010 DB",
                "cfS333BA"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorps, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsPalermoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Palermo -> CF000PA

            var validCarPlates = new[]
            {
                "CF 1B3PA",
                "cFA01 pA",
                "cF T42 Pa",
                "cf07Cpa"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsPalermo, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF 123PA",
                "cF001 pA",
                "cF342Pa",
                "cf070pa"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsPalermo));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "Cf FDA pa"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsPalermo));
            }
        }

        [Test]
        public void ForestyCorpsAgrigentoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Agrigento -> CF000AG

            var validCarPlates = new[]
            {
                "CF 1B3AG",
                "cFA01 aG",
                "cF T42 Ag",
                "cf07Cag"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsAgrigento, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123AG",
                "cF001aG",
                "cF342Ag",
                "cf070ag"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsAgrigento));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAag"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsAgrigento));
            }
        }

        [Test]
        public void ForestyCorpsBolzanoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia Autonomia di Bolzano -> CFFD000AA

            var validCarPlates = new[]
            {
                "CFF D123",
                "CFFD 1A3",
                "CF FD ABC",
                "Cffd999",
                "cfFd00d"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsBolzano, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsAostaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Aosta -> CF000AO

            var validCarPlates = new[]
            {
                "CF 123AO",
                "cf342ao",
                "CFA01AO",
                "cF000aO",
                "CfABCAO"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsAosta, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsCagliariPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Cagliari -> CFVA000CA

            var validCarPlates = new[]
            {
                "CFVA678CA",
                "CFVA1B3 CA",
                "cFva A01 cA",
                "cFVAT42ca",
                "cfVa07CCA"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsCagliari, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsCaltanissettaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Caltanissetta -> CF000CL

            var validCarPlates = new[]
            {
                "CF1B3CL",
                "cF A 01 cL",
                "cFT42Cl",
                "cf07Ccl"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsCaltanissetta, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123CL",
                "cF001cL",
                "cF342Cl",
                "cf070cl"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsCaltanissetta));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAcl"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsCaltanissetta));
            }
        }

        [Test]
        public void ForestyCorpsCataniaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Catania -> CF000CT

            var validCarPlates = new[]
            {
                "CF 1B3CT",
                "cFA01 cT",
                "cFT42Ct",
                "cf07Cct"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsCatania, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF 123 CT",
                "cF001cT",
                "cF342Ct",
                "cf070ct"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsCatania));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAct"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsCatania));
            }
        }

        [Test]
        public void ForestyCorpsEnnaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Enna -> CF000EN

            var validCarPlates = new[]
            {
                "CF1B3EN",
                "cFA01eN",
                "cFT42En",
                "cf07Cen"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsEnna, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123EN",
                "cF001eN",
                "cF342En",
                "cf070en"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsEnna));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAen"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsEnna));
            }
        }

        [Test]
        public void ForestyCorpsMedioCampidanoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Medio Campidano -> CFVA000CA

            var validCarPlates = new[]
            {
                "CFVA678VS",
                "CFVA1B3vs",
                "cFvaA01VS",
                "cFVAT42vS",
                "cfVa07CVs"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsMedioCampidano, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsMessinaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Enna -> CF000EN

            var validCarPlates = new[]
            {
                "CF1B3ME",
                "cFA01mE",
                "cFT42Me",
                "cf07Cme"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsMessina, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123ME",
                "cF001mE",
                "cF342Me",
                "cf070me"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsMessina));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAme"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsMessina));
            }
        }

        [Test]
        public void ForestyCorpsNuoroPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Nuoro -> CFVA000NU

            var validCarPlates = new[]
            {
                "CFVA678NU",
                "CFVA1B3nu",
                "cFvaA01Nu",
                "cFVAT42nU",
                "cfVa07CNU"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsNuoro, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsOgliastraPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia dell'Ogliastra -> CFVA000OG

            var validCarPlates = new[]
            {
                "CFVA678OG",
                "CFVA1B3og",
                "cFvaA01Og",
                "cFVAT42oG",
                "cfVa07COG"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsOgliastra, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsOlbiaTempioPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia dell'Ogliastra -> CFVA000OT

            var validCarPlates = new[]
            {
                "CFVA678OT",
                "CFVA1B3ot",
                "cFvaA01Ot",
                "cFVAT42oT",
                "cfVa07COT"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsOlbiaTempio, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsOristanoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di S -> CFVA000OR

            var validCarPlates = new[]
            {
                "CFVA678OR",
                "CFVA1B3or",
                "cFvaA01Or",
                "cFVAT42oR",
                "cfVa07COR"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsOristano, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsRagusaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Ragusa -> CF000RG

            var validCarPlates = new[]
            {
                "CF1B3RG",
                "cFA01rG",
                "cFT42Rg",
                "cf07Crg"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsRagusa, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123RG",
                "cF001rG",
                "cF342Rg",
                "cf070rg"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsRagusa));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDArg"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsRagusa));
            }
        }

        [Test]
        public void ForestyCorpsSassariPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Sassari -> CFVA000SS

            var validCarPlates = new[]
            {
                "CFVA678SS",
                "CFVA1B3ss",
                "cFvaA01Ss",
                "cFVAT42sS",
                "cfVa07CSS"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsSassari, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsSiracusaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Siracusa -> CF000SR

            var validCarPlates = new[]
            {
                "CF1B3SR",
                "cFA01sR",
                "cFT42Sr",
                "cf07Csr"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsSiracusa, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123SR",
                "cF001sR",
                "cF342Sr",
                "cf070sr"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsSiracusa));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAsr"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsSiracusa));
            }
        }

        [Test]
        public void ForestyCorpsSudSardegnaPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Oristano -> CFVA000SU

            var validCarPlates = new[]
            {
                "CFVA678SU",
                "CFVA1B3su",
                "cFvaA01Su",
                "cFVAT42sU",
                "cfVa07CSU"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsSudSardegna, result.Items.First().Type);
            }
        }

        [Test]
        public void ForestyCorpsTrapaniPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia di Trapani -> CF000TP

            var validCarPlates = new[]
            {
                "CF1B3TP",
                "cFA01tP",
                "cFT42Tp",
                "cf07Ctp"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsTrapani, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123Tp",
                "cF001tP",
                "cF342Tp",
                "cf070tp"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsTrapani));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAtp"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsTrapani));
            }
        }

        [Test]
        public void ForestyCorpsTrentoPlateShouldBeValidated()
        {
            // Corpo Forestale della Provincia Autonomia di Trenot -> CF000TN

            var validCarPlates = new[]
            {
                "CF1B3TN",
                "cFA01TN",
                "cFT42tN",
                "cf07Ctn"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ForestryCorpsTrento, result.Items.First().Type);
            }

            var ambiguousCarPlates = new[]
            {
                "CF123TN",
                "cF001TN",
                "cF342tN",
                "cf070tn"
            };

            foreach (var plate in ambiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.Car));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsTrento));
            }

            var extremelyAmbiguousCarPlates = new[]
            {
                "CfFDAtn"
            };

            foreach (var plate in extremelyAmbiguousCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(2, result.Items.Count);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsBolzano));
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ForestryCorpsTrento));
            }
        }

        [Test]
        public void ItalianAirForcePlateShouldBeValidated()
        {
            // Italian Air Force plates -> AM AA 000 (starting from AM AH 500)

            var validCarPlates = new[]
            {
                "AM AH 500",
                "AM AH 501",
                "AM AH 700",
                "am bg 666",
                "amll123",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianAirForce));
            }

            var notValidCarPlates = new[]
           {
                "AM AH 499",
                "am aa 666",
                "amab123",
            };

            foreach (var plate in notValidCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.IsFound);
                Assert.IsFalse(result.Items.Any(p => p.Type == PlateType.ItalianAirForce));
            }
        }

        [Test]
        public void ItalianAirForceMotorbikePlateShouldBeValidated()
        {
            // Italian Air Force motorbikes plates -> AM A/0000 (starting from AM A/6000)

            var validPlates = new[]
            {
                "AM A/6000",
                "AM A/6001",
                "am b/1234",
                "ama/7700",
            };

            foreach (var plate in validPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianAirForceMotorbike));
            }

            var notValidPlates = new[]
           {
                "AM A/5000",
                "am A/0000"
            };

            foreach (var plate in notValidPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.IsFound);
                Assert.IsFalse(result.Items.Any(p => p.Type == PlateType.ItalianAirForceMotorbike));
            }
        }

        [Test]
        public void ItalianArmyPlateShouldBeValidated()
        {
            // Italian Army plates -> EI AA 000

            var validCarPlates = new[]
            {
                "EI AH 500",
                "EI AH 501",
                "ei AH 700",
                "ei bg 666",
                "eiea637",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianArmy));
            }
        }

        [Test]
        public void ItalianArmyMotorbikePlateShouldBeValidated()
        {
            // Italian Army motorbikes plates -> EI A 0000

            var validPlates = new[]
            {
                "EI A 0000",
                "EI B 0501",
                "ei c 7000",
                "ei d 6966",
                "eih9637",
            };

            foreach (var plate in validPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianArmyMotorbike));
            }
        }

        [Test]
        public void ItalianArmyTankPlateShouldBeValidated()
        {
            // Italian Army tanks and armored vehicles plates -> EI 000000 (range EI 900000 - EI 999999 is reserved for old trailers)

            var validPlates = new[]
            {
                "EI 000000",
                "EI 000 001",
                "ei 123456",
                "ei 7 6 5 4 5 6"
            };

            foreach (var plate in validPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianArmyTank));
            }

            var notValidPlates = new[]
            {
                "EI 900000",
                "EI 999999",
                "ei 950000"
            };

            foreach (var plate in notValidPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.IsFound);
                Assert.IsFalse(result.Items.Any(p => p.Type == PlateType.ItalianArmyTank));
            }
        }

        [Test]
        public void ItalianArmyTrailerPlateShouldBeValidated()
        {
            // Italian Army Trailers plates -> RIMORCHIO EI AA 000

            var validPlates = new[]
            {
                "RIMORCHIO EI AH 50",
                "rimorchio EI AH 51",
                "ri mo rc hio ei AH 70",
                "RiMorChio ei bg 66",
                "rimorchioeiea37",
            };

            foreach (var plate in validPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.ItalianArmyTrailer));
            }
        }

        [Test]
        public void ItalianRedCrossPlateShouldBeValidated()
        {
            // Croce Rossa Italiana -> CRI000AA (>2007), CRI00000 (2002-2007), CRI00000 (1983-2002)
            var validCarPlates = new[]
            {
                "CRI001AA",
                "CRI12345",
                "CRIA118C",
                "CriB138d"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ItalianRedCross, result.Items.First().Type);
            }
        }

        [Test]
        public void ItalianRedCrossMotorbikePlateShouldBeValidated()
        {
            // Croce Rossa Italiana (Motocicli, rimorchi, roulotte e ciclomotori). -> CRI000, CRI0000

            var validCarPlates = new[]
            {
                "CRI001",
                "Cri123",
                "cri111",
                "Cri1234",
                "CRI1034"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.ItalianRedCrossMotorbike, result.Items.First().Type);
            }
        }

        [Test]
        public void PenitentiaryPolicePlateShouldBeValidated()
        {
            // Polizia penitenziaria -> POLIZIA PENITENZIARIA 000 AA
            var validCarPlates = new[]
            {
                "POLIZIA PENITENZIARIA 000 AA",
                "POLIZIAPENITENZIARIA 023be"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.PenitentiaryPolice, result.Items.First().Type);
            }
        }

        [Test]
        public void SovereignMilitaryOrderOfMaltaPlateShouldBeValidated()
        {
            // Sovrano Militare Ordine di Malta-> SMOM 000 (cars)
            // Sovrano Militare Ordine di Malta-> SMOM M 000 (motorbikes)

            var validCarPlates = new[]
            {
                "SMOM123",
                "smom453",
                "sMoM675"
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.SovereignMilitaryOrderOfMalta, result.Items.First().Type);
            }

            var validBikesPlates = new[]
            {
                "SMOMm123",
                "smomM453",
                "sMoMM675"
            };

            foreach (var plate in validBikesPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.SovereignMilitaryOrderOfMaltaMotorbike, result.Items.First().Type);
            }
        }

        [Test]
        public void StatePolicePlateShouldBeValidated()
        {
            // Polizia di Stato -> POLIZIA A 0000
            var validCarPlates = new[]
            {
                "POLIZIA A 0000",
                "POLIZIA k 6677",
                "POL IZIA k6677",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.AreEqual(1, result.Items.Count);
                Assert.AreEqual(PlateType.StatePolice, result.Items.First().Type);
            }
        }


        [Test]
        public void TestPlateShouldBeValidated()
        {
            // Test plates -> X0 P AAAAA

            var validCarPlates = new[]
            {
                "X0 P 1234A",
                "Mi P TO0O0",
                "RAP1n4t0",
            };

            foreach (var plate in validCarPlates)
            {
                var result = _platesHelper.TryIdentifyPlate(plate);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsFound);
                Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.TestVehicle));
            }
        }

        [Test]
        public void UnitedNationsPlateShouldBeValidated()
        {
            // ONU service vehicles -> UN 000 AA
            // ONU staff private vehicles -> UNP 000 AA (ambiguity with standard test plates)
            // ONU in transit vehicles -> UNT 000 AA

            {
                var validCarPlates = new[]
                {
                    "UN 000 AA",
                    "un123zz",
                    "u n 666 o o",
                };

                foreach (var plate in validCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.AreEqual(1, result.Items.Count);
                    Assert.AreEqual(PlateType.UnitedNations, result.Items.First().Type);
                }
            }

            {
                var validCarPlates = new[]
                {
                    "UNP 000 AA",
                    "unp123zz",
                    "u n p 666 o o",
                };

                foreach (var plate in validCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.AreEqual(2, result.Items.Count);
                    Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.UnitedNationsStaff));
                    Assert.IsTrue(result.Items.Any(p => p.Type == PlateType.TestVehicle));
                }
            }

            {
                var validCarPlates = new[]
                {
                    "UNT 000 AA",
                    "unt123zz",
                    "u n t666 o o",
                };

                foreach (var plate in validCarPlates)
                {
                    var result = _platesHelper.TryIdentifyPlate(plate);

                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.IsFound);
                    Assert.AreEqual(1, result.Items.Count);
                    Assert.AreEqual(PlateType.UnitedNationsInTransit, result.Items.First().Type);
                }
            }
        }

        [Test]
        public void PlatesHelperShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = _platesHelper.TryIdentifyPlate(string.Empty);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = _platesHelper.TryIdentifyPlate("   ");
            });
        }

        /*

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
   }*/
    }
}