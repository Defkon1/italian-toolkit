namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class TestVehiclesPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.TestVehicle;

        public override string Pattern => @"^[A-Za-z0-9]{2}[pP][A-Za-z0-9]{5}\b$";
    }
}
