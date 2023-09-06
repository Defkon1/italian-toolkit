using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class FireFightersPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.FireFighters,
                @"^[vV][fF][0-9]{5}\b$"
            },{
                PlateType.FireFightersTrailer,
                @"^[vV][fF][rR][0-9]{4}\b$"
            },{
                PlateType.FireFightersTestVehicle,
                @"^[vV][fF][pR][0-9]{5}\b$"
            }
        };
    }
}
