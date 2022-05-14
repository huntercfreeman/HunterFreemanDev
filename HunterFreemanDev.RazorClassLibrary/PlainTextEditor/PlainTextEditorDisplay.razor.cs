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

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class PlainTextEditorDisplay : FluxorComponent
{
    [Inject]
    private IState<KeyDownEventState> KeyDownEventState { get; set; } = null!;
    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;

    private StringBuilder _contentBuilder = new();
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
            _contentBuilder.Append(KeyDownEventState.Value.OnKeyDownEventRecord.Key);
        }

        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        KeyDownEventState.StateChanged -= KeyDownState_StateChanged;
    }
}