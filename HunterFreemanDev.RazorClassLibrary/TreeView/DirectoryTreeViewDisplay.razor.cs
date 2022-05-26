using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class DirectoryTreeViewDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public DirectoryFileTreeViewRecord DirectoryFileTreeViewRecord { get; set; } = null!;
}