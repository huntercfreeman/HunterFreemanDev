using Fluxor;
using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

[FeatureState]
public record GridState(Dictionary<Guid, object?> GridRecordMap, List<List<GridRecord>> GridRecords)
{
    private GridState() : this(new Dictionary<Guid, object?>(), new List<List<GridRecord>>())
    {
    }
}
