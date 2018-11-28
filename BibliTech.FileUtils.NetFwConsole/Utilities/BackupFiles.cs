using BibliTech.FileUtils.NetFwConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole.Utilities
{

    [Action(nameof(BackupFiles))]
    public class BackupFiles : IAction
    {

        public void Execute(ScriptOptions scriptOptions)
        {
            const string backupFolder = "Backup";
            var options = scriptOptions.BackupFilesOptions;

            var workingFolder = options.Folder;
            Utils.ScanFiles(workingFolder, options.Filter, filePath =>
            {
                Console.Write(filePath + " - ");
                var outputFilePath = Utils.BackupFile(filePath, workingFolder, backupFolder);
                Console.WriteLine(outputFilePath);
            });
        }

    }

    public class BackupFilesOptions : BaseOptions { }

}
