using HunterFreemanDev.ClassLibrary.Direction;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridButtonsDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback<CardinalDirectionKind> CardinalDirectionKindSelectedEventCallback { get; set; }
    [Parameter, EditorRequired]
    public CardinalDirectionKind SelectedCardinalDirectionKind { get; set; }
    [Parameter]
    public bool IsAbsolutePosition { get; set; }

    private string GetExplanationText => SelectedCardinalDirectionKind switch
    {
        CardinalDirectionKind.CurrentPosition => "Add a tab",
        _ => "Construct grid alongside this"
    };

    private void FireCardinalDirectionKind(CardinalDirectionKind cardinalDirectionKind)
    {
        if (CardinalDirectionKindSelectedEventCallback.HasDelegate)
            CardinalDirectionKindSelectedEventCallback.InvokeAsync(cardinalDirectionKind);
    }

    private string GetIsActiveCss(CardinalDirectionKind cardinalDirectionKind)
    {
        return SelectedCardinalDirectionKind == cardinalDirectionKind
            ? "hfd_active"
            : string.Empty;
    }
}