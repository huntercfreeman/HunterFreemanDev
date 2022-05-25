using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record AddGridItemRecordAction(GridRecordKey GridRecordKey,
    GridItemRecord GridItemRecord,
    CardinalDirectionKind CardinalDirectionKind,
    int? RowIndex,
    int? ActiveGridItemRecordIndex);