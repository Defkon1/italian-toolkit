using ItalianToolkit.Transports.Models;
using System.Collections.Generic;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CoastGuardPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.CoastGuardDepartment,
                @"^[cC][pP][1][0-9]{3}\b$"
            },{
                PlateType.CoastGuard,
                @"^[cC][pP][24][0-9]{3}\b$"
            },{
                PlateType.CoastGuardMotorbike,
                @"^[cC][pP][3][0-9]{3}\b$"
            },{
                PlateType.CoastGuardTrailer,
                @"^[cC][pP][0][0-9]{3}[rR]\b$"
            },
        };
    }
}
