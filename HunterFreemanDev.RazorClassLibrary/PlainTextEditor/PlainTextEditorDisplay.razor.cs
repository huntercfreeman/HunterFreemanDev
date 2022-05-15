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
using HunterFreemanDev.ClassLibrary.Store.Grid;
using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class PlainTextEditorDisplay : FluxorComponent
{
    [Inject]
    private IState<KeyDownEventState> KeyDownEventState { get; set; } = null!;
    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridRecord? GridRecord { get; set; }

    [Parameter]
    public PlainTextEditorRecord PlainTextEditorRecord { get; set; } = null!;

    private FocusBoundaryDisplay? _focusBoundaryDisplay = null!;

    protected override void OnInitialized()
    {
        KeyDownEventState.StateChanged += KeyDownState_StateChanged;

        base.OnInitialized();
    }

    private async void KeyDownState_StateChanged(object? sender, EventArgs e)
    {
        if (_focusBoundaryDisplay?.GetIsFocused() ?? false)
        {
            PlainTextEditorRecord = await PlainTextEditorRecord.HandleKeyDownEventAsync(KeyDownEventState.Value.OnKeyDownEventRecord);

            if(GridRecord is not null)
            {
                var saveGridStateAction = new ReplaceGridRecordAction(GridRecord, PlainTextEditorRecord);

                Dispatcher.Dispatch(saveGridStateAction);
            }
        }

        await InvokeAsync(StateHasChanged);
    }

    protected override void Dispose(bool disposing)
    {
        KeyDownEventState.StateChanged -= KeyDownState_StateChanged;

        base.Dispose(disposing);
    }
}