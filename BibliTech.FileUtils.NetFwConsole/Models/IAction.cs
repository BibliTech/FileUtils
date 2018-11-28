using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliTech.FileUtils.NetFwConsole.Models
{

    public interface IAction
    {

        void Execute(ScriptOptions scriptOptions);

    }

}
