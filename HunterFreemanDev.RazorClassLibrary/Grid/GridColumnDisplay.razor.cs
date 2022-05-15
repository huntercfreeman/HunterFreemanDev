using Fluxor;
using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Element;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridColumnDisplay : ComponentBase
{
    [Inject]
    private IState<GridState> GridState { get; set; } = null!;

    [Parameter, EditorRequired]
    public int GridColumnIndex { get; set; }
    [Parameter, EditorRequired]
    public int GridTotalColumnCount { get; set; }
    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex)> AddWindowEventCallback { get; set; }

    private string GetStyle => $"width: calc({100.0 / GridTotalColumnCount}% - 3px);";

    private Dictionary<string, object>? GetDynamicComponentParameters()
    {
        if(!string.IsNullOrWhiteSpace(GridRecord.GridRecordChildComponentStateParameterName))
        {
            return new Dictionary<string, object>
            {
                {
                    GridRecord.GridRecordChildComponentStateParameterName,
                    GridState.Value.GridRecordMap[GridRecord.GridRecordId]
                }
            };
        }

        return null;
    }

    private void OnAddWindowEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((cardinalDirectionKind, GridColumnIndex));
    }
}