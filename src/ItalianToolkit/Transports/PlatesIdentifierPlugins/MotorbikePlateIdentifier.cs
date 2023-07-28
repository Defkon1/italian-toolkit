using System.Text.RegularExpressions;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class MotorbikePlateIdentifier : IPlatesIdentifierPlugin
    {
        public PlateType PlateType => PlateType.Motorbike;
        public string Pattern => @"^(?!EE)(?!Ee)(?!eE)(?!ee)[A-HJ-NPR-TV-Za-hj-npr-tv-z]{2}\d{5}\b$";

        public virtual Plate TryIdentifyPlate(string plateNumber)
        {
            var testPlateNumber = plateNumber.Trim().ToUpper().Replace(" ", "");

            PlateSearchResult result = new PlateSearchResult();

            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            if (regex.IsMatch(testPlateNumber))
            {
                // TODO: match first group, and exclude it if ambigouos with old provinces codes
                //       e.g. AN12345 is formally valid, but can't use AN -> old Ancona code

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
