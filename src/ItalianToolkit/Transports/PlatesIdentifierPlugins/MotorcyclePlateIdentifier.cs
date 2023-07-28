namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class MotorcyclePlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.Motorcycle;
        public override string Pattern => @"^[B-DF-HJ-NPR-TV-Zb-df-hj-npr-tv-z2-9]{6}\b$";
    }
}
