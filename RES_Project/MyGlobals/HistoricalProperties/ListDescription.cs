using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGlobals.HistoricalProperties
{
    [Serializable, XmlRoot("ListDescription")]
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
