//-----------------------------------------------------------------------
// <copyright file="ListDescription.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
