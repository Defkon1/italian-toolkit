namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForeignExcursionistsPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForeignExcursionists;
        public override string Pattern => @"^[eE][eE]\d{3}[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\b$";
    }
}
