using Fluxor;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
