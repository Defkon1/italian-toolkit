namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CivilProtectionAostaValleyPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.CivilProtectionAosta;

        public override string Pattern => @"^[pP][cC][A-Za-z0-9]{3}\b$";
    }
}
