using System.Collections.Immutable;
using Fluxor;
using HunterFreemanDev.ClassLibrary.ConstructorAction;
using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.ClassLibrary.Store.Html;

[FeatureState]
public record HtmlElementRecordsState
{
    private Dictionary<HtmlElementRecordKey, HtmlElementRecord> _htmlElementRecordMap;
    
    public HtmlElementRecordsState()
    {
        _htmlElementRecordMap = new();
    }

    public HtmlElementRecordsState(HtmlElementRecordsState otherHtmlElementRecordsState, 
        ConstructorActionKind constructorActionKind,
        HtmlElementRecord htmlElementRecord)
    {
        _htmlElementRecordMap = new(otherHtmlElementRecordsState._htmlElementRecordMap);

        switch(constructorActionKind)
        {
            case ConstructorActionKind.Add:
                PerformAdd(htmlElementRecord);
                break;
            case ConstructorActionKind.Replace:
                PerformReplace(htmlElementRecord);
                break;
        }
    }
    
    public HtmlElementRecordsState(HtmlElementRecordsState otherHtmlElementRecordsState, 
        HtmlElementRecordKey htmlElementRecordKey)
    {
        _htmlElementRecordMap = new(otherHtmlElementRecordsState._htmlElementRecordMap);

        _htmlElementRecordMap.Remove(htmlElementRecordKey);
    }

    private void PerformAdd(HtmlElementRecord htmlElementRecord)
    {
        _htmlElementRecordMap.Add(htmlElementRecord.HtmlElementRecordKey, htmlElementRecord);
    }

    public ImmutableArray<HtmlElementRecord> HtmlElementRecords => _htmlElementRecordMap.Values
        .ToImmutableArray();

    private void PerformReplace(HtmlElementRecord htmlElementRecord)
    {
        _htmlElementRecordMap[htmlElementRecord.HtmlElementRecordKey] = htmlElementRecord;
    }

    public HtmlElementRecord LookupHtmlElementRecord(HtmlElementRecordKey htmlElementRecordKey)
    {
        return _htmlElementRecordMap[htmlElementRecordKey];
    }
}
