using System.Text;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Store.Drag;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HunterFreemanDev.RazorClassLibrary.Drag;

public partial class DragEventProviderDisplay : FluxorComponent
{
    [Inject]
    private IState<DragEventProviderState> DragEventProviderState { get; set; } = null!;
    [Inject]
    private IState<DragState> DragState { get; set; } = null!;
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private SemaphoreSlim _dragStateChangedSemaphoreSlim = new(1, 1);
    private Stack<MouseEventArgs> _dragStateChangedStack = new();
    private CancellationTokenSource? _dragStateChangedCancellationTokenSource;
    private readonly TimeSpan _dragStateThrottlingDelay = TimeSpan.FromMilliseconds(25);
    private Task? _dragStateThrottlingTask;
    
    private string IsActiveCssClass => DragEventProviderState.Value.OnDragEventSubscriptions.Any()
        ? "hfd_active"
        : string.Empty;

    private async Task DispatchOnDragEventActionOnMouseMove(MouseEventArgs mouseEventArgs)
    {
        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync(_dragStateChangedCancellationTokenSource?.Token ?? default);

            _dragStateChangedStack.Push(mouseEventArgs);
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }

        if (_dragStateThrottlingTask is not null)
        {
            await _dragStateThrottlingTask.WaitAsync(_dragStateChangedCancellationTokenSource?.Token ?? default);

            if (_dragStateThrottlingTask.IsCanceled)
                return;
        }

        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync(_dragStateChangedCancellationTokenSource?.Token ?? default);

            if (_dragStateChangedStack.Any())
            {
                var mostRecentMouseEventArgs = _dragStateChangedStack.Pop();

                _dragStateChangedStack.Clear();

                var action = new DragEventAction(mostRecentMouseEventArgs);

                if (_dragStateThrottlingTask?.IsCanceled ?? false)
                    return;

                Dispatcher.Dispatch(action);

                _dragStateChangedCancellationTokenSource?.Cancel();
                _dragStateChangedCancellationTokenSource = new();

                _dragStateThrottlingTask = Task.Run(async () =>
                {
                    await Task.Delay(_dragStateThrottlingDelay);
                }, _dragStateChangedCancellationTokenSource.Token);
            }
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }
    }
    
    private async Task DispatchUnsubscribeActionOnMouseUp(MouseEventArgs mouseEventArgs)
    {
        _dragStateChangedCancellationTokenSource?.Cancel();
        _dragStateChangedCancellationTokenSource = null;

        var clearDragEventSubscriptionsAction = new ClearDragEventSubscriptionsAction();

        Dispatcher.Dispatch(clearDragEventSubscriptionsAction);

        var onDragEventAction = new DragEventAction(null);

        Dispatcher.Dispatch(onDragEventAction);

        // DragEvent state is being set to a MouseEventArgs of null due to this method.
        // However, the throttling is set to 25 miliseconds.
        // Seemingly there is a timing issue introduced by the throttling where the MouseEventArgs is set to null
        // then the throttled Task to set the MouseEventArgs to a actual value is completed changing it from null.
        // The user then experiences a "jump" when they drag something as it thinks they dragged in one motion
        // but in reality the drag event was remnants and should have been null.
        await Task.Delay(_dragStateThrottlingDelay * 2);
        Dispatcher.Dispatch(onDragEventAction);
    }
}