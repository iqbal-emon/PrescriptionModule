
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Runtime.Loader;
using PluginDIService.PluginDependencyRepository;

namespace AuthenticationSystem.Application.PluginLoaderService
{
    public static class PluginLoader
    {
        /// <summary>
        /// Assemblies loaded for plugins - shared so DI and MVC use the same type identities.
        /// Loading the same DLL twice (LoadFrom vs LoadFromAssemblyPath) causes "Unable to resolve service" on other machines.
        /// </summary>
        private static List<Assembly>? _loadedPluginAssemblies;

        /// <param name="pluginBasePath">App output directory (e.g. bin/Debug/net8.0) where project-reference DLLs like Degree, Doctor are copied.</param>
        /// <param name="pluginsSubfolder">Optional Plugins subfolder; can be same as pluginBasePath for single-path.</param>
        public static void LoadPlugins(IServiceCollection services, string pluginBasePath, string? pluginsSubfolder = null)
        {
            var processedAssemblies = new HashSet<Assembly>();
            var pluginAssemblies = new List<Assembly>();
            var entryAssembly = Assembly.GetEntryAssembly();
            var entryName = entryAssembly?.GetName().Name ?? "AuthenticationSystem";

            if (string.IsNullOrEmpty(pluginsSubfolder))
                pluginsSubfolder = Path.Combine(pluginBasePath, "Plugins");

            // 1. Load from Plugins subfolder if it exists and has DLLs
            if (Directory.Exists(pluginsSubfolder))
            {
                var resolver = new AssemblyDependencyResolver(pluginsSubfolder);
                var assemblyPath = resolver.ResolveAssemblyToPath(new AssemblyName("HtmlAgilityPack"));
                if (assemblyPath != null)
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);

                var dllFiles = Directory.GetFiles(pluginsSubfolder, "*.dll", SearchOption.AllDirectories)
                    .GroupBy(Path.GetFileNameWithoutExtension)
                    .Select(g => g.OrderByDescending(File.GetCreationTime).First())
                    .ToList();

                foreach (var dll in dllFiles)
                {
                    try
                    {
                        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                        pluginAssemblies.Add(assembly);
                    }
                    catch (FileLoadException ex)
                    {
                        Console.WriteLine($"Skipping already loaded assembly: {dll}. Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(pluginsSubfolder);
            }

            // 2. Load from app output directory (Degree.dll, Doctor.dll from project references)
            if (Directory.Exists(pluginBasePath))
            {
                var baseDlls = Directory.GetFiles(pluginBasePath, "*.dll", SearchOption.TopDirectoryOnly)
                    .GroupBy(Path.GetFileNameWithoutExtension)
                    .Select(g => g.First())
                    .Where(f => !string.Equals(Path.GetFileNameWithoutExtension(f), entryName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var dll in baseDlls)
                {
                    var name = Path.GetFileNameWithoutExtension(dll);
                    if (pluginAssemblies.Any(a => string.Equals(a.GetName().Name, name, StringComparison.OrdinalIgnoreCase)))
                        continue;
                    try
                    {
                        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                        pluginAssemblies.Add(assembly);
                    }
                    catch (FileLoadException ex)
                    {
                        Console.WriteLine($"Skipping already loaded assembly: {dll}. Error: {ex.Message}");
                    }
                }
            }

            _loadedPluginAssemblies = pluginAssemblies;

            foreach (var assembly in pluginAssemblies)
                RegisterPluginServices(services, assembly, processedAssemblies);

            // 3. Any already-loaded assemblies that implement IPlugin (e.g. loaded as dependencies)
            foreach (var assembly in AssemblyLoadContext.Default.Assemblies)
            {
                if (assembly == entryAssembly || processedAssemblies.Contains(assembly))
                    continue;
                if (assembly.IsDynamic || string.IsNullOrEmpty(assembly.Location))
                    continue;
                try
                {
                    RegisterPluginServices(services, assembly, processedAssemblies);
                }
                catch (ReflectionTypeLoadException) { }
            }
        }

        private static void RegisterPluginServices(IServiceCollection services, Assembly assembly, HashSet<Assembly> processedAssemblies)
        {
            if (processedAssemblies.Contains(assembly))
                return;
            try
            {
                var pluginTypes = assembly.GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                foreach (var type in pluginTypes)
                {
                    if (Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        plugin.RegisterServices(services);
                        processedAssemblies.Add(assembly);
                        return;
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                // Skip assemblies that fail to load types
            }
        }

        public static void LoadPlugin(IServiceCollection services, string pluginFolder)
        {
            var mvcBuilder = services.AddControllers();
            var partManager = new ApplicationPartManager();

            // Use the same assemblies already loaded in LoadPlugins so DI and controller types match
            if (_loadedPluginAssemblies != null && _loadedPluginAssemblies.Count > 0)
            {
                foreach (var assembly in _loadedPluginAssemblies)
                {
                    partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            }
            else
            {
                // Fallback if LoadPlugins was not called or folder was missing
                if (!Directory.Exists(pluginFolder))
                {
                    Directory.CreateDirectory(pluginFolder);
                }

                var dllFiles = Directory.GetFiles(pluginFolder, "*.dll", SearchOption.AllDirectories)
                    .GroupBy(Path.GetFileNameWithoutExtension)
                    .Select(g => g.OrderByDescending(File.GetCreationTime).First())
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
            }

            mvcBuilder.ConfigureApplicationPartManager(m =>
            {
                foreach (var part in partManager.ApplicationParts)
                {
                    m.ApplicationParts.Add(part);
                }
            });
        }
    }
}