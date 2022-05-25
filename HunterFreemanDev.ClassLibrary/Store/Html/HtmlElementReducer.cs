using Fluxor;
using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.ClassLibrary.Store.Html;

public class HtmlElementReducer
{
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceZIndexResponseAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        ZIndexResponseAction zIndexResponseAction)
    {
        var previousHtmlElementRecord = previousHtmlElementRecordsState.LookupHtmlElementRecord(zIndexResponseAction.HtmlElementRecordKey);

        var nextHtmlElementRecord = previousHtmlElementRecord with
        {
            ZIndexRecord = zIndexResponseAction.ZIndexRecord
        };

        return new HtmlElementRecordsState(previousHtmlElementRecordsState, 
            ConstructorAction.ConstructorActionKind.Replace, 
            nextHtmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceReplaceHtmlElementDimensionsRecordAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        ReplaceHtmlElementDimensionsRecordAction replaceHtmlElementDimensionsRecordAction)
    {
        var previousHtmlElementRecord = previousHtmlElementRecordsState.LookupHtmlElementRecord(replaceHtmlElementDimensionsRecordAction.HtmlElementRecordKey);

        var nextHtmlElementRecord = previousHtmlElementRecord with
        {
            DimensionsRecord = replaceHtmlElementDimensionsRecordAction.DimensionsRecord,
            HtmlElementSequence = Guid.NewGuid()
        };

        return new HtmlElementRecordsState(previousHtmlElementRecordsState, 
            ConstructorAction.ConstructorActionKind.Replace, 
            nextHtmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceRegisterHtmlElementAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        RegisterHtmlElementAction registerHtmlElementAction)
    {
        var htmlElementRecord = new HtmlElementRecord(registerHtmlElementAction.HtmlElementRecordKey,
            registerHtmlElementAction.DimensionsRecord,
            registerHtmlElementAction.ZIndexRecord,
            Guid.NewGuid());

        return new HtmlElementRecordsState(previousHtmlElementRecordsState,
            ConstructorAction.ConstructorActionKind.Add,
            htmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceUnregisterHtmlElementAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        UnregisterHtmlElementAction unregisterHtmlElementAction)
    {
        return new HtmlElementRecordsState(previousHtmlElementRecordsState,
            unregisterHtmlElementAction.HtmlElementRecordKey);
    }
}
