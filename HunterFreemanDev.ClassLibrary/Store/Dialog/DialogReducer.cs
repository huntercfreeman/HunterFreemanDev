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

        var previousDialogRecord =
            nextDialogStates.DialogRecordMap[setIsMinimizedDialogAction.DialogRecord.DialogRecordId];

        nextDialogStates.DialogRecordMap[setIsMinimizedDialogAction.DialogRecord.DialogRecordId] = previousDialogRecord
            with
            {
                IsMinimized = setIsMinimizedDialogAction.IsMinimized
            };

        return nextDialogStates;
    }

    [ReducerMethod]
    public static DialogStates ReduceUnregisterDialogAction(DialogStates previousDialogStates,
        UnregisterDialogAction unregisterDialogAction)
    {
        var nextDialogStates = new DialogStates(new Dictionary<Guid, DialogRecord>(previousDialogStates.DialogRecordMap));

        nextDialogStates.DialogRecordMap.Remove(unregisterDialogAction.DialogRecordId);

        return nextDialogStates;
    }
}