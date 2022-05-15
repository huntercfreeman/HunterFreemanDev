using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record ReplaceGridRecordAction(GridRecord GridRecord, object? GridRecordChildComponentState);
