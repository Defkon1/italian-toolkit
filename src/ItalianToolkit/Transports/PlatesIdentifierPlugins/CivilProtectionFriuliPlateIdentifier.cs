namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CivilProtectionFriuliPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.CivilProtectionFriuli;

        public override string Pattern => @"^[pP][cC][A-Za-z0-9]{3}\b$";
    }
}
