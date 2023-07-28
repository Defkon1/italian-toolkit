namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsBolzanoPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForestryCorpsBolzano;

        public override string Pattern => @"^[cC][fF][fF][dD][A-Za-z0-9]{3}\b$";
    }
}
