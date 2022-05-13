using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Dimension;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using HunterFreemanDev.RazorClassLibrary.Transformative;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DialogRecord DialogRecord { get; set; } = null!;

    private TransformativeDisplay _transformativeDisplay = null!;

    private void FireSubscribeToDragEventWithMoveHandle()
    {
        _transformativeDisplay.SubscribeToDragEventWithMoveHandle();
    }

    private void OnDimensionsRecordChangedEventCallback(DimensionsRecord replacementDimensionsRecord)
    {
        var action = new ReplaceDialogDimensionsRecordAction(DialogRecord, replacementDimensionsRecord);

        Dispatcher.Dispatch(action);
    }

    private void MinimizeDialogOnClick()
    {
        var action = new SetIsMinimizedDialogAction(DialogRecord, true);

        Dispatcher.Dispatch(action);
    }
    
    private void CloseDialogOnClick()
    {
        var action = new UnregisterDialogAction(DialogRecord);

        Dispatcher.Dispatch(action);
    }
}