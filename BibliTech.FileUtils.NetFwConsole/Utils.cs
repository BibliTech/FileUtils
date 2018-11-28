using BibliTech.FileUtils.NetFwConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole
{

    internal static class Utils
    {

        public static void BackupFile(string filePath, string startingFolder, string backupFolder)
        {
            var newFilePath = GetMovePath(filePath, startingFolder, backupFolder);

            var newFileFolderPath = Path.GetDirectoryName(newFilePath);
            Directory.CreateDirectory(newFileFolderPath);

            File.Copy(filePath, newFilePath, true);
        }

        public static string GetMovePath(string filePath, string startingFolder, string newFolder)
        {
            var relativePath = MakeRelativePath(startingFolder, filePath);

            return Path.Combine(newFolder, relativePath);
        }

        public static string MakeRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        public static IAction GetAction(string name)
        {
            var actions = typeof(Utils).Assembly
                .DefinedTypes
                .Where(q => q.ImplementedInterfaces.Any(p => p == typeof(IAction)))
                .ToList();

            foreach (var action in actions)
            {
                var actionAttribute = action.GetCustomAttributes(typeof(ActionAttribute), false)
                    .FirstOrDefault()
                    as ActionAttribute;

                if (actionAttribute.Name == name)
                {
                    return action.GetConstructor(new Type[0])
                        .Invoke(new object[0]) 
                        as IAction;
                }
            }

            throw new ArgumentException($"{name} action not found.");
        }

        public static void ScanFiles(string folder, string pattern, Action<string> onFileFound)
        {
            pattern = pattern ?? "*.*";

            var files = Directory.GetFiles(folder, pattern);

            foreach (var file in files)
            {
                onFileFound?.Invoke(file);
            }

            var subFolders = Directory.GetDirectories(folder);
            foreach (var subFolder in subFolders)
            {
                ScanFiles(subFolder, pattern, onFileFound);
            }
        }

        public static T ReadJsonFile<T>(string file, bool throwIfNotFound = true)
        {
            if (!File.Exists(file))
            {
                if (throwIfNotFound)
                {
                    throw new IOException("File not found: " + file);
                }
                else
                {
                    return default(T);
                }
            }

            var content = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<T>(content);
        }

    }

}
