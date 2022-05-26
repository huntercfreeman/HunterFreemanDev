using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class DefaultFileTreeViewDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public DefaultFileTreeViewRecord DefaultFileTreeViewRecord { get; set; } = null!;

    private void OnToggleIsExpandedEventCallback()
    {
        DefaultFileTreeViewRecord.IsExpanded = !DefaultFileTreeViewRecord.IsExpanded;
    }
}