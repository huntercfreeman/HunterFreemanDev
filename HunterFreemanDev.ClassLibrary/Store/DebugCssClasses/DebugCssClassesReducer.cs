using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

public class DebugCssClassesReducer
{
    [ReducerMethod]
    public static DebugCssClassesState ReduceSetIsEnabledDebugCssClassRecordAction(DebugCssClassesState previousDebugCssClassesState,
        SetIsEnabledDebugCssClassRecordAction setIsEnabledDebugCssClassRecordAction)
    {
        var nextDebugCssClassesState = new DebugCssClassesState(
            new Dictionary<Guid, ClassLibrary.DebugCssClasses.DebugCssClassRecord>(previousDebugCssClassesState.DebugCssClassRecordMap));

        if(nextDebugCssClassesState.DebugCssClassRecordMap
            .TryGetValue(setIsEnabledDebugCssClassRecordAction.DebugCssClassRecordId, out var previousDebugCssClassRecord))
        {
            nextDebugCssClassesState.DebugCssClassRecordMap[setIsEnabledDebugCssClassRecordAction.DebugCssClassRecordId] = previousDebugCssClassRecord with
            {
                IsEnabled = setIsEnabledDebugCssClassRecordAction.IsEnabled
            };
        }

        return nextDebugCssClassesState;
    }
}
