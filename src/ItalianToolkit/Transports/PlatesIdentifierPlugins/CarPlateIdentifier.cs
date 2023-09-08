namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CarPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.Car;
        public override string Pattern => @"^(?!EE)(?!Ee)(?!eE)(?!ee)[A-HJ-NPR-TVY-Za-hj-npr-tvy-z][A-HJ-NPR-TV-Za-hj-npr-tv-z]\d{3}[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\b$";
    }
}
