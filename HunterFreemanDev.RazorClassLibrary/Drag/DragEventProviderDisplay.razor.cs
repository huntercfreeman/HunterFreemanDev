using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Drag;

namespace HunterFreemanDev.RazorClassLibrary.Drag;

public partial class DragEventProviderDisplay : FluxorComponent
{
    [Inject]
    private IState<DragEventProviderState> DragEventProviderState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private string IsActiveCssClass => DragEventProviderState.Value.OnDragEventSubscriptions.Any()
        ? "hfd_active"
        : string.Empty;

    private void DispatchOnDragEventActionOnMouseMove(MouseEventArgs mouseEventArgs)
    {
        var action = new OnDragEventAction(mouseEventArgs);

        Dispatcher.Dispatch(action);
    }
}