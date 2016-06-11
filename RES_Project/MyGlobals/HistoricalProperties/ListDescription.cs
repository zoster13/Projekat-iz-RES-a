using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobals.HistoricalProperties
{
    [Serializable]
    public class ListDescription
    {
        List<HistoricalDescription> listHistoricalDesc;
        
        public ListDescription()
        {
            listHistoricalDesc = new List<HistoricalDescription>();
        }

        public List<HistoricalDescription> ListHistoricalDesc
        {
            get
            {
                return this.listHistoricalDesc;
            }
            set
            {
                this.listHistoricalDesc = value;
            }
        }
    }
}
