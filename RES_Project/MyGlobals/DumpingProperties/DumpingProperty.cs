using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobals
{
    public class DumpingProperty
    {
        private Codes code;
        private float dumpingValue;

        public Codes Code
        {
            get
            {
                return this.code;
            }

            set
            {
                this.code = value;
            }
        }

        public float DumpingValue
        {
            get
            {
                return this.dumpingValue;
            }

            set
            {
                this.dumpingValue = value;
            }
        }

        public DumpingProperty()
        {
        }

        public DumpingProperty(Codes code, float value)
        {
            this.code = code;
            this.dumpingValue = value;
        }
    }
}
