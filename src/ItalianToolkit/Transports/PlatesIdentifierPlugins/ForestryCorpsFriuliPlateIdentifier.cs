namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsFriuliPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForestryCorpsFriuli;

        public override string Pattern => @"^[cC][fF][A-Za-z0-9]{3}\b$";
    }
}
