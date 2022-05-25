using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record RemoveGridRowRecordAction(GridRecordKey GridRecordKey,
    int RowIndex);