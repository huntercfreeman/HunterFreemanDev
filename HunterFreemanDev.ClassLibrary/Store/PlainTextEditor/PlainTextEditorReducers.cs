using Fluxor;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;

namespace HunterFreemanDev.ClassLibrary.Store.PlainTextEditor;

public class PlainTextEditorReducers
{
    [ReducerMethod]
    public static PlainTextEditorsState ReduceRegisterPlainTextEditorAction(PlainTextEditorsState previousPlainTextEditorsState,
        RegisterPlainTextEditorAction registerPlainTextEditorAction)
    {
        var nextPlainTextEditorsState = new PlainTextEditorsState(
            new Dictionary<Guid, PlainTextEditorRecord>(previousPlainTextEditorsState.PlainTextEditorMap));

        nextPlainTextEditorsState.PlainTextEditorMap.Add(registerPlainTextEditorAction.PlainTextEditorRecord.PlainTextEditorRecordId,
            registerPlainTextEditorAction.PlainTextEditorRecord);

        return nextPlainTextEditorsState;
    }

    [ReducerMethod]
    public static PlainTextEditorsState ReduceRegisterPlainTextEditorAction(PlainTextEditorsState previousPlainTextEditorsState,
        UnregisterPlainTextEditorAction unregisterPlainTextEditorAction)
    {
        var nextPlainTextEditorsState = new PlainTextEditorsState(
            new Dictionary<Guid, PlainTextEditorRecord>(previousPlainTextEditorsState.PlainTextEditorMap));

        nextPlainTextEditorsState.PlainTextEditorMap.Remove(unregisterPlainTextEditorAction.PlainTextEditorRecord.PlainTextEditorRecordId);

        return nextPlainTextEditorsState;
    }
}