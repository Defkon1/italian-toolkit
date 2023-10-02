namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ItalianRedCrossPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ItalianRedCross;

        public override string Pattern => @"^[cC][rR][iI][A-HJ-NPR-TV-Za-hj-npr-tv-z0-9]{5}\b$";
    }
}
