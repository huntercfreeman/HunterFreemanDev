using HunterFreemanDev.ClassLibrary.Element;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridRowDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public int GridRowIndex { get; set; }
    [Parameter, EditorRequired]
    public List<ElementRecord> GridRowElementReferences { get; set; } = null!;
}