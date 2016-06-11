using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobals.HistoricalProperties
{
    [Serializable]
    public class HistoricalDescription
    {
        private string id;
        private int dataset;
        private List<HistoricalProperty> historicalProperties;

        public HistoricalDescription()
        {
            historicalProperties = new List<HistoricalProperty>();
        }

        public string ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public int Dataset
        {
            get
            {
                return this.dataset;
            }
            set
            {
                this.dataset = value;
            }
        }

        public List<HistoricalProperty> HistoricalProperties
        {
            get
            {
                return historicalProperties;
            }
            set
            {
                this.historicalProperties = value;
            }
        }
    }
}
