using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Focus;
using HunterFreemanDev.ClassLibrary.Focus;
using Fluxor.Blazor.Web.Components;

namespace HunterFreemanDev.RazorClassLibrary.Focus;

public partial class FocusBoundaryDisplay : FluxorComponent
{
    public FocusBoundaryDisplay()
    {
        FocusRecord = new FocusRecord(Guid.NewGuid(), FocusRecordDisplayName);
    }

    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;
    [Parameter, EditorRequired]
    public string FocusRecordDisplayName { get; set; } = null!;
    [Parameter]
    public string Class { get; set; }
    [Parameter]
    public bool InitiallySetFocusOnAfterRender { get; set; } = true;

    private FocusTrapDisplay? _focusTrapDisplay = null!;

    public FocusRecord FocusRecord { get; }

    protected override void OnInitialized()
    {
        FocusState.StateChanged += FocusState_StateChanged;

        base.OnInitialized();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && InitiallySetFocusOnAfterRender)
        {
            var action = new SetActiveFocusRecordAction(FocusRecord);

            Dispatcher.Dispatch(action);
        }

        base.OnAfterRender(firstRender);
    }

    public bool GetIsFocused() => FocusState.Value.FocusRecord is not null &&
                FocusRecord is not null &&
                FocusState.Value.FocusRecord.FocusRecordId == FocusRecord.FocusRecordId;

    private async void FocusState_StateChanged(object? sender, EventArgs e)
    {
        if(FocusState.Value.FocusRecord is not null &&
            FocusState.Value.FocusRecord.FocusRecordId == FocusRecord.FocusRecordId)
        {
            if(_focusTrapDisplay is not null)
                await _focusTrapDisplay.GetFocusTrapElementReference().FocusAsync();
        }
    }

    public async Task FocusIn()
    {
        try
        {
            var action = new SetActiveFocusRecordAction(FocusRecord);

            Dispatcher.Dispatch(action);
        }
        catch (Microsoft.JSInterop.JSException)
        {
            // Caused when calling:
            // await _focusTrap.FocusAsync();
            // After component is no longer rendered
        }
    }
    
    public async Task FocusOut()
    {
        var action = new SetActiveFocusRecordAction(null);

        Dispatcher.Dispatch(action);
    }

    protected override void Dispose(bool disposing)
    {
        FocusState.StateChanged -= FocusState_StateChanged;

        base.Dispose(disposing);
    }
}