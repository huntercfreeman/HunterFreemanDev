using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridBodyDisplay : ComponentBase
{
    [CascadingParameter]
    public RenderFragment EmptyGridTabContainerRenderFragment { get; set; } = null!;

    [Parameter, EditorRequired]
    public Type? RenderedContentType { get; set; }
    [Parameter, EditorRequired]
    public Dictionary<string, object>? Parameters { get; set; }
}