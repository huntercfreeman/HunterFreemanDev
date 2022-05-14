using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.KeyDown;
using System.Text;
using HunterFreemanDev.ClassLibrary.Store.Focus;
using HunterFreemanDev.ClassLibrary.Focus;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.RazorClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.Store.PlainTextEditor;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class PlainTextEditorDisplay : FluxorComponent
{
    [Inject]
    private IState<KeyDownEventState> KeyDownEventState { get; set; } = null!;
    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private PlainTextEditorRecord _plainTextEditorRecord = new(Guid.NewGuid());
    private FocusBoundaryDisplay? _focusBoundaryDisplay = null!;

    protected override void OnInitialized()
    {
        var action = new UnregisterPlainTextEditorAction(_plainTextEditorRecord);

        Dispatcher.Dispatch(action);

        KeyDownEventState.StateChanged += KeyDownState_StateChanged;

        base.OnInitialized();
    }

    private async void KeyDownState_StateChanged(object? sender, EventArgs e)
    {
        if (_focusBoundaryDisplay?.GetIsFocused() ?? false)
        {
            _plainTextEditorRecord = await _plainTextEditorRecord.HandleKeyDownEventAsync(KeyDownEventState.Value.OnKeyDownEventRecord);
        }

        await InvokeAsync(StateHasChanged);
    }

    protected override void Dispose(bool disposing)
    {
        var action = new UnregisterPlainTextEditorAction(_plainTextEditorRecord);

        Dispatcher.Dispatch(action);

        KeyDownEventState.StateChanged -= KeyDownState_StateChanged;

        base.Dispose(disposing);
    }
}