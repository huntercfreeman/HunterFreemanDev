using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Drag;

[FeatureState]
public record DragEventProviderState(Dictionary<Guid, Action> OnDragEventSubscriptions)
{
    public DragEventProviderState() : this(new Dictionary<Guid, Action>())
    {

    }
}
