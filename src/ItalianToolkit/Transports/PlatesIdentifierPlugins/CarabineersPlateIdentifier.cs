namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CarabineersPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.Carabineers;

        public override string Pattern => @"^[cC]{2}[A-Za-z]{2}[0-9]{3}\b$";
    }
}
