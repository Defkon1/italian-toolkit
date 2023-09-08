using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ItalianNavyPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.ItalianNavy,
                @"^[mM]{2}[A-Za-z]{2}[0-9]{3}\b$"
            },{
                PlateType.ItalianNavyTrailer,
                @"^[rR][iI][mM][oO][rR][cC][hH][iI][oO][mM]{2}[A-Za-z]{2}[0-9]{3}\b$"
            }
        };
    }
}
