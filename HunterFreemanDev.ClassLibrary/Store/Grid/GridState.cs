using Fluxor;
using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

[FeatureState]
public record GridState(Dictionary<Guid, GridRecord> GridRecordMap, List<List<GridRecord>> GridRecords)
{
    private GridState() : this(new Dictionary<Guid, GridRecord>(), new List<List<GridRecord>>())
    {
    }
}
