using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsSardiniaPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.ForestryCorpsCagliari,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[cC][aA]\b$"
            },{
                PlateType.ForestryCorpsCarboniaIglesias,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[cC][iI]\b$"
            },{
                PlateType.ForestryCorpsMedioCampidano,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[vV][sS]\b$"
            },{
                PlateType.ForestryCorpsNuoro,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[nN][uU]\b$"
            },{
                PlateType.ForestryCorpsOgliastra,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[oO][gG]\b$"
            },{
                PlateType.ForestryCorpsOlbiaTempio,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[oO][tT]\b$"
            },{
                PlateType.ForestryCorpsOristano,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[oO][rR]\b$"
            },{
                PlateType.ForestryCorpsSassari,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[sS][sS]\b$"
            },{
                PlateType.ForestryCorpsSudSardegna,
                @"^[cC][fF][vV][aA][A-Za-z0-9]{3}[sS][uU]\b$"
            }
        };
    }
}
