using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobals
{
    public class DumpingPropertyCollection
    {
        private HashSet<DumpingProperty> dumpingCollection = new HashSet<DumpingProperty>();

        public DumpingPropertyCollection()
        {
        }
       
        public HashSet<DumpingProperty> DumpingCollection
        {
            get
            {
                return this.dumpingCollection;
            }

            set
            {
                this.dumpingCollection = value;
            }
        }
    }
}
