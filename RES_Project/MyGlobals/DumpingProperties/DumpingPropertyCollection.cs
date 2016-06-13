//-----------------------------------------------------------------------
// <copyright file="DumpingPropertyCollection.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System.Collections.Generic;

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
