using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public interface IPlatesIdentifierPlugin
    {
        Plate TryIdentifyPlate(string identifier);
    }
}
