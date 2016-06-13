//-----------------------------------------------------------------------
// <copyright file="HistoricalDescription.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

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
