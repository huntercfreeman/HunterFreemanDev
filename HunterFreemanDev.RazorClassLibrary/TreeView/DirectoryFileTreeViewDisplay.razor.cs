using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class DirectoryFileTreeViewDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public DirectoryFileTreeViewRecord DirectoryFileTreeViewRecord { get; set; } = null!;

    private void OnToggleIsExpandedEventCallback()
    {
        DirectoryFileTreeViewRecord.IsExpanded = !DirectoryFileTreeViewRecord.IsExpanded;

        if(!DirectoryFileTreeViewRecord.GetChildTreeViewRecords.Any())
            DirectoryFileTreeViewRecord.LoadChildTreeViewRecords();
    }
}