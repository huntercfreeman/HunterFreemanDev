using HunterFreemanDev.ClassLibrary.Element;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridColumnDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public int GridColumnIndex { get; set; }
    [Parameter, EditorRequired]
    public ElementRecord ElementRecord { get; set; } = null!;
}