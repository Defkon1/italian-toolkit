using System.Text.RegularExpressions;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public abstract class RegexPlateIdentifier : IPlatesIdentifierPlugin
    {
        public virtual PlateType PlateType { get; }
        public virtual string Pattern { get; }

        public virtual Plate TryIdentifyPlate(string plateNumber)
        {
            var testPlateNumber = plateNumber.Trim().ToUpper().Replace(" ", "");

            PlateSearchResult result = new PlateSearchResult();

            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            if (regex.IsMatch(testPlateNumber))
            {

                return new Plate()
                {
                    PlateNumber = plateNumber,
                    Type = PlateType
                };
            }

            return null;
        }
    }
}
