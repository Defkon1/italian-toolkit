namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ForestryCorpsTrentoPlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ForestryCorpsTrento;

        public override string Pattern => @"^[cC][fF][A-Za-z0-9]{3}[tT][nN]\b$";
    }
}
