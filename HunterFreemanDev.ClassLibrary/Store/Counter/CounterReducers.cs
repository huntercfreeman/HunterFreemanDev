using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
