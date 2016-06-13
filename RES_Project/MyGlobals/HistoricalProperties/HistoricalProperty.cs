//-----------------------------------------------------------------------
// <copyright file="HistoricalProperty.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;

namespace MyGlobals.HistoricalProperties
{
    [Serializable]
    public class HistoricalProperty
    {
        private Codes code;
        private float historicalValue;
        private DateTime time;

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

        public float HistoricalValue
        {
            get
            {
                return this.historicalValue;
            }

            set
            {
                this.historicalValue = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                this.time = value;
            }
        }
        public HistoricalProperty() { }

        public HistoricalProperty(Codes code, float value)
        {
            this.code = code;
            this.historicalValue= value;
        }
    }
}
