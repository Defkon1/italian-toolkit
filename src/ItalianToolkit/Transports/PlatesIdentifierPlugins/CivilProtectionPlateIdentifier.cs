using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CivilProtectionPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.CivilProtectionDepartment,
                @"^[dD][pP][cC][A-HJ-NPR-TV-Za-hj-npr-tv-z]\d{4}\b$"
            }
        };
    }
}
