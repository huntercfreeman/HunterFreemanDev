using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class TreeViewTitleDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public string Title { get; set; } = null!;
}