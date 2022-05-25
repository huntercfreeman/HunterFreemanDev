using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Counter;

public class CounterReducers
{
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState previousCounterState,
        IncrementCounterAction incrementCounterAction)
    {
        return new CounterState(previousCounterState.Count + 1);
    }
}
