
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Runtime.Loader;
using PluginDIService.PluginDependencyRepository;

namespace AuthenticationSystem.Application.PluginLoaderService
{
    public static class PluginLoader
    {
        public static void LoadPlugins(IServiceCollection services, string pluginsPath)
        {
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
                return;
            }

            // Get all DLL files in the Plugins directory
            var pluginAssemblies = Directory.GetFiles(pluginsPath, "*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom)
                .ToList();

            var resolver = new AssemblyDependencyResolver(pluginsPath);
            var assemblyPath = resolver.ResolveAssemblyToPath(new AssemblyName("HtmlAgilityPack"));
            if (assemblyPath != null)
            {
                Assembly.LoadFrom(assemblyPath);
            }


            foreach (var assembly in pluginAssemblies)
            {
                // Find all types that implement IPlugin
                var pluginTypes = assembly.GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                foreach (var type in pluginTypes)
                {
                    // Instantiate the plugin and register its services
                    if (Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        plugin.RegisterServices(services);
                    }
                }
            }
        }


        public static void LoadPlugin(IServiceCollection services, string pluginFolder)
        {
            var mvcBuilder = services.AddControllers();
            var partManager = new ApplicationPartManager();

            // Ensure that the plugin folder exists
            if (!Directory.Exists(pluginFolder))
            {
                Directory.CreateDirectory(pluginFolder);
            }

            // Get the latest DLLs by checking creation time
            var dllFiles = Directory.GetFiles(pluginFolder, "*.dll", SearchOption.AllDirectories)
                                    .GroupBy(Path.GetFileNameWithoutExtension) // Group by assembly name
                                    .Select(g => g.OrderByDescending(File.GetCreationTime).First()) // Take the latest version
                                    .ToList();

            foreach (var dll in dllFiles)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                    partManager.ApplicationParts.Add(new AssemblyPart(assembly));

                    Console.WriteLine($"Loaded: {dll}");
                }
                catch (FileLoadException ex)
                {
                    Console.WriteLine($"Skipping already loaded assembly: {dll}. Error: {ex.Message}");
                }
            }


            mvcBuilder.ConfigureApplicationPartManager(m =>
            {
                // Add each ApplicationPart individually
                foreach (var part in partManager.ApplicationParts)
                {
                    m.ApplicationParts.Add(part);
                }
            });
        }
    }
}