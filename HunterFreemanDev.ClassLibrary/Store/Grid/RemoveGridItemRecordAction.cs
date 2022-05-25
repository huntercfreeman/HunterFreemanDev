using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record RemoveGridItemRecordAction(GridRecordKey GridRecordKey,
    int RowIndex,
    int GridItemIndex);