using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record AddGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecord GridTabRecord,
    int? TabToSetAsActive);