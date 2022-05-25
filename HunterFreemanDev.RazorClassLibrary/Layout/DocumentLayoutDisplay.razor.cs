using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
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
