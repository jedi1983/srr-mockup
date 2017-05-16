using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;


namespace SRR_Devolopment.Services
{
    public interface IPeriodDataService
    {
        ICollection<USP_CGL_KP_M_Period_Status_Result> GetDataGridData();

        bool newPeriod(DateTime getData, ref string message, string user);

        bool closePeriod(DateTime getData, ref string message, string user);

        bool reOpenPeriod(DateTime getData, ref string message, string user);

    }
}
