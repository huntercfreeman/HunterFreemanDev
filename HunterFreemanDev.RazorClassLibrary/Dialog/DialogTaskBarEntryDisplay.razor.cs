using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using Fluxor;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogTaskBarEntryDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DialogRecord DialogRecord { get; set; } = null!;

    private void ToggleIsMinizedDialogOnClick()
    {
        var action = new SetIsMinimizedDialogAction(DialogRecord, !DialogRecord.IsMinimized);

        Dispatcher.Dispatch(action);
    }

    private void CloseDialogOnClick()
    {
        var action = new UnregisterDialogAction(DialogRecord.DialogRecordId);

        Dispatcher.Dispatch(action);
    }
}