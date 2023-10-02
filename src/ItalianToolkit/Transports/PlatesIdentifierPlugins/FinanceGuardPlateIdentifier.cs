using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class FinanceGuardPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.FinanceGuard,
                @"^[gG][dD][iI][fF]\d{3}[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\b$"
            },{
                PlateType.FinanceGuardMotorbike,
                @"^[gG].?[dD][iI][fF].?[1][0-1]\d{3}\b$"
            },{
                PlateType.FinanceGuardTrailer,
               @"^[gG].?[dD][iI][fF].?\d{3}[rR]\b$"
            }
        };
    }
}
