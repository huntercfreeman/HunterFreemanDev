using Microsoft.AspNetCore.Components;
using Fluxor;
using System.Text;
using HunterFreemanDev.ClassLibrary.Store.Focus;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.RazorClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Store.KeyDownEvent;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class PlainTextEditorDisplay : FluxorComponent
{
    [Inject]
    private IState<KeyDownEventState> KeyDownEventState { get; set; } = null!;
    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridRecord? GridRecord { get; set; }

    [Parameter]
    public PlainTextEditorRecord PlainTextEditorRecord { get; set; } = null!;

    private FocusBoundaryDisplay? _focusBoundaryDisplay = null!;

    private static readonly Guid[] DebugCssClasses = 
    {
        DebugCssClassInitialStates.PlainTextEditorDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
    };

    protected override void OnInitialized()
    {
        KeyDownEventState.StateChanged += KeyDownState_StateChanged;

        base.OnInitialized();
    }

    private async void KeyDownState_StateChanged(object? sender, EventArgs e)
    {
        if (_focusBoundaryDisplay?.GetIsFocused() ?? false)
        {
            PlainTextEditorRecord = await PlainTextEditorRecord.HandleKeyDownEventAsync(KeyDownEventState.Value.OnKeyDownEventRecord);
        }

        await InvokeAsync(StateHasChanged);
    }

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

    protected override void Dispose(bool disposing)
    {
        KeyDownEventState.StateChanged -= KeyDownState_StateChanged;

        base.Dispose(disposing);
    }
}