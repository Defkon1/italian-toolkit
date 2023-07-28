using System.Collections.Generic;
using System.Linq;

namespace ItalianToolkit.Transports.Models
{
    public class PlateSearchResult
    {
        public bool IsFound => Items.Any(p => p.Type != PlateType.Unknown);

        public List<Plate> Items { get; set; } = new List<Plate> { };
    }
}
