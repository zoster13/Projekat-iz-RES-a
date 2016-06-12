using MyGlobals;
using MyGlobals.HistoricalProperties;
using System.Collections.Generic;

namespace CommonLibrary
{
    public interface IHistorical
    {
        void WriteToHistory(CollectionDescription CD);

        List<HistoricalProperty> GetChangesForInterval(Codes code);
    }
}
