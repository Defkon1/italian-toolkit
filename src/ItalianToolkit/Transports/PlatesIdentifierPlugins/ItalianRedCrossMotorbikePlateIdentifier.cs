namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class ItalianRedCrossMotorbikePlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.ItalianRedCrossMotorbike;

        public override string Pattern => @"^[cC][rR][iI]\d{3,4}\b$";
    }
}
