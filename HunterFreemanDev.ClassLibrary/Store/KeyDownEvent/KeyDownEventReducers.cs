using Fluxor;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.Store.KeyDown;

public class KeyDownEventReducers
{
    [ReducerMethod]
    public static KeyDownEventState ReduceOnKeyDownEventAction(KeyDownEventState previousKeyDownEventState,
        KeyDownEventAction onKeyDownEventAction)
    {
        return new KeyDownEventState(onKeyDownEventAction.OnKeyDownEventRecord);
    }
}