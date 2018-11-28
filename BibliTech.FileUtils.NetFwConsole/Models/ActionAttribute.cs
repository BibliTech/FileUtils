using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole.Models
{

    [AttributeUsage(AttributeTargets.Class)]
    public class ActionAttribute : Attribute
    {

        public string Name { get; set; }

        public ActionAttribute(string name)
        {
            this.Name = name;
        }

    }

}
