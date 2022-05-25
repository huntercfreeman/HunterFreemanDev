using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Counter;

[FeatureState]
public record CounterState(int Count)
{
    private CounterState() : this(0)
    {
    }
}
