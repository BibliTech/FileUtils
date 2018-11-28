using BibliTech.FileUtils.NetFwConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole.Utilities
{

    [Action(nameof(AddToFiles))]
    public class AddToFiles : IAction
    {

        public void Execute(ScriptOptions scriptOptions)
        {
            var options = scriptOptions.AddToFilesOptions;

            var workingFolder = options.Folder;
            const string backupFolder = "Backup";
            Utils.ScanFiles(workingFolder, options.Filter, filePath =>
            {
                var fileContent = File.ReadAllText(filePath, Encoding.UTF8);

                if (options.UniqueAppend)
                {
                    if (fileContent.Contains(options.Content))
                    {
                        return;
                    }
                }

                if (options.EnsureNewLine)
                {
                    if (fileContent.Length > 0 && !fileContent.EndsWith(Environment.NewLine))
                    {
                        fileContent += Environment.NewLine;
                    }
                }

                fileContent += options.Content;

                if (options.Backup)
                {
                    Utils.BackupFile(filePath, workingFolder, backupFolder);
                }

                File.WriteAllText(filePath, fileContent, Encoding.UTF8);
            });
        }

    }

    public class AddToFilesOptions : BaseOptions
    {

        public string Content { get; set; }
        public bool UniqueAppend { get; set; }
        public bool EnsureNewLine { get; set; }

    }

}
