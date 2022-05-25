using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record CloseGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecordKey GridTabRecordKey,
    int? TabToSetAsActive);