//-----------------------------------------------------------------------
// <copyright file="IHistorical.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using MyGlobals;
using MyGlobals.HistoricalProperties;
using System.Collections.Generic;

namespace CommonLibrary
{
    public interface IHistorical
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CD"> Structure which  </param>
        void WriteToHistory(CollectionDescription CD);

        List<HistoricalProperty> GetChangesForInterval(Codes code);

        void ManualWriteToHistory(Codes code, float value);
    }
}
