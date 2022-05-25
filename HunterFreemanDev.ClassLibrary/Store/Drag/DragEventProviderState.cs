using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Drag;

[FeatureState]
public record DragEventProviderState(Dictionary<Guid, Action> OnDragEventSubscriptions)
{
    public DragEventProviderState() : this(new Dictionary<Guid, Action>())
    {

    }
}
