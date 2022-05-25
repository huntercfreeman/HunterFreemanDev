using Fluxor;
using HunterFreemanDev.ClassLibrary.Focus;

namespace HunterFreemanDev.ClassLibrary.Store.Focus;

[FeatureState]
public record FocusState(FocusRecord? FocusRecord)
{
    public FocusState() : this(default(FocusRecord))
    {

    }
}
