namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForestryCorps;

        public override string Pattern => @"^[cC][fF][sS]\d{3}[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\b$";
    }
}
