using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class UnitedNationsPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.UnitedNations,
                @"^[uU][nN]\d{3}[A-Za-z]{2}\b$"
            },{
                PlateType.UnitedNationsStaff,
                @"^[uU][nN][pP]\d{3}[A-Za-z]{2}\b$"
            },{
                PlateType.UnitedNationsInTransit,
                @"^[uU][nN][tT]\d{3}[A-Za-z]{2}\b$"
            }
        };
    }
}
