namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class StatePolicePlateIdentifier : RegexPlateIdentifier
    {
        public override Models.PlateType PlateType => Models.PlateType.StatePolice;
        public override string Pattern => @"^[pP][oO][lL][iI][zZ][iI][aA][A-Za-z]\d{4}\b$";
    }
}
