using ItalianToolkit.Transports.Models;
using System.Collections.Generic;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class SovereignMilitaryOrderOfMaltaPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.SovereignMilitaryOrderOfMalta,
                @"^[sS][mM][oO][mM]\d{3}\b$"
            },{
                PlateType.SovereignMilitaryOrderOfMaltaMotorbike,
                @"^[sS][mM][oO][mM]{2}\d{3}\b$"
            }
        };
    }
}
