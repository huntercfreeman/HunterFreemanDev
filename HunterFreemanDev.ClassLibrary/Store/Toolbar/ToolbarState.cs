using Fluxor;
using HunterFreemanDev.ClassLibrary.Toolbar;

namespace HunterFreemanDev.ClassLibrary.Store.Toolbar;

[FeatureState]
public record ToolbarState(Dictionary<Guid, ToolbarRecord> MainRowToolbarRecordMap,
    Dictionary<Guid, ToolbarRecord> SecondaryRowToolbarRecordMap)
{
    public ToolbarState() : this(new Dictionary<Guid, ToolbarRecord>(), new Dictionary<Guid, ToolbarRecord>())
    {

    }
}
