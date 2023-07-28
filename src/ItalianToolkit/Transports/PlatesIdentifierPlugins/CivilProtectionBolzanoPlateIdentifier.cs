namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class CivilProtectionBolzanoPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.CivilProtectionBolzano;

        public override string Pattern => @"^[pP][cC][zZ][sS][A-Za-z0-9]{3}\b$";
    }
}
