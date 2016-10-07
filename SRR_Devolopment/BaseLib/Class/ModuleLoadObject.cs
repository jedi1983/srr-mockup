using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR_Devolopment.BaseLib.Class
{
    class ModuleLoadObject
    {
        public static Object GetModule(string Name) 
        {
            return System.Windows.Application.LoadComponent(new Uri(Name, UriKind.RelativeOrAbsolute));
        }
    }
}
