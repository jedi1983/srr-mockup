using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    public interface IProcessDataService
    {
        bool generateIuran(DateTime nowData, ref string message);

        bool balanceCalculation(DateTime nowData,ref string message);

        Collection<CGL_KP_M_Period_H> getPeriod();

    }
}
