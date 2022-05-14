using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Element;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using HunterFreemanDev.RazorClassLibrary.Counter;
using HunterFreemanDev.RazorClassLibrary.PlainTextEditor;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IState<GridState> GridState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private void OnAddWindowEventCallback((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) argumentTuple)
    {
        var action = new AddGridStateAction(argumentTuple, new ElementRecord(typeof(PlainTextEditorDisplay)));

        Dispatcher.Dispatch(action);
    }
}