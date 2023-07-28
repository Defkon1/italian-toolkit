using System.Collections.Generic;
using System.Text.RegularExpressions;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public abstract class RegexMultiPlateIdentifier : IPlatesIdentifierPlugin
    {
        public virtual Dictionary<PlateType, string> Patterns => new Dictionary<PlateType, string>();
       
        public virtual Plate TryIdentifyPlate(string plateNumber)
        {
            var testPlateNumber = plateNumber.Trim().ToUpper().Replace(" ", "");

            Plate res = null;

            foreach (var pattern in Patterns)
            {
                Regex regex = new Regex(pattern.Value, RegexOptions.IgnoreCase);
                if (regex.IsMatch(testPlateNumber))
                {
                     res = new Plate()
                    {
                        PlateNumber = plateNumber,
                        Type = pattern.Key
                    };

                    break;
                }
            }

            return res;
        }
    }
}
