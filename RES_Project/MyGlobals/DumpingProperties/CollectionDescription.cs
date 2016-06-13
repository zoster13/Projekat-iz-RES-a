//-----------------------------------------------------------------------
// <copyright file="CollectionDescription.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

namespace MyGlobals
{
    public class CollectionDescription
    {
        private string id;
        private int dataset;
        private DumpingPropertyCollection dumpingPropertyCollection = new DumpingPropertyCollection();

        public CollectionDescription()
        {
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

        public DumpingPropertyCollection DumpingPropertyCollection
        {
            get
            {
                return this.dumpingPropertyCollection;
            }

            set
            {
                this.dumpingPropertyCollection = value;
            }
        }
    }
}