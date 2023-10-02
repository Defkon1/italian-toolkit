using System;
using System.Collections.Generic;
using System.Linq;
using ItalianToolkit.Transports.Models;
using ItalianToolkit.Transports.PlatesIdentifierPlugins;

namespace ItalianToolkit.Transports
{
    public class PlatesIdentifier
    {
        private List<IPlatesIdentifierPlugin> _plugins = new List<IPlatesIdentifierPlugin>();

        public PlatesIdentifier()
        {
            var plugins = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IPlatesIdentifierPlugin).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (var plugin in plugins)
            {
                var pluginInstance = (IPlatesIdentifierPlugin)Activator.CreateInstance(plugin);

                _plugins.Add(pluginInstance);
            }
        }

        public PlateSearchResult TryIdentifyPlate(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                throw new ArgumentException($"'{nameof(plateNumber)}' cannot be null or whitespace.", nameof(plateNumber));
            }

            PlateSearchResult result = new PlateSearchResult();

            foreach (var plugin in _plugins)
            {
                var res = plugin.TryIdentifyPlate(plateNumber);

                if (!(res is null))
                {
                    result.Items.Add(res);
                }
            }

            return result;
        }
    }
}
