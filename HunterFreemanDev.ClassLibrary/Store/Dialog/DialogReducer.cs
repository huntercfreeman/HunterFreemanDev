using Fluxor;
using HunterFreemanDev.ClassLibrary.Dialog;

namespace HunterFreemanDev.ClassLibrary.Store.Dialog;

public class DialogReducer
{
    [ReducerMethod]
    public static DialogStates ReduceRegisterDialogAction(DialogStates previousDialogStates,
        RegisterDialogAction registerDialogAction)
    {
        var nextDialogStates = new DialogStates(new Dictionary<Guid, DialogRecord>(previousDialogStates.DialogRecordMap));

        nextDialogStates.DialogRecordMap.Add(registerDialogAction.DialogRecord.DialogRecordId, 
            registerDialogAction.DialogRecord);

        return nextDialogStates;
    }
    
    [ReducerMethod]
    public static DialogStates ReduceSetIsMinimizedDialogAction(DialogStates previousDialogStates,
        SetIsMinimizedDialogAction setIsMinimizedDialogAction)
    {
        var nextDialogStates = new DialogStates(new Dictionary<Guid, DialogRecord>(previousDialogStates.DialogRecordMap));

        if(nextDialogStates.DialogRecordMap.TryGetValue(setIsMinimizedDialogAction.DialogRecord.DialogRecordId, out var dialogRecord))
        {
            nextDialogStates.DialogRecordMap.Remove(setIsMinimizedDialogAction.DialogRecord.DialogRecordId);

            nextDialogStates.DialogRecordMap.Add(setIsMinimizedDialogAction.DialogRecord.DialogRecordId, setIsMinimizedDialogAction.DialogRecord with
            {
                IsMinimized = setIsMinimizedDialogAction.IsMinimized
            });
        }

        return nextDialogStates;
    }

    [ReducerMethod]
    public static DialogStates ReduceUnregisterDialogAction(DialogStates previousDialogStates,
        UnregisterDialogAction unregisterDialogAction)
    {
        var nextDialogStates = new DialogStates(new Dictionary<Guid, DialogRecord>(previousDialogStates.DialogRecordMap));

        nextDialogStates.DialogRecordMap.Remove(unregisterDialogAction.DialogRecord.DialogRecordId);

        return nextDialogStates;
    }
    
    [ReducerMethod]
    public static DialogStates ReduceReplaceDialogDimensionsRecordAction(DialogStates previousDialogStates,
        ReplaceDialogDimensionsRecordAction replaceDialogDimensionsRecordAction)
    {
        var nextDialogStates = new DialogStates(new Dictionary<Guid, DialogRecord>(previousDialogStates.DialogRecordMap));

        var nextDialogRecord = replaceDialogDimensionsRecordAction.DialogRecord with
        {
            DimensionsRecord = replaceDialogDimensionsRecordAction.DimensionsRecord
        };

        nextDialogStates.DialogRecordMap.Remove(replaceDialogDimensionsRecordAction.DialogRecord.DialogRecordId);

        nextDialogStates.DialogRecordMap.Add(nextDialogRecord.DialogRecordId,
            nextDialogRecord);

        return nextDialogStates;
    }
}