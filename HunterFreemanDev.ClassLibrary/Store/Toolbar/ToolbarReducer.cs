using Fluxor;
using HunterFreemanDev.ClassLibrary.Toolbar;

namespace HunterFreemanDev.ClassLibrary.Store.Toolbar;

public class ToolbarReducer
{
    [ReducerMethod]
    public static ToolbarState ReduceRegisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        RegisterMainRowToolbarRecordAction registerMainRowToolbarRecordAction)
    {
        var nextToolbarState = 
            new ToolbarState(new Dictionary<Guid, ToolbarRecord>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, ToolbarRecord>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Add(registerMainRowToolbarRecordAction.ToolbarRecord.ToolbarRecordId, 
            registerMainRowToolbarRecordAction.ToolbarRecord);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceRegisterSecondaryRowToolbarRecordAction(ToolbarState previousToolbarState,
        RegisterSecondaryRowToolbarRecordAction registerSecondaryRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, ToolbarRecord>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, ToolbarRecord>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.SecondaryRowToolbarRecordMap.Add(registerSecondaryRowToolbarRecordAction.ToolbarRecord.ToolbarRecordId,
            registerSecondaryRowToolbarRecordAction.ToolbarRecord);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceUnregisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        UnregisterMainRowToolbarRecordAction unregisterMainRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, ToolbarRecord>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, ToolbarRecord>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Remove(unregisterMainRowToolbarRecordAction.ToolbarRecordId);

        return nextToolbarState;
    }

    [ReducerMethod]
    public static ToolbarState ReduceUnregisterMainRowToolbarRecordAction(ToolbarState previousToolbarState,
        UnregisterSecondaryRowToolbarRecordAction unregisterSecondaryRowToolbarRecordAction)
    {
        var nextToolbarState =
            new ToolbarState(new Dictionary<Guid, ToolbarRecord>(previousToolbarState.MainRowToolbarRecordMap),
                new Dictionary<Guid, ToolbarRecord>(previousToolbarState.SecondaryRowToolbarRecordMap));

        nextToolbarState.MainRowToolbarRecordMap.Remove(unregisterSecondaryRowToolbarRecordAction.ToolbarRecordId);

        return nextToolbarState;
    }
}
