using BibliTech.FileUtils.NetFwConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var options = Utils.ReadJsonFile<ScriptOptions>("options.json", false) ?? new ScriptOptions();

            Console.WriteLine(options.Action);

            var action = Utils.GetAction(options.Action);
            action.Execute(options);
        }

    }

}
