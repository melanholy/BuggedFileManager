using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GUI.Application
{
    public class PluginRepo
    {
        private readonly Dictionary<Type, Dictionary<string, object>> Plugins;
        private readonly Type[] KnownPlugins;

        public PluginRepo(Type[] knownPlugins)
        {
            Plugins = new Dictionary<Type, Dictionary<string, object>>();
            KnownPlugins = knownPlugins;
        }

        public void RegisterPlugins(IEnumerable<string> pluginFiles)
        {
            Plugins.Clear();
            foreach (var pluginFile in pluginFiles.Select(Assembly.LoadFile))
                foreach (var pluginType in KnownPlugins)
                {
                    Plugins.Add(pluginType, new Dictionary<string, object>());
                    var openers = pluginFile.GetTypes()
                        .Where(x => x.GetInterface(pluginType.Name) != null)
                        .Select(Activator.CreateInstance);
                    foreach (dynamic opener in openers)
                        foreach (var extension in opener.Extensions)
                            Plugins[pluginType].Add(extension, opener);
                }
        }

        public bool TryGet<TPlugin>(string extension, out TPlugin plugin)
        {
            plugin = default(TPlugin);
            if (!KnownPlugins.Contains(typeof(TPlugin)))
                return false;
            object result;
            Plugins[typeof(TPlugin)].TryGetValue(extension, out result);
            plugin = (TPlugin) result;
            return plugin != null;
        }
    }
}
