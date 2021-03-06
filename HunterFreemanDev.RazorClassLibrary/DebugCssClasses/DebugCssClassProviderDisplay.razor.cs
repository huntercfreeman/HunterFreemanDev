using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using System.Text;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;

namespace HunterFreemanDev.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassProviderDisplay : FluxorComponent
{
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    protected override void OnInitialized()
    {
        var action = new InitializeDebugCssClassesStateAction(DebugCssClassInitialStates.AllDebugCssClassRecords);

        Dispatcher.Dispatch(action);

        base.OnInitialized();
    }

    private string GetDebugCssClasses()
    {
        var cssClassesBuilder = new StringBuilder();

        var enabledDebugCssClasses = DebugCssClassesState.Value.DebugCssClassRecordMap.Values
            .Where(cssClass => cssClass.IsEnabled)
            .ToList();

        foreach(var cssClass in enabledDebugCssClasses)
        {
            cssClassesBuilder.Append($"{cssClass.CssClassString} ");
        }

        return cssClassesBuilder.ToString();
    }
}