using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record ReplaceGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecord GridTabRecord,
    int? TabToSetAsActive);