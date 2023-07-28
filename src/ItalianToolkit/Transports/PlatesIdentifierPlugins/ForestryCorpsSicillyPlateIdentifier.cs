using System.Collections.Generic;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsSicillyPlateIdentifier : RegexMultiPlateIdentifier
    {
        public override Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>()
        {
            {
                PlateType.ForestryCorpsAgrigento,
                @"^[cC][fF][A-Za-z0-9]{3}[aA][gG]\b$"
            },{
                PlateType.ForestryCorpsCaltanissetta,
                @"^[cC][fF][A-Za-z0-9]{3}[cC][lL]\b$"
            },{
                PlateType.ForestryCorpsCatania,
                @"^[cC][fF][A-Za-z0-9]{3}[cC][tT]\b$"
            },{
                PlateType.ForestryCorpsEnna,
                @"^[cC][fF][A-Za-z0-9]{3}[eE][nN]\b$"
            },{
                PlateType.ForestryCorpsMessina,
                @"^[cC][fF][A-Za-z0-9]{3}[mM][eE]\b$"
            },{
                PlateType.ForestryCorpsPalermo,
                @"^[cC][fF][A-Za-z0-9]{3}[pP][aA]\b$"
            },{
                PlateType.ForestryCorpsRagusa,
                @"^[cC][fF][A-Za-z0-9]{3}[rR][gG]\b$"
            },{
                PlateType.ForestryCorpsSiracusa,
                @"^[cC][fF][A-Za-z0-9]{3}[sS][rR]\b$"
            },{
                PlateType.ForestryCorpsTrapani,
                @"^[cC][fF][A-Za-z0-9]{3}[tT][pP]\b$"
            }
        };
    }
}
