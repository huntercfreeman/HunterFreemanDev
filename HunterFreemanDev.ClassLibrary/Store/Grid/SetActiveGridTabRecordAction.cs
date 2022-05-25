using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record SetActiveGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecordKey GridTabRecordKey,
    int? TabToSetAsActive);