using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class LocalPolicePlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.LocalPolice,
                @"^[pP][oO][lL][iI][zZ][iI][aA][lL][oO][cC][aA][lL][eE][yY][A-Za-z][0-9]{3}[A-Za-z]{2}\b$"
            },{
                PlateType.LocalPoliceMotorcycle,
                @"^[pP][oO][lL][iI][zZ][iI][aA][lL][oO][cC][aA][lL][eE][yY][A-Za-z][0-9]{5}\b$"
            },{
                PlateType.LocalPoliceMotorbike,
                @"^[pP][oO][lL][iI][zZ][iI][aA][lL][oO][cC][aA][lL][eE][yY][0-9]{3}[A-Za-z]{2}\b$"
            }
        };
    }
}
