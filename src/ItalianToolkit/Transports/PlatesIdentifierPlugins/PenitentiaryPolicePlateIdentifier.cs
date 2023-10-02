namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class PenitentiaryPolicePlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.PenitentiaryPolice;
        public override string Pattern => @"^[pP][oO][lL][iI][zZ][iI][aA][pP][eE][nN][iI][tT][eE][nN][zZ][iI][aA][rR][iI][aA]\d{3}[A-Za-z]{2}\b$";
    }
}
