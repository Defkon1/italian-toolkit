namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class FireFightersTrentoPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.FireFightersTrento;

        public override string Pattern => @"^[vV][fF][A-Za-z0-9]{3}[tT][nN]\b$";
    }
}
