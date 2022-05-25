using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridRecordsState ReduceRegisterGridRecordAction(GridRecordsState previousGridRecordsState,
        RegisterGridRecordAction registerGridRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState, registerGridRecordAction.GridRecordKey);
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceAddGridItemRecordAction(GridRecordsState previousGridRecordsState,
        AddGridItemRecordAction addGridItemRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState,
            addGridItemRecordAction.GridRecordKey,
            addGridItemRecordAction.GridItemRecord,
            addGridItemRecordAction.CardinalDirectionKind,
            addGridItemRecordAction.RowIndex,
            addGridItemRecordAction.ActiveGridItemRecordIndex);
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceRemoveGridItemRecordAction(GridRecordsState previousGridRecordsState,
        RemoveGridItemRecordAction removeGridItemRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState,
            removeGridItemRecordAction.GridRecordKey,
            removeGridItemRecordAction.RowIndex,
            removeGridItemRecordAction.GridItemIndex);
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceRemoveGridRowRecordAction(GridRecordsState previousGridRecordsState,
        RemoveGridRowRecordAction removeGridRowRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState,
            removeGridRowRecordAction.GridRecordKey,
            removeGridRowRecordAction.RowIndex);
    }
}
