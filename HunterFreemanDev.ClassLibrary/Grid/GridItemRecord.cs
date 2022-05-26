using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.ClassLibrary.Grid;

public record GridItemRecord(GridItemRecordKey GridItemRecordKey,
    HtmlElementRecordKey HtmlElementRecordKey,
    GridTabRecord? InitialGridTabRecord = null);
