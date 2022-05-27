using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using HunterFreemanDev.ClassLibrary.Store.Html;
using HunterFreemanDev.RazorClassLibrary.Html;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridItemDisplay : FluxorComponent
{
    [Inject]
    private IState<GridItemRecordsState> GridItemRecordsState { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [CascadingParameter]
    public GridRecordKey GridRecordKey { get; set; } = null!;
    [CascadingParameter]
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    [CascadingParameter]
    public int TotalGridItemCountInRow { get; set; }
    
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
    
    private GridTabContainerRecord? _cachedGridTabContainer;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _previousTotalGridItemCountInRow;

    private bool _isInitialized;

    private DimensionValuedUnit _heightOfGridTabDimensionValuedUnit = new DimensionValuedUnit(2, DimensionUnitKind.Rem);
    
    protected override void OnInitialized()
    {
        GridItemRecordsState.StateChanged += OnStateChanged;
        HtmlElementRecordsState.StateChanged += OnStateChanged;

        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridItemRecord.HtmlElementRecordKey);
        }
        catch (KeyNotFoundException)
        {
            // Not yet initialized
            var registerHtmlElementAction = new RegisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey,
                GetDimensionsRecord(),
                new ZIndexRecord(0));

            Dispatcher.Dispatch(registerHtmlElementAction);
        }
        
        try
        {
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);
        }
        catch (KeyNotFoundException)
        {
            // Not yet initialized
            var registerGridTabContainerRecordAction = new RegisterGridTabContainerRecordAction(GridItemRecord.GridItemRecordKey);

            Dispatcher.Dispatch(registerGridTabContainerRecordAction);
        }
        
        base.OnInitialized();
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        try
        {
            if (_previousTotalGridItemCountInRow != TotalGridItemCountInRow)
            {
                _previousTotalGridItemCountInRow = TotalGridItemCountInRow;
                
                var replaceHtmlElementDimensionsRecordAction = new ReplaceHtmlElementDimensionsRecordAction(GridItemRecord.HtmlElementRecordKey,
                    GetDimensionsRecord());
                
                Dispatcher.Dispatch(replaceHtmlElementDimensionsRecordAction);
            }
            
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridItemRecord.HtmlElementRecordKey);
            
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);

            if (!_isInitialized && GridItemRecord.InitialGridTabRecord is not null && _cachedGridTabContainer is not null)
            {
                var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecord.GridItemRecordKey,
                   GridItemRecord.InitialGridTabRecord,
                0);

                Dispatcher.Dispatch(addGridTabRecordAction);
            }

            _isInitialized = true;
        }
        catch (KeyNotFoundException)
        {
        }
    }

    private DimensionsRecord GetDimensionsRecord()
    {
        var initialWidth = 
            new DimensionValuedUnit(100.0 / (TotalGridItemCountInRow == 0 ? 1 : TotalGridItemCountInRow),
                DimensionUnitKind.PercentageOfParent);
        var initialHeight = new DimensionValuedUnit(100.0, DimensionUnitKind.PercentageOfParent);
        var initialLeft = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        var initialTop = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);

        return new DimensionsRecord(initialWidth, initialHeight, initialLeft, initialTop);
    }

    private string GetKey()
    {
        return $"{TotalGridItemCountInRow}" +
               $"{_cachedGridTabContainer.GridTabContainerRecordSequence}" +
               $"{_cachedHtmlElementRecord}";
    }

    private void AddGridTabRecordOnClick()
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecord.GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()),
                              true,
                              typeof(HtmlElementExampleWrapperDisplay),
                              nameof(HtmlElementExampleWrapperDisplay)),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }

    private void OnGridTabRecordChosenAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecord.GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), 
                true,
                argumentTuple.renderedContentType,
                argumentTuple.renderedContentTabDisplayName),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }
    
    private GridTabRecord? GetActiveGridTab()
    {
        if(_cachedGridTabContainer is not null &&
           _cachedGridTabContainer.ActiveGridTabIndex is not null)
        {
            return _cachedGridTabContainer.GridTabRecords[_cachedGridTabContainer.ActiveGridTabIndex.Value];
        }

        return null;
    }

    private Guid? GetActiveGridTabId()
    {
        if(_cachedGridTabContainer is not null &&
           _cachedGridTabContainer.ActiveGridTabIndex is not null)
        {
            return _cachedGridTabContainer
                .GridTabRecords[_cachedGridTabContainer.ActiveGridTabIndex.Value]
                .GridTabRecordKey.Id;
        }

        return null;
    }
    
    protected override void Dispose(bool disposing)
    {
        GridItemRecordsState.StateChanged -= OnStateChanged;
        HtmlElementRecordsState.StateChanged -= OnStateChanged;
        
        var unregisterHtmlElementAction = new UnregisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey);

        Dispatcher.Dispatch(unregisterHtmlElementAction);

        base.Dispose(disposing);
    }
}