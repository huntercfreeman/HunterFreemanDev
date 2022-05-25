using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using HunterFreemanDev.ClassLibrary.Store.Html;
using HunterFreemanDev.RazorClassLibrary.Transformative;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogDisplay : FluxorComponent
{
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DialogRecord DialogRecord { get; set; } = null!;
    [Parameter]
    public EventCallback<object> CompleteDialogInteractionEventCallback { get; set; }

    private TransformativeDisplay _transformativeDisplay = null!;
    private Guid? _previousHtmlElementSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _renderCount;

    protected override async Task OnInitializedAsync()
    {
        var dimensionsRecordForDialog = await HunterFreemanDev.ClassLibrary.Dialog.DialogRecord
            .ConstructDefaultDimensionsRecord(ViewportDimensionsService);
        
        var registerHtmlElementAction = new RegisterHtmlElementAction(DialogRecord.HtmlElementRecordKey,
            dimensionsRecordForDialog,
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElementAction);

        await base.OnInitializedAsync();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(DialogRecord.HtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;

        base.OnAfterRender(firstRender);
    }

    private void FireSubscribeToDragEventWithMoveHandle()
    {
        _transformativeDisplay.SubscribeToDragEventWithMoveHandle();
    }

    private void MinimizeDialogOnClick()
    {
        var action = new SetIsMinimizedDialogAction(DialogRecord, true);

        Dispatcher.Dispatch(action);
    }

    private void CloseDialogOnClick()
    {
        var action = new UnregisterDialogAction(DialogRecord.DialogRecordId);

        Dispatcher.Dispatch(action);
    }
}