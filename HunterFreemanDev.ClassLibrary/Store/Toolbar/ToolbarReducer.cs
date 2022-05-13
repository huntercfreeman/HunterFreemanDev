using Fluxor;
using HunterFreemanDev.ClassLibrary.Toolbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Toolbar;

public class ToolbarReducer
{
    [ReducerMethod]
    public static ToolbarState ReduceRegisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        RegisterMainRowToolbarRecordAction registerMainRowToolbarRecordAction)
    {
        var nextToolbarState = 
            new ToolbarState(new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Add(registerMainRowToolbarRecordAction.ToolbarRecordUntyped.ToolbarRecordId, 
            registerMainRowToolbarRecordAction.ToolbarRecordUntyped);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceRegisterSecondaryRowToolbarRecordAction(ToolbarState previousToolbarState,
        RegisterSecondaryRowToolbarRecordAction registerSecondaryRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.SecondaryRowToolbarRecordMap.Add(registerSecondaryRowToolbarRecordAction.ToolbarRecordUntyped.ToolbarRecordId,
            registerSecondaryRowToolbarRecordAction.ToolbarRecordUntyped);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceUnregisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        UnregisterMainRowToolbarRecordAction unregisterMainRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Remove(unregisterMainRowToolbarRecordAction.ToolbarRecordId);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceUnregisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        UnregisterSecondaryRowToolbarRecordAction unregisterSecondaryRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, IToolbarRecordUntyped>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Remove(unregisterSecondaryRowToolbarRecordAction.ToolbarRecordId);

        return nextToolbarState;
    }
}
