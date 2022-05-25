using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.Html;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Html;

public partial class HtmlElementExampleDisplay : FluxorComponent
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public HtmlElementRecordKey HtmlElementRecordKey { get; set; } = null!;

    private Guid? _previousHtmlElementSequence;

    private int _renderedCount;

    private void RequestZIndexOnClick()
    {
        var zIndexRequestAction = new ZIndexRequestAction(HtmlElementRecordKey);

        Dispatcher.Dispatch(zIndexRequestAction);
    }

    protected override bool ShouldRender()
    {
        var htmlElementRecord = HtmlElementRecordsState.Value.LookupHtmlElementRecord(HtmlElementRecordKey);

        bool shouldRender;

        if(_previousHtmlElementSequence is null ||
            _previousHtmlElementSequence.Value != htmlElementRecord.HtmlElementSequence)
        {
            shouldRender = true;
        }
        else
        {
            shouldRender = false;
        }

        _previousHtmlElementSequence = htmlElementRecord.HtmlElementSequence;

        return shouldRender;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        _renderedCount++;

        return base.OnAfterRenderAsync(firstRender);
    }
}