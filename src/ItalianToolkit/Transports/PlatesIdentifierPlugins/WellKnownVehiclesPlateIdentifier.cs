using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class WellKnownVehiclesPlateIdentifier : IPlatesIdentifierPlugin
    {
        private struct KnownPlateData
        {
            public KnownPlateData(PlateType type, string details)
            {
                Type = type;
                Details = details;
            }

            public PlateType Type { get; }

            public string Details { get; }
        }

        private readonly Dictionary<string, KnownPlateData> _wellKnownPlates = new Dictionary<string, KnownPlateData>()
        {
            { "EY897ZX", new KnownPlateData(PlateType.LocalPolice, "Ferrari 458 - Polizia Locale di Milano")},
            { "POLIZIALOCALEEY897ZX", new KnownPlateData(PlateType.LocalPolice, "Ferrari 458 - Polizia Locale di Milano")},
            { "POLIZIAM2658", new KnownPlateData(PlateType.StatePolice, "Lamborghini Huracán LP-610")},
            { "POLIZIAH8862", new KnownPlateData(PlateType.StatePolice, "Lamborghini Huracán LP-610")},
            { "POLIZIAE8300", new KnownPlateData(PlateType.StatePolice, "Lamborghini Gallardo")},
            { "POLIZIAF8743", new KnownPlateData(PlateType.StatePolice, "Lamborghini Gallardo")},
        };

        public virtual Plate TryIdentifyPlate(string plateNumber)
        {
            var testPlateNumber = plateNumber.Trim().ToUpper().Replace(" ", "");

            if (_wellKnownPlates.ContainsKey(testPlateNumber))
            {
                var wellKnowPlate = _wellKnownPlates[testPlateNumber];

                return new WellKnownPlate()
                {
                    PlateNumber = plateNumber,
                    Type = wellKnowPlate.Type,
                    Details = wellKnowPlate.Details
                };
            }

            return null;
        }
    }
}
