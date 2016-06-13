//-----------------------------------------------------------------------
// <copyright file="DumpingProperty.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

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
