using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Dimension;
using System.Text;
using HunterFreemanDev.ClassLibrary.Store.Drag;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using Fluxor.Blazor.Web.Components;

namespace HunterFreemanDev.RazorClassLibrary.Transformative;

public partial class TransformativeDisplay : FluxorComponent
{
    [Inject]
    private IState<DragState> DragState { get; set; } = null!;
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DimensionsRecord DimensionsRecord { get; set; } = null!;

    private DimensionValuedUnit DEFAULT_HANDLE_SIZE_IN_PIXELS = new DimensionValuedUnit(7, DimensionUnitKind.Pixels);
    private SemaphoreSlim _dragStateChangedSemaphoreSlim = new(1, 1);
    private Stack<(object? sender, EventArgs e)> _dragStateChangedStack = new();
    private CancellationTokenSource _dragStateChangedCancellationTokenSource = new();
    private Task? _dragStateThrottlingTask;
    private Action? _dragStateHandler;
    private Guid _transformativeDisplayId = Guid.NewGuid();

    private int _resizeEventCounter;

    protected override void OnInitialized()
    {
        DragState.StateChanged += DragState_StateChanged;

        base.OnInitialized();
    }

    private async void DragState_StateChanged(object? sender, EventArgs e)
    {
        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync();

            _dragStateChangedStack.Push((sender, e));
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }

        if(_dragStateThrottlingTask is not null)
            await _dragStateThrottlingTask;

        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync();

            if(_dragStateChangedStack.Any())
            {
                _dragStateChangedStack.Clear();



                _dragStateThrottlingTask = Task.Run(async () =>
                {
                    await Task.Delay(300);
                }, _dragStateChangedCancellationTokenSource.Token);
            }
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }
    }

    #region HandleCssStylings
    private string GetNorthResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels = 
            new DimensionValuedUnit(
                DimensionsRecord.Width.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value, 
                DimensionUnitKind.Pixels);
        
        DimensionValuedUnit heightInPixels = 
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value, 
            DimensionUnitKind.Pixels);
        
        DimensionValuedUnit leftInPixels = 
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0, 
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0, 
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }

    private string GetEastResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DimensionsRecord.Height.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(DimensionsRecord.Width.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    
    private string GetSouthResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DimensionsRecord.Width.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DimensionsRecord.Height.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }

    private string GetWestResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DimensionsRecord.Height.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    
    private string GetNorthEastResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(DimensionsRecord.Width.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    
    private string GetSouthEastResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(DimensionsRecord.Width.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DimensionsRecord.Height.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    
    private string GetSouthWestResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DimensionsRecord.Height.Value - DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    
    private string GetNorthWestResizeHandleCssStyling()
    {
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Width), DimensionsRecord.Width);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Height), DimensionsRecord.Height);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Left), DimensionsRecord.Left);
        ValidateDimensionUnitKindIsSupported(nameof(DimensionsRecord.Top), DimensionsRecord.Top);

        var cssStylingBuilder = new StringBuilder();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HANDLE_SIZE_IN_PIXELS.Value,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(-1 * DEFAULT_HANDLE_SIZE_IN_PIXELS.Value / 2.0,
            DimensionUnitKind.Pixels);

        cssStylingBuilder.Append($"width: {widthInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"height: {heightInPixels.BuildCssStyleString()}; ");

        cssStylingBuilder.Append($"left: {leftInPixels.BuildCssStyleString()}; ");
        cssStylingBuilder.Append($"top: {topInPixels.BuildCssStyleString()}; ");

        return cssStylingBuilder.ToString();
    }
    #endregion

    private void SubscribeToDragEventWithNorthResizeHandle() =>
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerNorthResizeHandle);

    private void DispatchSubscribeToDragEventProviderStateAction(Action dragEventActionHandler)
    {
        var action = new SubscribeToDragEventProviderStateAction(_transformativeDisplayId,
            dragEventActionHandler);

        Dispatcher.Dispatch(action);
    }

    private void DragEventHandlerNorthResizeHandle()
    {
        _resizeEventCounter++;

        InvokeAsync(StateHasChanged);
    }

    private void ValidateDimensionUnitKindIsSupported(string dimensionName, 
        DimensionValuedUnit dimensionValuedUnit)
    {
        if (dimensionValuedUnit.DimensionUnitKind != DimensionUnitKind.Pixels)
            throw new ApplicationException($"The {nameof(DimensionUnitKind)}: {dimensionValuedUnit.DimensionUnitKind} " +
                $"is not supported for {nameof(TransformativeDisplay)}. The name of the dimension with this " +
                $"unsupported type is named: {dimensionName}.");
    }

    public void Dispose()
    {
        DragState.StateChanged -= DragState_StateChanged;
        _dragStateChangedCancellationTokenSource?.Cancel();
    }
}