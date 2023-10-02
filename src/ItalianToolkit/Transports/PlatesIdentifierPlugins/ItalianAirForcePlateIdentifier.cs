using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ItalianAirForcePlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.ItalianAirForce,
                @"^[aA][mM]([Aa][Hh][5-9][0-9]{2}|[Aa][Ii][0-9]{3}|[B-Zb-z][A-Za-z][0-9]{3})\b$"
            },{
                PlateType.ItalianAirForceMotorbike,
                @"^[aA][mM]([Aa]\/[6-9][0-9]{3}|[B-Zb-z]\/[0-9]{4})\b$"
            },
        };
    }
}
