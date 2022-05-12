using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Element;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridColumnDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public int GridColumnIndex { get; set; }
    [Parameter, EditorRequired]
    public int GridTotalColumnCount { get; set; }
    [Parameter, EditorRequired]
    public ElementRecord ElementRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex)> AddWindowEventCallback { get; set; }

    private string GetStyle => $"width: {100.0 / GridTotalColumnCount}%;";

    private void OnAddWindowEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((cardinalDirectionKind, GridColumnIndex));
    }
}