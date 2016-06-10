using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileManager.GUI.Application
{
    public class PluginRepo
    {
        private readonly Dictionary<Type, Dictionary<string, object>> Plugins;
        private readonly Type[] KnownPlugins;

        public PluginRepo(Type[] knownPlugins)
        {
            Plugins = new Dictionary<Type, Dictionary<string, object>>();
            foreach (var plugin in knownPlugins)
                Plugins.Add(plugin, new Dictionary<string, object>());
            KnownPlugins = knownPlugins;
        }

        public void RegisterPlugins(IEnumerable<Assembly> assemblies)
        {
            foreach (var pluginFile in assemblies)
                foreach (var pluginType in KnownPlugins)
                {
                    var plugins = pluginFile.GetTypes()
                        .Where(x => x.GetInterface(pluginType.Name) != null)
                        .Select(Activator.CreateInstance);
                    foreach (dynamic plugin in plugins)
                        foreach (var extension in plugin.Extensions)
                            Plugins[pluginType].Add(extension, plugin);
                }
        }

        public bool TryGet<TPlugin>(string extension, out TPlugin plugin)
        {
            plugin = default(TPlugin);
            if (!KnownPlugins.Contains(typeof(TPlugin)))
                return false;
            object result;
            var d = Plugins[typeof(TPlugin)];
            d.TryGetValue(extension, out result);
            plugin = (TPlugin) result;
            return plugin != null;
        }
    }
}
