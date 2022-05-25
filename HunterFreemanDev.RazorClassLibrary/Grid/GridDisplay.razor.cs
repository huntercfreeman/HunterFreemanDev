using System.Text;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using HunterFreemanDev.ClassLibrary.Store.Html;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IState<GridRecordsState> GridRecordsState { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    [Parameter]
    public DimensionsRecord? InitialDimensionsRecord { get; set; }
    [Parameter]
    public bool IsFixedDimensions { get; set; }

    public const string ON_CHOSE_GRID_TAB_RECORD_ACTION_PARAMETER_NAME = "OnChoseGridTabRecordAction";
    
    private GridBoardRecord? _cachedGridBoard;
    private HtmlElementRecord? _cachedHtmlElementRecord;

    protected override void OnInitialized()
    {
        GridRecordsState.StateChanged += OnStateChanged;
        HtmlElementRecordsState.StateChanged += OnStateChanged;
        
        var registerHtmlElementAction = new RegisterHtmlElementAction(GridRecord.HtmlElementRecordKey,
            InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElementAction);
        
        var registerGridRecordAction = new RegisterGridRecordAction(GridRecord.GridRecordKey);

        Dispatcher.Dispatch(registerGridRecordAction);

        base.OnInitialized();
    }

    private async void OnStateChanged(object? sender, EventArgs e)
    {
        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridRecord.HtmlElementRecordKey);

            _cachedGridBoard = GridRecordsState.Value
                .LookupGridBoard(GridRecord.GridRecordKey);
        }
        catch (KeyNotFoundException)
        {
        }
        
        await InvokeAsync(StateHasChanged);
    }

    private string GetKey()
    {
        return $"{_cachedGridBoard.GridBoardSequence}" +
               $"{_cachedHtmlElementRecord.HtmlElementSequence}";
    }

    private void AddGridItemRecordOnClick()
    {
        var addGridItemRecordAction = new AddGridItemRecordAction(GridRecord.GridRecordKey,
            new GridItemRecord(new GridItemRecordKey(Guid.NewGuid()),
                               new HtmlElementRecordKey(Guid.NewGuid())),
            CardinalDirectionKind.CurrentPosition,
            null,
            null);

        Dispatcher.Dispatch(addGridItemRecordAction);
    }
    
    protected override void Dispose(bool disposing)
    {
        GridRecordsState.StateChanged -= OnStateChanged;
        HtmlElementRecordsState.StateChanged -= OnStateChanged;
        
        var unregisterHtmlElementAction = new UnregisterHtmlElementAction(GridRecord.HtmlElementRecordKey);

        Dispatcher.Dispatch(unregisterHtmlElementAction);

        base.Dispose(disposing);
    }
}