using Fluxor;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;

namespace HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

[FeatureState]
public record DebugCssClassesState(Dictionary<Guid, DebugCssClassRecord> DebugCssClassRecordMap)
{
    public DebugCssClassesState() : this(new Dictionary<Guid, DebugCssClassRecord>())
    {
        foreach (var debugCssClass in DebugCssClassInitialStates.AllDebugCssClassRecords)
        {
            DebugCssClassRecordMap.Add(debugCssClass.DebugCssClassId, debugCssClass);
        }
    }
}
