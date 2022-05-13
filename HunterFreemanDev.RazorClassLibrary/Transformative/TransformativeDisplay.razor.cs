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
    [Parameter, EditorRequired]
    public EventCallback<DimensionsRecord> OnDimensionsRecordChangedEventCallback { get; set; }

    private DimensionValuedUnit DEFAULT_HANDLE_SIZE_IN_PIXELS = new DimensionValuedUnit(7, DimensionUnitKind.Pixels);
    private Action? _dragStateEventHandler;
    private Guid _transformativeDisplayId = Guid.NewGuid();

    private int _resizeEventCounter;

    protected override void OnInitialized()
    {
        DragState.StateChanged += DragState_StateChanged;

        base.OnInitialized();
    }

    private void DragState_StateChanged(object? sender, EventArgs e)
    {
        if (_dragStateEventHandler is not null)
        {
            _dragStateEventHandler();
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

    private void DispatchSubscribeToDragEventProviderStateAction(Action dragEventActionHandler)
    {
        var action = new SubscribeToDragEventProviderStateAction(_transformativeDisplayId,
            dragEventActionHandler);

        Dispatcher.Dispatch(action);
    }

    private void SubscribeToDragEventWithNorthResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerNorthResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerNorthResizeHandle);
    }

    private void DragEventHandlerNorthResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = 
                new DimensionValuedUnit(DimensionsRecord.Height.Value - DragState.Value.DeltaY, 
                    DimensionUnitKind.Pixels),
            Top =
                new DimensionValuedUnit(DimensionsRecord.Top.Value + DragState.Value.DeltaY,
                    DimensionUnitKind.Pixels),
        };

        if(OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithEastResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerEastResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerEastResizeHandle);
    }

    private void DragEventHandlerEastResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value + DragState.Value.DeltaX, 
                DimensionUnitKind.Pixels)
        };

        if(OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }

    private void SubscribeToDragEventWithSouthResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerSouthResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerSouthResizeHandle);
    }

    private void DragEventHandlerSouthResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = new DimensionValuedUnit(DimensionsRecord.Height.Value + DragState.Value.DeltaY, 
                DimensionUnitKind.Pixels)
        };

        if(OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithWestResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerWestResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerWestResizeHandle);
    }

    private void DragEventHandlerWestResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value - DragState.Value.DeltaX, 
                DimensionUnitKind.Pixels),
            Left = new DimensionValuedUnit(DimensionsRecord.Left.Value + DragState.Value.DeltaX, DimensionUnitKind.Pixels)
        };

        if(OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithNorthEastResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerNorthEastResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerNorthEastResizeHandle);
    }

    private void DragEventHandlerNorthEastResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = new DimensionValuedUnit(DimensionsRecord.Height.Value - DragState.Value.DeltaY,
                    DimensionUnitKind.Pixels),
            Top = new DimensionValuedUnit(DimensionsRecord.Top.Value + DragState.Value.DeltaY,
                    DimensionUnitKind.Pixels),
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value + DragState.Value.DeltaX,
                DimensionUnitKind.Pixels)
        };

        if (OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithSouthEastResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerSouthEastResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerSouthEastResizeHandle);
    }

    private void DragEventHandlerSouthEastResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = new DimensionValuedUnit(DimensionsRecord.Height.Value + DragState.Value.DeltaY,
                DimensionUnitKind.Pixels),
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value + DragState.Value.DeltaX,
                DimensionUnitKind.Pixels)
        };

        if (OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithSouthWestResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerSouthWestResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerSouthWestResizeHandle);
    }

    private void DragEventHandlerSouthWestResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = new DimensionValuedUnit(DimensionsRecord.Height.Value + DragState.Value.DeltaY,
                DimensionUnitKind.Pixels),
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value - DragState.Value.DeltaX,
                DimensionUnitKind.Pixels),
            Left = new DimensionValuedUnit(DimensionsRecord.Left.Value + DragState.Value.DeltaX, DimensionUnitKind.Pixels)
        };

        if (OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }
    
    private void SubscribeToDragEventWithNorthWestResizeHandle()
    {
        _dragStateEventHandler = DragEventHandlerNorthWestResizeHandle;
        DispatchSubscribeToDragEventProviderStateAction(DragEventHandlerNorthWestResizeHandle);
    }

    private void DragEventHandlerNorthWestResizeHandle()
    {
        _resizeEventCounter++;

        var nextDimensionsRecord = DimensionsRecord with
        {
            Height = new DimensionValuedUnit(DimensionsRecord.Height.Value - DragState.Value.DeltaY,
                    DimensionUnitKind.Pixels),
            Top = new DimensionValuedUnit(DimensionsRecord.Top.Value + DragState.Value.DeltaY,
                    DimensionUnitKind.Pixels),
            Width = new DimensionValuedUnit(DimensionsRecord.Width.Value - DragState.Value.DeltaX,
                DimensionUnitKind.Pixels),
            Left = new DimensionValuedUnit(DimensionsRecord.Left.Value + DragState.Value.DeltaX, DimensionUnitKind.Pixels)
        };

        if (OnDimensionsRecordChangedEventCallback.HasDelegate)
            OnDimensionsRecordChangedEventCallback.InvokeAsync(nextDimensionsRecord);
    }

    private void ValidateDimensionUnitKindIsSupported(string dimensionName, 
        DimensionValuedUnit dimensionValuedUnit)
    {
        if (dimensionValuedUnit.DimensionUnitKind != DimensionUnitKind.Pixels)
            throw new ApplicationException($"The {nameof(DimensionUnitKind)}: {dimensionValuedUnit.DimensionUnitKind} " +
                $"is not supported for {nameof(TransformativeDisplay)}. The name of the dimension with this " +
                $"unsupported type is named: {dimensionName}.");
    }

    protected override void Dispose(bool disposing)
    {
        DragState.StateChanged -= DragState_StateChanged;

        base.Dispose(disposing);
    }
}