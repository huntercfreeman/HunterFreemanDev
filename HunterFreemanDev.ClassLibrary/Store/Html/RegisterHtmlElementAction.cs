using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.ClassLibrary.Store.Html;

public record RegisterHtmlElementAction(HtmlElementRecordKey HtmlElementRecordKey,
        DimensionsRecord DimensionsRecord,
        ZIndexRecord ZIndexRecord);
