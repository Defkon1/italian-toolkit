namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsAostaValleyPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForestryCorpsAosta;

        public override string Pattern => @"^[cC][fF][A-Za-z0-9]{3}[aA][oO]\b$";
    }
}
