using System;
using SRR_Devolopment.Model;

namespace SRR_Devolopment.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Roland Testing");
            callback(item, null);
        }
    }
}