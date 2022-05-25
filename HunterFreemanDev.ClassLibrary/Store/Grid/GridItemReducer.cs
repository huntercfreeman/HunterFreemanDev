using Fluxor;
using HunterFreemanDev.ClassLibrary.ConstructorAction;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public class GridItemReducer
{
    [ReducerMethod]
    public static GridItemRecordsState ReduceRegisterGridTabContainerRecordAction(GridItemRecordsState previousGridItemRecordsState,
        RegisterGridTabContainerRecordAction registerGridTabContainerRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            registerGridTabContainerRecordAction.GridItemRecordKey);

        return nextGridItemRecordsState;
    }
    
    [ReducerMethod]
    public static GridItemRecordsState ReduceAddGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        AddGridTabRecordAction addGridTabRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            addGridTabRecordAction.GridItemRecordKey,
            addGridTabRecordAction.GridTabRecord,
            addGridTabRecordAction.TabToSetAsActive,
            ConstructorActionKind.Add);

        return nextGridItemRecordsState;
    }
    
    [ReducerMethod]
    public static GridItemRecordsState ReduceCloseGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        CloseGridTabRecordAction closeGridTabRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            closeGridTabRecordAction.GridItemRecordKey,
            closeGridTabRecordAction.GridTabRecordKey,
            closeGridTabRecordAction.TabToSetAsActive,
            ConstructorActionKind.Remove);

        return nextGridItemRecordsState;
    }
    
    [ReducerMethod]
    public static GridItemRecordsState ReduceSetActiveGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        SetActiveGridTabRecordAction setActiveGridTabRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            setActiveGridTabRecordAction.GridItemRecordKey,
            setActiveGridTabRecordAction.GridTabRecordKey,
            setActiveGridTabRecordAction.TabToSetAsActive,
            ConstructorActionKind.Replace);

        return nextGridItemRecordsState;
    }
    
    [ReducerMethod]
    public static GridItemRecordsState ReduceReplaceGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        ReplaceGridTabRecordAction replaceGridTabRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            replaceGridTabRecordAction.GridItemRecordKey,
            replaceGridTabRecordAction.GridTabRecord,
            replaceGridTabRecordAction.TabToSetAsActive,
            ConstructorActionKind.Replace);

        return nextGridItemRecordsState;
    }
}
