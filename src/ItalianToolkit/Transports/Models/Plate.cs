namespace ItalianToolkit.Transports.Models
{
    public class Plate
    {
        public string PlateNumber { get; set; }

        public PlateType Type { get; set; } = PlateType.Unknown;

        public bool IsPlateRecognized => Type != PlateType.Unknown;
    }
}
