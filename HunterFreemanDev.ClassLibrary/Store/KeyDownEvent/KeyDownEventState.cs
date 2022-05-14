using Fluxor;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.Store.KeyDown;

[FeatureState]
public record class KeyDownEventState(KeyDownEventRecord? OnKeyDownEventRecord)
{
    public KeyDownEventState() : this(default(KeyDownEventRecord))
    {

    }
}
