namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class FireFightersBolzanoPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.FireFightersBolzano;

        public override string Pattern => @"^[vV][fF][fF][wW][A-Za-z0-9]{3}\b$";
    }
}
