using ItalianToolkit.Transports.Models;
using System.Collections.Generic;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ItalianArmyPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.ItalianArmy,
                @"^[eE][iI][A-Za-z]{2}[0-9]{3}\b$"
            },{
                PlateType.ItalianArmyTrailer,
                @"^[rR][iI][mM][oO][rR][cC][hH][iI][oO][eE][iI][A-Za-z]{2}[0-9]{2}\b$"
            },{
                PlateType.ItalianArmyMotorbike,
                @"^[eE][iI][A-Za-z][0-9]{4}\b$"
            },{
                PlateType.ItalianArmyTank,
                @"^[eE][iI][0-8][0-9]{5}\b$"
            }
        };
    }
}
