using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Microsoft.AspNetCore.Components;
using System.Text;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using HunterFreemanDev.RazorClassLibrary.FolderExplorer;

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

    private readonly GridRecord _gridRecord = new(new GridRecordKey(Guid.NewGuid()), 
        new HtmlElementRecordKey(Guid.NewGuid()));

    private readonly GridTabRecord _folderExplorerGridTabRecord = new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()),
        false,
        typeof(FolderExplorerDisplay),
        nameof(FolderExplorerDisplay));

    private readonly DimensionsRecord _initialDimensionsRecordForGrid = new(new DimensionValuedUnit(100, DimensionUnitKind.PercentageOfParent),
        new DimensionValuedUnit(100, DimensionUnitKind.PercentageOfParent),
        new DimensionValuedUnit(0, DimensionUnitKind.Pixels),
        new DimensionValuedUnit(0, DimensionUnitKind.Pixels));

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
