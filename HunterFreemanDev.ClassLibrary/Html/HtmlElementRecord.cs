using HunterFreemanDev.ClassLibrary.Dimension;

namespace HunterFreemanDev.ClassLibrary.Html;

public record HtmlElementRecord(HtmlElementRecordKey HtmlElementRecordKey, 
    DimensionsRecord DimensionsRecord, 
    ZIndexRecord ZIndexRecord,
    Guid HtmlElementSequence)
{
}
