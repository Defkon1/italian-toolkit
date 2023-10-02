namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class TrailerPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.Trailer;
        public override string Pattern => @"^[X][A-HJ-NPR-TV-Za-hj-npr-tv-z]\d{3}[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\b$";
    }
}
