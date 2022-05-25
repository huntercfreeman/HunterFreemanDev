using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Focus;

public class FocusReducers
{
    [ReducerMethod]
    public static FocusState ReduceSetActiveFocusRecordAction(FocusState previousFocusState,
        SetActiveFocusRecordAction setActiveFocusRecordAction)
    {
        return new FocusState(setActiveFocusRecordAction.FocusRecord);
    }
}