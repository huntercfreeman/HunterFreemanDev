using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Microsoft.AspNetCore.Components;
using System.Text;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

namespace HunterFreemanDev.RazorClassLibrary.Layout;

public partial class DocumentLayoutDisplay : FluxorLayout
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;

    private static readonly Guid[] DebugCssClasses =
    {
        DebugCssClassInitialStates.GlobalDebugCssClassRecord.DebugCssClassId
    };

    private GridRecord _gridRecord = new(new GridRecordKey(Guid.NewGuid()), 
        new HtmlElementRecordKey(Guid.NewGuid()));

    private string GetDebugCssClasses()
    {
        var cssClassesBuilder = new StringBuilder();

        var enabledDebugCssClasses = DebugCssClassesState.Value.DebugCssClassRecordMap.Values
            .Where(cssClass => cssClass.IsEnabled && DebugCssClasses.Contains(cssClass.DebugCssClassId))
            .ToList();

        foreach (var cssClass in enabledDebugCssClasses)
        {
            cssClassesBuilder.Append($"{cssClass.CssClassString} ");
        }

        return cssClassesBuilder.ToString();
    }
}
