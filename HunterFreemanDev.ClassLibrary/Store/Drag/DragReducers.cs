using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Drag;

public class DragReducers
{
    [ReducerMethod]
    public static DragState ReduceOnDragEventAction(DragState previousDragState, DragEventAction onDragEventAction)
    {
        double nextDeltaX;
        double nextDeltaY;

        if (previousDragState.MouseEventArgs is not null && onDragEventAction.MouseEventArgs is not null)
        {
            nextDeltaX = onDragEventAction.MouseEventArgs.ClientX - previousDragState.MouseEventArgs.ClientX;
            nextDeltaY = onDragEventAction.MouseEventArgs.ClientY - previousDragState.MouseEventArgs.ClientY;
        }
        else
        {
            nextDeltaX = 0;
            nextDeltaY = 0;
        }

        return new DragState(nextDeltaX, nextDeltaY, onDragEventAction.MouseEventArgs);
    }

    [ReducerMethod]
    public static DragEventProviderState ReduceDragEventProviderState(DragEventProviderState previousDragEventProviderState, 
        SubscribeToDragEventProviderStateAction subscribeToDragEventProviderStateAction)
    {
        var nextDragEventProviderState = 
            new DragEventProviderState(new Dictionary<Guid, Action>(previousDragEventProviderState.OnDragEventSubscriptions));

        nextDragEventProviderState.OnDragEventSubscriptions.TryAdd(subscribeToDragEventProviderStateAction.Id, subscribeToDragEventProviderStateAction.Action);

        return nextDragEventProviderState;
    }
    
    [ReducerMethod]
    public static DragEventProviderState ReduceDragEventProviderState(DragEventProviderState previousDragEventProviderState,
        ClearDragEventSubscriptionsAction clearDragEventSubscriptionsAction)
    {
        var nextDragEventProviderState = 
            new DragEventProviderState(new Dictionary<Guid, Action>());

        return nextDragEventProviderState;
    }
}
