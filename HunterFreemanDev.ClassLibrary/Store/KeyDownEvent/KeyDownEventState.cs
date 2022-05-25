using Fluxor;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.Store.KeyDownEvent;

[FeatureState]
public record class KeyDownEventState(KeyDownEventRecord? OnKeyDownEventRecord)
{
    public KeyDownEventState() : this(default(KeyDownEventRecord))
    {

    }
}
