using Fluxor;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DebugCssClassRecord DebugCssClassRecord { get; set; } = null!;

    private string GetIsActiveCssClass()
    {
        return DebugCssClassRecord.IsEnabled
            ? "hfd_active"
            : string.Empty;
    }

    private string GetIsActiveDisplayText()
    {
        return DebugCssClassRecord.IsEnabled
            ? "Active"
            : "NOT Active";
    }

    private void DispatchSetIsEnabledDebugCssClassRecordAction()
    {
        var action = new SetIsEnabledDebugCssClassRecordAction(DebugCssClassRecord.DebugCssClassId,
            !DebugCssClassRecord.IsEnabled);

        Dispatcher.Dispatch(action);
    }
}