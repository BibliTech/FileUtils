using BibliTech.FileUtils.NetFwConsole.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole.Models
{

    public class ScriptOptions
    {

        public string Action { get; set; }

        public AddToFilesOptions AddToFilesOptions { get; set; }
        public BackupFilesOptions BackupFilesOptions { get; set; }

    }

    public class BaseOptions
    {

        public bool Backup { get; set; } = true;
        public string Folder { get; set; }
        public string Filter { get; set; } = "*.*";

    }

}
