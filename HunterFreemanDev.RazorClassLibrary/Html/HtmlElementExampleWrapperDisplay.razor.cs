using Fluxor;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.Html;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Html;

public partial class HtmlElementExampleWrapperDisplay
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private List<HtmlElementRecordKey> _htmlElementRecordKeys = new();

    private void AddHtmlElementOnClick()
    {
        var htmlElementKey = new HtmlElementRecordKey(Guid.NewGuid());
        
        _htmlElementRecordKeys.Add(htmlElementKey);

        var registerHtmlElementAction = new RegisterHtmlElementAction(htmlElementKey,
            DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElementAction);
    }
}